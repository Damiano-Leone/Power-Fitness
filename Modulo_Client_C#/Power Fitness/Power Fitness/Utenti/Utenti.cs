using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Power_Fitness.Abbonamenti;
using System.Threading.Tasks;

namespace Power_Fitness.Utenti
{
    internal partial class Utenti : Power_Fitness.GestionePalestraBase.GestionePalestraBase
    {
        private string token;
        public Utenti(string token)
        {
            InitializeComponent();
            this.token = token;
            this.apiClient.SetAuthToken(token);
            TipoOggetto = "Utente";

            // Aggiungi le colonne alla DataGridView
            InizializzaDataGridView();
        }

        protected override void GestionePalestraBase_Load(object sender, EventArgs e)
        {
            base.GestionePalestraBase_Load(sender, e);

            // Imposta il valore di default per il DateTimePicker
            txtDataNascita.Value = new DateTime(1990, 1, 1);

            btnGestisci.Text = "Gestisci Abbonamenti";
            btnPrenotazioni.Text = "Gestisci Prenotazioni";

            // Imposta il flag per indicare che l'evento Load è stato completato
            isLoadCompleted = true;

            // Imposta le larghezze delle colonne in base al numero di colonne del form
            SetColumnWidths();
        }

        protected override void caricaIcone()
        {
            base.caricaIcone();
            btnGestisci.Image = res.Properties.Resources.abbonamenti_icon;
            btnPrenotazioni.Image = res.Properties.Resources.gym_appointment;
        }

        protected override async void btnAggiungi_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Length < 1)
            {
                MessageBox.Show("Il campo Nome non può essere vuoto");
                return;
            }

            if (txtCognome.Text.Length < 1)
            {
                MessageBox.Show("Il campo Cognome non può essere vuoto");
                return;
            }

            if (txtCodFiscale.Text.Length < 1)
            {
                MessageBox.Show("Il campo Codice Fiscale non può essere vuoto");
                return;
            }

            // Verifica se la lunghezza è corretta (16 caratteri)
            if (txtCodFiscale.Text.Length != 16)
            {
                MessageBox.Show("Il campo Codice Fiscale deve contenere 16 caratteri");
                return;
            }

            // Verifica se il codice fiscale contiene solo caratteri validi
            if (!Regex.IsMatch(txtCodFiscale.Text, "^[A-Za-z0-9]+$"))
            {
                MessageBox.Show("Il campo Codice Fiscale deve contenere solo caratteri validi");
                return;
            }

            // Verifica se è stato selezionato un genere
            if (comboBoxGenere.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona un genere (Maschio o Femmina)");
                return;
            }

            if (txtEmail.Text.Length < 1)
            {
                MessageBox.Show("Il campo Email non può essere vuoto");
                return;
            }

