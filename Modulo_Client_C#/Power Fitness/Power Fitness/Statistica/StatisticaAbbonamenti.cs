using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;
using Power_Fitness.Abbonamenti;
using Power_Fitness.FlaskApi;

namespace Power_Fitness.Statistica
{
    public partial class StatisticaAbbonamenti : Form
    {
        private FlaskApiClient apiClient;
        private Dictionary<string, List<int>> dizionarioAbbonamenti = new Dictionary<string, List<int>>();

        public StatisticaAbbonamenti()
        {
            InitializeComponent();
            apiClient = new FlaskApiClient();
        }

        private void StatisticaAbbonamenti_Load(object sender, EventArgs e)
        {
            dataInizioStat.Value = DateTime.Now.AddDays(-14);
        }

        private async Task CaricaStatisticaAbbonamenti(string data)
        {
            try
            {
                string abbonamentiJson = await apiClient.PostAsync(Abbonamento.GetDatiRoute, data);
                Console.WriteLine($"GetAbbonamenti: {abbonamentiJson}");

                // Pulisce il dizionario esistente
                dizionarioAbbonamenti.Clear();

                // Converti la risposta JSON in un oggetto dizionario
                dizionarioAbbonamenti = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(abbonamentiJson)
                    .OrderBy(kvp => DateTime.ParseExact(kvp.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message + "\nImpossibile caricare gli abbonamenti.",
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void dataInizioStat_ValueChanged(object sender, EventArgs e)
        {
            await aggiornaChart(dataInizioStat.Value);
        }

        private async Task aggiornaChart(DateTime data)
        {
            // Rimuovi tutti i punti di ciascuna serie
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }

            // Data attuale
            DateTime currentDate = data;
            await CaricaStatisticaAbbonamenti(currentDate.ToString());

            
            var dateList = new List<string>();

            DateTime startDate = currentDate; // Imposta la data iniziale
            DateTime endDate;

            // Ottieni l'ultima data presente nel dizionario
            string lastDateInDictionary = dizionarioAbbonamenti.Keys.LastOrDefault();

            if (lastDateInDictionary != null)
            {
                endDate = DateTime.ParseExact(lastDateInDictionary, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(1);
            }
            else
            {
                // Se il dizionario è vuoto, puoi impostare endDate come la data attuale
                endDate = currentDate;
            }

            while (startDate <= endDate)
            {
                dateList.Add(startDate.ToString("yyyy-MM-dd"));
                startDate = startDate.AddDays(1);
            }


            // Per ogni data possibile, verifica se i dati sono presenti e aggiungi un punto dati all'istogramma
            foreach (string date in dateList)
            {
                if (dizionarioAbbonamenti.ContainsKey(date))
                {
                    List<int> statistics = dizionarioAbbonamenti[date];

                    chart.Series["1 Mese"].Points.AddXY(date, statistics[0]);
                    chart.Series["2 Mesi"].Points.AddXY(date, statistics[1]);
                    chart.Series["3 Mesi"].Points.AddXY(date, statistics[2]);
                    chart.Series["6 Mesi"].Points.AddXY(date, statistics[3]);
                    chart.Series["Annuale"].Points.AddXY(date, statistics[4]);
                }
                else
                {
                    // Impostiamo il valore a 0 che rappresenta l'assenza di dati in quel giorno.
                    chart.Series["1 Mese"].Points.AddXY(date, 0);
                    chart.Series["2 Mesi"].Points.AddXY(date, 0);
                    chart.Series["3 Mesi"].Points.AddXY(date, 0);
                    chart.Series["6 Mesi"].Points.AddXY(date, 0);
                    chart.Series["Annuale"].Points.AddXY(date, 0);
                }
            }
        }
    }
}
