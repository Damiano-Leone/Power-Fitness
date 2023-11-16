using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Power_Fitness.Corsi
{
    internal partial class Corsi : Power_Fitness.GestionePalestraBase.GestionePalestraBase
    {
        private string token;

        public Corsi(string token)
        {
            InitializeComponent();
            this.token = token;
            TipoOggetto = "Corso";

            // Aggiungi le colonne alla DataGridView
            InizializzaDataGridView();
            this.apiClient.SetAuthToken(token);
        }

        protected override void GestionePalestraBase_Load(object sender, EventArgs e)
        {
            base.GestionePalestraBase_Load(sender, e);
            // Imposta il valore di default per i DateTimePicker
            txtDataInizioCorso.Value = DateTime.Today;
            txtDataFineCorso.Value = DateTime.Today.AddDays(30);

            btnGestisci.Text = "Gestisci Lezioni";
            btnPrenotazioni.Visible = false;

            // Imposta il flag per indicare che l'evento Load è stato completato
            isLoadCompleted = true;

            // Imposta le larghezze delle colonne in base al numero di colonne del form
            SetColumnWidths();
        }

        protected override void caricaIcone()
        {
            base.caricaIcone();
            btnGestisci.Image = res.Properties.Resources.calendar_icon;
        }

        protected override async void btnAggiungi_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Length < 1)
            {
                MessageBox.Show("Il campo Nome non può essere vuoto");
                return;
            }

            int result = DateTime.Compare(txtDataFineCorso.Value, txtDataInizioCorso.Value);
            if (result < 0)
            {
                MessageBox.Show("Impostare una data di fine corso che sia successiva a quella di inizio corso");
                return;
            }


            try
            {
                // ToString per dare la certezza al compilatore che il tag sia effettivamente una stringa
                if (btnAggiungi.Tag.ToString() == "aggiungi")
                {
                    Corso corso = new Corso(txtNome.Text, txtDataInizioCorso.Value,
                        txtDataFineCorso.Value, txtDescrizione.Text);

                    string response = await apiClient.PostAsync<Corso>(Corso.AddCorsoRoute, corso);
                    Console.WriteLine($"AddCorso: {txtNome.Text}, {response}");
                }
                else
                {
                    int id = Convert.ToInt32(dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value);
                    Corso corso = new Corso(id, txtNome.Text, txtDataInizioCorso.Value,
                        txtDataFineCorso.Value, txtDescrizione.Text);

                    string response = await apiClient.PostAsync<Corso>(Corso.UpdateCorsoRoute, corso);
                    Console.WriteLine($"UpdateCorso: {txtNome.Text}, {response}");
                }
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Per non far chiudere il pannello azzerando i dati immessi mettiamo un return
                return;
            }

            base.btnAggiungi_Click(sender, e);
        }

        protected override void ResetPanel()
        {
            txtNome.Text = "";
            txtDataInizioCorso.Value = DateTime.Today;
            txtDataFineCorso.Value = DateTime.Today.AddDays(30);
            txtDescrizione.Text = "";
            base.ResetPanel();
        }

        protected override async void CaricaDatabase()
        {
            base.CaricaDatabase();

            try
            {
                string corsiJson = await apiClient.GetAsync(Corso.GetCorsiRoute);
                Console.WriteLine($"GetCorsi: {corsiJson}");

                // Converti la risposta JSON in un oggetto Corso
                List<Corso> listaCorsi = JsonConvert.DeserializeObject<List<Corso>>(corsiJson);

                int numeroRighe = 0;

                // Aggiungi i dati alla griglia
                foreach (var corso in listaCorsi)
                {
                    dataElencoIstanze.Rows.Add(corso.ValoreId.ToString(), corso.Nome,
                        corso.DataInizioCorso.ToString("dddd, dd MMMM yyyy"),
                        corso.DataFineCorso.ToString("dddd, dd MMMM yyyy"),
                        corso.Descrizione);

                    // Incrementa il contatore delle righe
                    numeroRighe++;

                    Console.WriteLine($"ValoreId: {corso.ValoreId}");
                    Console.WriteLine($"Nome: {corso.Nome}");
                    Console.WriteLine($"DataInizioCorso: {corso.DataInizioCorso.ToString("dd MMMM yyyy")}");
                    Console.WriteLine($"DataFineCorso: {corso.DataFineCorso.ToString("dd MMMM yyyy")}");
                    Console.WriteLine($"Descrizione: {corso.Descrizione}");
                    Console.WriteLine("--------------------------------------");
                }
                // Visualizza il numero totale di righe
                lblNumeroTotale.Text = $"{numeroRighe}";
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Al momento, non ci sono corsi registrati", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        protected override void CaricaDatiIstanzaSelezionata()
        {
            // Modifica campi
            txtNome.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[1].Value.ToString();
            txtDataInizioCorso.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[2].Value.ToString();
            txtDataFineCorso.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[3].Value.ToString();
            txtDescrizione.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[4].Value.ToString();
        }

        protected override async void btnRimuoviIstanza_Click(object sender, EventArgs e)
        {
            base.btnRimuoviIstanza_Click(sender, e);

            if (dataElencoIstanze.SelectedRows.Count > 0)
            {
                string idRimozione = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value.ToString();
                try
                {
                    if (MessageBox.Show("Sei sicuro di voler rimuovere il corso con ID = "
                        + idRimozione + "?", "Rimuovere il corso?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        string response = await apiClient.PostAsync<string>(Corso.DeleteCorsoRoute, idRimozione);
                        Console.WriteLine($"DeleteCorso: {idRimozione}, {response}");

                        // Aggiorna la vista del Database
                        CaricaDatabase();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Per non far chiudere il pannello azzerando i dati immessi mettiamo un return
                    return;
                }
            }
        }

        protected override void InizializzaDataGridView()
        {
            // Aggiungi le colonne alla DataGridView
            dataElencoIstanze.Columns.Add("ID", "ID");
            dataElencoIstanze.Columns.Add("Nome", "Nome");
            dataElencoIstanze.Columns.Add("DataInizioCorso", "Data Inizio Corso");
            dataElencoIstanze.Columns.Add("DataFineCorso", "Data Fine Corso");
            dataElencoIstanze.Columns.Add("Descrizione", "Descrizione");

            btnAggiungiIstanza.Text = $"Aggiungi {TipoOggetto}";
            btnModificaIstanza.Text = $"Modifica {TipoOggetto}";
            btnRimuoviIstanza.Text = $"Rimuovi {TipoOggetto}";
        }

        protected override void dataElencoIstanze_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataElencoIstanze.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string messaggioTooltip = "Clicca due volte per aprire la pagina delle lezioni del corso.";
                cell.ToolTipText = messaggioTooltip;
            }
        }

        protected override void txtRicerca_TextChanged(object sender, EventArgs e)
        {
            FiltraDataGridView(dataElencoIstanze, txtRicerca.Text, "Nome");
        }

        protected override void EseguiAzioneSullaRigaSelezionata(string clickedBtn = "")
        {
            List<string> contenutoColonne = OtteniContenutoCelleSelezionate("ID", "Nome");

            if (clickedBtn == "btnGestisci")
            {
                Lezioni.Lezioni FormLezioni = new Lezioni.Lezioni(token, contenutoColonne);
                FormLezioni.ShowDialog();
            }

        }


    }
}