            // Verifica se se l'indirizzo email ha il formato corretto
            if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Il campo Email deve avere il formato corretto");
                return;
            }

            // Verifica se il campo è presente e se presente, se contiene un numero intero positivo
            if (txtNumCivico.Text.Length >= 1 && !Regex.IsMatch(txtNumCivico.Text, @"^\d+$"))
            {
                MessageBox.Show("Il campo Numero Civico deve contenere un numero intero positivo.");
                return;
            }

            try
            {
                // ToString per dare la certezza al compilatore che il tag sia effettivamente una stringa
                if (btnAggiungi.Tag.ToString() == "aggiungi")
                {
                    Utente utente = new Utente(txtCodFiscale.Text.ToUpper(), txtNome.Text, txtCognome.Text,
                        comboBoxGenere.Text, txtDataNascita.Value, txtCellulare.Text,
                        txtEmail.Text, txtIndirizzo.Text, txtNumCivico.Text);

                    string response = await apiClient.PostAsync<Utente>(Utente.AddUtenteRoute, utente);
                    Console.WriteLine($"AddUtente: {txtCodFiscale.Text.ToUpper()}, {response}");
                }
                else
                {
                    int id = Convert.ToInt32(dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value);
                    Utente utente = new Utente(id, txtCodFiscale.Text.ToUpper(), txtNome.Text, txtCognome.Text,
                        comboBoxGenere.Text, txtDataNascita.Value, txtCellulare.Text,
                        txtEmail.Text, txtIndirizzo.Text, txtNumCivico.Text);

                    string response = await apiClient.PostAsync<Utente>(Utente.UpdateUtenteRoute, utente);
                    Console.WriteLine($"UpdateUtente: {txtCodFiscale.Text.ToUpper()}, {response}");
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
            txtCodFiscale.Text = "";
            txtNome.Text = "";
            txtCognome.Text = "";
            comboBoxGenere.SelectedIndex = -1;
            txtDataNascita.Value = new DateTime(1990, 1, 1);
            txtCellulare.Text = "";
            txtEmail.Text = "";
            txtIndirizzo.Text = "";
            txtNumCivico.Text = "";
            base.ResetPanel();
        }

        protected override void btnAggiungiIstanza_Click(object sender, EventArgs e)
        {
            base.btnAggiungiIstanza_Click(sender, e);

            txtNome.Enabled = true;
            txtCognome.Enabled = true;
            txtCodFiscale.Enabled = true;
            comboBoxGenere.Enabled = true;
            txtDataNascita.Enabled = true;
        }

        protected override void btnModificaIstanza_Click(object sender, EventArgs e)
        {
            base.btnModificaIstanza_Click(sender, e);

            txtNome.Enabled = false;
            txtCognome.Enabled = false;
            txtCodFiscale.Enabled = false;
            comboBoxGenere.Enabled = false;
            txtDataNascita.Enabled = false;
        }

        protected override async void CaricaDatabase()
        {
            base.CaricaDatabase();

            try
            {
                string utentiJson = await apiClient.GetAsync(Utente.GetUtentiRoute);
                Console.WriteLine($"GetUtenti: {utentiJson}");

                // Converti la risposta JSON in un oggetto Utente
                List<Utente> listaUtenti = JsonConvert.DeserializeObject<List<Utente>>(utentiJson);

                int numeroRighe = 0;

                // Aggiungi i dati alla griglia
                foreach (var utente in listaUtenti)
                {
                    if (utente.NumeroCivico == "0")
                    {
                        utente.NumeroCivico = ""; // Sostituisci "0" con una stringa vuota
                    }

                    dataElencoIstanze.Rows.Add(utente.ValoreId.ToString(), 
                        utente.Nome, utente.Cognome, utente.CodiceFiscale, utente.Genere,
                        utente.DataNascita.ToString("dddd, dd MMMM yyyy"), utente.Cellulare,
                        utente.Email, utente.Indirizzo, utente.NumeroCivico.ToString());

                    // Incrementa il contatore delle righe
                    numeroRighe++;

                    Console.WriteLine($"ValoreId: {utente.ValoreId}");
                    Console.WriteLine($"CodiceFiscale: {utente.CodiceFiscale}");
                    Console.WriteLine($"Nome: {utente.Nome}");
                    Console.WriteLine($"Cognome: {utente.Cognome}");
                    Console.WriteLine($"Genere: {utente.Genere}");
                    Console.WriteLine($"DataNascita: {utente.DataNascita.ToString("dd MMMM yyyy")}");
                    Console.WriteLine($"Cellulare: {utente.Cellulare}");
                    Console.WriteLine($"Email: {utente.Email}");
                    Console.WriteLine($"Indirizzo: {utente.Indirizzo}");
                    Console.WriteLine($"NumeroCivico: {utente.NumeroCivico}");
                    Console.WriteLine("--------------------------------------");
                }
                // Visualizza il numero totale di righe
                lblNumeroTotale.Text = $"{numeroRighe}";
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Al momento, non ci sono utenti registrati", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void CaricaDatiIstanzaSelezionata()
        {
            // Modifica campi
            txtNome.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[1].Value.ToString();
            txtCognome.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[2].Value.ToString();
            txtCodFiscale.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[3].Value.ToString();
            comboBoxGenere.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[4].Value.ToString();
            txtDataNascita.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[5].Value.ToString();
            txtCellulare.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[6].Value.ToString();
            txtEmail.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[7].Value.ToString();
            txtIndirizzo.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[8].Value.ToString();
            txtNumCivico.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[9].Value.ToString();
        }

        protected override async void btnRimuoviIstanza_Click(object sender, EventArgs e)
        {
            base.btnRimuoviIstanza_Click(sender, e);

            if (dataElencoIstanze.SelectedRows.Count > 0)
            {
                string idRimozione = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value.ToString();
                try
                {
                    if (MessageBox.Show("Sei sicuro di voler rimuovere l'utente con ID = "
                        + idRimozione + "?", "Rimuovere l'utente?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        string response = await apiClient.PostAsync<string>(Utente.DeleteUtenteRoute, idRimozione);
                        Console.WriteLine($"DeleteUtente: {idRimozione}, {response}");

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
            dataElencoIstanze.Columns.Add("Cognome", "Cognome");
            dataElencoIstanze.Columns.Add("Codice Fiscale", "Codice Fiscale");
            dataElencoIstanze.Columns.Add("Genere", "Genere");
            dataElencoIstanze.Columns.Add("Data di Nascita", "Data di Nascita");
            dataElencoIstanze.Columns.Add("Cellulare", "Cellulare");
            dataElencoIstanze.Columns.Add("Email", "Email");
            dataElencoIstanze.Columns.Add("Indirizzo", "Indirizzo");
            dataElencoIstanze.Columns.Add("Numero Civico", "Numero Civico");

            btnAggiungiIstanza.Text = $"Aggiungi {TipoOggetto}";
            btnModificaIstanza.Text = $"Modifica {TipoOggetto}";
            btnRimuoviIstanza.Text = $"Rimuovi {TipoOggetto}";
        }

        protected override void dataElencoIstanze_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataElencoIstanze.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string messaggioTooltip = "Clicca due volte per aprire la pagina degli abbonamenti dell'utente.";
                cell.ToolTipText = messaggioTooltip;
            }
        }

        protected override void txtRicerca_TextChanged(object sender, EventArgs e)
        {
            FiltraDataGridView(dataElencoIstanze, txtRicerca.Text, "Codice Fiscale", "Nome", "Cognome", "Email");
        }


        protected override async void EseguiAzioneSullaRigaSelezionata(string clickedBtn = "")
        {
            List<string> contenutoColonne = OtteniContenutoCelleSelezionate("ID", "Nome", "Cognome", "Email");

            if (clickedBtn == "btnGestisci")
            {
                Abbonamenti.Abbonamenti FormAbbonamenti = new Abbonamenti.Abbonamenti(token, contenutoColonne);
                FormAbbonamenti.ShowDialog();
            }

            else if (clickedBtn == "btnPrenotazioni")
            {
                // Verifico se ha un abbonamento attivo
                Abbonamento abbonamento = await controlloAbbonamentoUtente();
                if (abbonamento != null)
                {
                    // Se ha un abbonamento attivo posso prenotarlo alle lezioni
                    Prenotazioni.Prenotazioni FormPrenotazioni = new Prenotazioni.Prenotazioni(token, contenutoColonne);
                    FormPrenotazioni.ShowDialog();
                }
            }
        }

        private async Task<Abbonamento> controlloAbbonamentoUtente()
        {
            try
            {
                string id = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value.ToString();
                string abbonamentoJson = await apiClient.PostAsync<string>(Abbonamento.GetAbbonamentoAttivoRoute, id);
                Console.WriteLine($"GetAbbonamentoAttivo: {abbonamentoJson}");

                // Gestisci il caso in cui non ci sia alcun abbonamento attivo
                if (string.IsNullOrEmpty(abbonamentoJson))
                {
                    // Nessun abbonamento attivo trovato
                    return null; 
                }

                // Converti la risposta JSON in un oggetto Abbonamento
                Abbonamento abbonamento = JsonConvert.DeserializeObject<Abbonamento>(abbonamentoJson);
                return abbonamento;
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Restituisci un valore null in caso di errore
                return null; 
            }
        }

    }
}
