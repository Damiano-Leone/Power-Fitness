using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Power_Fitness.Corsi.Lezioni
{
    internal partial class Lezioni : Power_Fitness.GestionePalestraBase.GestionePalestraBase
    {
        private List<string> corso;

        public Lezioni(string token, List<string> corso)
        {
            InitializeComponent();
            this.apiClient.SetAuthToken(token);
            TipoOggetto = "Lezione";
            this.corso = new List<string>(corso);

            this.Text = "Lezioni di: " + corso[1];
            // Aggiungi le colonne alla DataGridView
            InizializzaDataGridView();
        }

        protected override void GestionePalestraBase_Load(object sender, EventArgs e)
        {
            base.GestionePalestraBase_Load(sender, e);

            // Imposta il valore di default per i DateTimePicker
            oraInizio.Value = DateTime.Today.AddHours(15).AddMinutes(0);
            oraFine.Value = DateTime.Today.AddHours(17).AddMinutes(0);
            numMaxPart.Value = 30;

            btnGestisci.Visible = false;
            btnPrenotazioni.Visible = false;

            // Imposta il flag per indicare che l'evento Load è stato completato
            isLoadCompleted = true;

            // Imposta le larghezze delle colonne in base al numero di colonne del form
            SetColumnWidths();
        }


        protected override async void btnAggiungi_Click(object sender, EventArgs e)
        {
            // Verifica se è stato selezionato un giorno della settimana
            if (comboBoxGiorno.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona un Giorno della Settimana");
                return;
            }

            int result = DateTime.Compare(oraFine.Value, oraInizio.Value);
            if (result <= 0)
            {
                MessageBox.Show("Impostare un ora di fine lezione che sia successiva all'ora di inizio");
                return;
            }

            string nomeGiornoItaliano = comboBoxGiorno.Text;
            string nomeGiornoInglese = "";
            if (Lezione.mappaGiorni.TryGetValue(nomeGiornoItaliano, out nomeGiornoInglese))
            {
                Console.WriteLine(nomeGiornoInglese);
            }
            else
            {
                MessageBox.Show("Impostare un giorno della settimana valido");
                return;
            }

            try
            {
                // ToString per dare la certezza al compilatore che il tag sia effettivamente una stringa
                if (btnAggiungi.Tag.ToString() == "aggiungi")
                {
                    if (ControlloGiornoEFasciaOraria(dataElencoIstanze, comboBoxGiorno.Text,
                        oraInizio.Value, oraFine.Value, out DateTime? oraInizioLezione, out DateTime? oraFineLezione))
                    {
                        MessageBox.Show("Non è possibile aggiungere la seguente fascia oraria, poiché esiste già una lezione dalle: "
                            + oraInizioLezione.Value.ToString("HH:mm") + " alle: " + oraFineLezione.Value.ToString("HH:mm")
                            + ".\nPer favore, seleziona un'altra fascia oraria.",
                             $"Imposssibile aggiungere la lezione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Lezione lezione = new Lezione(Convert.ToInt32(corso[0]), nomeGiornoInglese,
                        oraInizio.Value, oraFine.Value, Convert.ToInt32(numMaxPart.Value));

                    string response = await apiClient.PostAsync<Lezione>(Lezione.AddLezioneRoute, lezione);
                    Console.WriteLine($"AddLezione: {lezione}, {response}");
                }
                else
                {
                    int id = Convert.ToInt32(dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value);
                    int rowIndex = dataElencoIstanze.SelectedRows[0].Index;

                    if (ControlloGiornoEFasciaOraria(dataElencoIstanze, comboBoxGiorno.Text, oraInizio.Value, oraFine.Value, 
                        out DateTime? oraInizioLezione, out DateTime? oraFineLezione, rowIndex))
                    {
                        MessageBox.Show("Non è possibile aggiungere la seguente fascia oraria, poiché esiste già una lezione dalle: "
                            + oraInizioLezione.Value.ToString("HH:mm") + " alle: " + oraFineLezione.Value.ToString("HH:mm")
                            + ".\nPer favore, seleziona un'altra fascia oraria.",
                             $"Imposssibile aggiungere la lezione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    
                    Lezione lezione = new Lezione(id, Convert.ToInt32(corso[0]), nomeGiornoInglese,
                        oraInizio.Value, oraFine.Value, Convert.ToInt32(numMaxPart.Value));

                    string response = await apiClient.PostAsync<Lezione>(Lezione.UpdateLezioneRoute, lezione);
                    Console.WriteLine($"UpdateLezione: {lezione}, {response}");
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
            comboBoxGiorno.SelectedIndex = -1;
            oraInizio.Value = DateTime.Today.AddHours(15).AddMinutes(0);
            oraFine.Value = DateTime.Today.AddHours(17).AddMinutes(0);
            numMaxPart.Value = 30;
            base.ResetPanel();
        }

        protected override void btnAggiungiIstanza_Click(object sender, EventArgs e)
        {
            base.btnAggiungiIstanza_Click(sender, e);

            comboBoxGiorno.Enabled = true;
        }

        protected override void btnModificaIstanza_Click(object sender, EventArgs e)
        {
            base.btnModificaIstanza_Click(sender, e);

            comboBoxGiorno.Enabled = false;
        }

        protected override async void CaricaDatabase()
        {
            base.CaricaDatabase();

            try
            {
                string lezioniJson = await apiClient.PostAsync(Lezione.GetLezioniRoute, corso[0]);
                Console.WriteLine($"GetLezioni: {lezioniJson}");

                // Converti la risposta JSON in un oggetto Lezione
                List<Lezione> listaLezioni = JsonConvert.DeserializeObject<List<Lezione>>(lezioniJson);

                int numeroRighe = 0;

                // Aggiungi i dati alla griglia
                foreach (var lezione in listaLezioni)
                {
                    string nomeGiornoItaliano = Lezione.mappaGiorni.FirstOrDefault(x => x.Value == lezione.Giorno).Key;

                    dataElencoIstanze.Rows.Add(lezione.ValoreId.ToString(), nomeGiornoItaliano,
                        lezione.OraInizio.ToString("HH:mm"), lezione.OraFine.ToString("HH:mm"), 
                        lezione.MaxPartecipanti.ToString());

                    // Incrementa il contatore delle righe
                    numeroRighe++;

                    Console.WriteLine($"ValoreId: {lezione.ValoreId}");
                    Console.WriteLine($"Giorno: {lezione.Giorno}");
                    Console.WriteLine($"OraInizio: {lezione.OraInizio.ToString("HH:mm")}");
                    Console.WriteLine($"OraFine: {lezione.OraFine.ToString("HH:mm")}");
                    Console.WriteLine($"MaxPartecipanti: {lezione.MaxPartecipanti}");
                    Console.WriteLine("--------------------------------------");
                }
                // Visualizza il numero totale di righe
                lblNumeroTotale.Text = $"{numeroRighe}";
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Al momento, non sono programmate lezioni per questo corso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void CaricaDatiIstanzaSelezionata()
        {
            // Modifica campi
            comboBoxGiorno.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[1].Value.ToString();
            oraInizio.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[2].Value.ToString();
            oraFine.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[3].Value.ToString();
            numMaxPart.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[4].Value.ToString();
        }

        protected override async void btnRimuoviIstanza_Click(object sender, EventArgs e)
        {
            base.btnRimuoviIstanza_Click(sender, e);

            if (dataElencoIstanze.SelectedRows.Count > 0)
            {
                string idRimozione = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value.ToString();
                try
                {
                    if (MessageBox.Show("Sei sicuro di voler rimuovere la lezione con ID = "
                        + idRimozione + "?", "Rimuovere la lezione?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        string response = await apiClient.PostAsync<string>(Lezione.DeleteLezioneRoute, idRimozione);
                        Console.WriteLine($"DeleteLezione: {idRimozione}, {response}");

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
            dataElencoIstanze.Columns.Add("Giorno della Settimana", "Giorno della Settimana");
            dataElencoIstanze.Columns.Add("Ora di Inizio", "Ora di Inizio");
            dataElencoIstanze.Columns.Add("Ora di Fine", "Ora di Fine");
            dataElencoIstanze.Columns.Add("Numero max di Partecipanti", "Numero max di Partecipanti");
            

            btnAggiungiIstanza.Text = $"Aggiungi {TipoOggetto}";
            btnModificaIstanza.Text = $"Modifica {TipoOggetto}";
            btnRimuoviIstanza.Text = $"Rimuovi {TipoOggetto}";
        }

        protected override void dataElencoIstanze_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataElencoIstanze.Rows[e.RowIndex];
                string idLezione = row.Cells["ID"].Value.ToString();
                string messaggioTooltip = "ID Lezione: " + idLezione + ", Lezione di " + corso[1];
                row.Cells[e.ColumnIndex].ToolTipText = messaggioTooltip;
            }
        }

        protected override void txtRicerca_TextChanged(object sender, EventArgs e)
        {
            FiltraDataGridView(dataElencoIstanze, txtRicerca.Text, "Giorno della Settimana");
        }

        private bool ControlloGiornoEFasciaOraria(DataGridView dataGridView, string giornoSettimana, 
            DateTime oraInizioDaVerificare, DateTime oraFineDaVerificare,
            out DateTime? oraInizioLezione, out DateTime? oraFineLezione, int rowIndexToExclude = -1)
        {
            oraInizioLezione = null;
            oraFineLezione = null;

            // Itera su tutte le righe della DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Ignora la riga che si sta cercando di modificare
                if (row.Index == rowIndexToExclude)
                {
                    continue;
                }

                if (row.Cells["Giorno della Settimana"].Value.ToString() == giornoSettimana)
                {
                    // Ci assicuriamo che la DataGridView abbia colonne per ora di inizio e ora di fine
                    if (row.Cells["Ora di Inizio"].Value != null && row.Cells["Ora di Fine"].Value != null)
                    {
                        // Estrae l'ora di inizio e l'ora di fine dalla riga corrente
                        DateTime oraInizio = DateTime.Parse(row.Cells["Ora di Inizio"].Value.ToString());
                        DateTime oraFine = DateTime.Parse(row.Cells["Ora di Fine"].Value.ToString());

                        // Controlla se la fascia oraria inserita è compresa tra l'ora di inizio e l'ora di fine
                        bool match = oraInizioDaVerificare > oraInizio && oraInizioDaVerificare < oraFine;
                        bool match2 = oraFineDaVerificare > oraInizio && oraFineDaVerificare < oraFine;
                        if (match || match2)
                        {
                            // Almeno una corrispondenza è stata trovata
                            oraInizioLezione = oraInizio;
                            oraFineLezione = oraFine;
                            return true;
                        }
                    }
                }
            }
                return false;
        }

    }
}
