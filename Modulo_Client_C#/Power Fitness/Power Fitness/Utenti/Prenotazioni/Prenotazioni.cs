using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Power_Fitness.Abbonamenti;
using Power_Fitness.Corsi;
using Power_Fitness.Corsi.Lezioni;

namespace Power_Fitness.Utenti.Prenotazioni
{
    internal partial class Prenotazioni : Power_Fitness.GestionePalestraBase.GestionePalestraBase
    {
        private List<string> utente;
        private Dictionary<int, string> dizionarioCorsi = new Dictionary<int, string>();
        private Dictionary<int, List<string>> dizionarioLezioni = new Dictionary<int, List<string>>();

        public Prenotazioni(string token, List<string> utente)
        {
            InitializeComponent();
            this.apiClient.SetAuthToken(token);
            TipoOggetto = "Prenotazioni";
            this.utente = new List<string>(utente);
            this.Text = "Prenotazioni di: " + utente[1] + " " + utente[2];
            // Aggiungi le colonne alla DataGridView
            InizializzaDataGridView();
        }

        protected override async void GestionePalestraBase_Load(object sender, EventArgs e)
        {

            // Inizializzo il Dizionario Corsi con i relativi corsi
            dizionarioCorsi = await CaricaCorsi();

            base.GestionePalestraBase_Load(sender, e);

            // Imposta il valore di default per i DateTimePicker
            dataPrenotazione.Value = DateTime.Today;

            // Disabilita dataPrenotazione e FasciaOraria se non c'è nessuna opzione selezionata come Corso
            dataPrenotazione.Enabled = false;
            comboBoxFasciaOraria.Enabled = false;

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
            if (comboBoxCorso.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona un Corso");
                return;
            }

            int result = DateTime.Compare(dataPrenotazione.Value, DateTime.Today);
            if (result < 0)
            {
                MessageBox.Show("Impostare una Data di Prenotazione che sia pari o successiva a quella di oggi");
                return;
            }

            // Verifica se è stato selezionato un giorno della settimana
            if (comboBoxFasciaOraria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona una Fascia Oraria");
                return;
            }

            Abbonamento abbonamento = await getAbbonamentoAttivo(Convert.ToInt32(utente[0]));

            if (abbonamento == null )
            {
                // si è verificato un errore
                MessageBox.Show("Impossibile prenotare, l'utente non ha un abbonamento attivo in data: " + dataPrenotazione.Value,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string giornoDellaSettimana = dataPrenotazione.Value.DayOfWeek.ToString();
            string fasciaOraria = comboBoxFasciaOraria.Text;

            int idLezione = dizionarioLezioni.FirstOrDefault(
                kvp => kvp.Value[0] == giornoDellaSettimana && kvp.Value[1] == fasciaOraria).Key;

            if (idLezione == 0)
            {
                return;
            }

            try
            {
                // ToString per dare la certezza al compilatore che il tag sia effettivamente una stringa
                if (btnAggiungi.Tag.ToString() == "aggiungi")
                {

                    Prenotazione prenotazione = new Prenotazione(Convert.ToInt32(idLezione), Convert.ToInt32(utente[0]),
                        dataPrenotazione.Value);

                    string response = await apiClient.PostAsync<Prenotazione>(Prenotazione.AddPrenotazioneRoute, prenotazione);
                    Console.WriteLine($"AddLezione: {prenotazione}, {response}");
                }
                else
                {
                    int id = Convert.ToInt32(dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value);

                    Prenotazione prenotazione = new Prenotazione(id, Convert.ToInt32(idLezione), Convert.ToInt32(utente[0]),
                        dataPrenotazione.Value);

                    string response = await apiClient.PostAsync<Prenotazione>(Prenotazione.UpdatePrenotazioneRoute, prenotazione);
                    Console.WriteLine($"UpdateLezione: {prenotazione}, {response}");
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
            comboBoxCorso.SelectedIndex = -1;
            dataPrenotazione.Value = DateTime.Now;
            dataPrenotazione.Enabled = false;
            comboBoxFasciaOraria.SelectedIndex = -1;
            comboBoxFasciaOraria.Text = string.Empty;
            base.ResetPanel();
        }


        protected override void btnModificaIstanza_Click(object sender, EventArgs e)
        {
            comboBoxCorso.Enabled = true;
            dataPrenotazione.Enabled = true;
            comboBoxFasciaOraria.Enabled = true;
            base.btnModificaIstanza_Click(sender, e);
            comboBoxCorso.Enabled = false;
            dataPrenotazione.Enabled = false;
            comboBoxFasciaOraria.Enabled = true;
        }

        protected override async void CaricaDatabase()
        {
            base.CaricaDatabase();

            try
            {
                // Gli passo Id Lezione e IdUtente in un json
                string prenotazioniJson = await apiClient.PostAsync(Prenotazione.GetPrenotazioniUserRoute, utente[0]);
                Console.WriteLine($"GetLezioni: {prenotazioniJson}");

                // Converti la risposta JSON in un oggetto Lezione
                List<Prenotazione> listaPrenotazioni = JsonConvert.DeserializeObject<List<Prenotazione>>(prenotazioniJson);

                int numeroRighe = 0;

                // Aggiungi i dati alla griglia
                foreach (var prenotazione in listaPrenotazioni)
                {

                    int idLezione = prenotazione.IdLezione;

                    Lezione lezione = await getLezione(idLezione);
                    if (lezione == null)
                    {
                        // si è verificato un errore
                        MessageBox.Show("Si è verificata un incongruenza nel database.\nNessuna Lezione ha l'id a cui si è prenotato l'utente.",
                            "Errore: ID lezione non trovato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Corso corso = await getCorso(lezione.IdCorso);
                    if (corso == null)
                    {
                        // si è verificato un errore
                        MessageBox.Show("Si è verificata un incongruenza nel database.\nNessun Corso ha l'id a cui si è prenotato l'utente.",
                            "Errore: ID corso non trovato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    string fasciaOraria = $"{lezione.OraInizio.ToString("HH:mm")}-{lezione.OraFine.ToString("HH:mm")}";

                    dataElencoIstanze.Rows.Add(prenotazione.ValoreId.ToString(), corso.Nome, 
                        prenotazione.Data.ToString("dddd, dd MMMM yyyy"), fasciaOraria);

                    // Incrementa il contatore delle righe
                    numeroRighe++;

                    Console.WriteLine($"ValoreId: {prenotazione.ValoreId}");
                    Console.WriteLine($"NomeCorso: {corso.Nome}");
                    Console.WriteLine($"DataPrenotazione: {prenotazione.Data.ToString("dd MMMM yyyy")}");
                    Console.WriteLine($"FasciaOraria: {fasciaOraria}");
                    Console.WriteLine("--------------------------------------");
                }
                // Visualizza il numero totale di righe
                lblNumeroTotale.Text = $"{numeroRighe}";
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Al momento, non ci sono prenotazioni registrate per questo utente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void CaricaDatiIstanzaSelezionata()
        {
            // Modifica campi
            comboBoxCorso.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[1].Value.ToString();
            dataPrenotazione.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[2].Value.ToString();
            dataPrenotazione.Enabled = false;
            comboBoxFasciaOraria.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[3].Value.ToString();
        }

        protected override async void btnRimuoviIstanza_Click(object sender, EventArgs e)
        {
            base.btnRimuoviIstanza_Click(sender, e);

            if (dataElencoIstanze.SelectedRows.Count > 0)
            {
                string idRimozione = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value.ToString();
                try
                {
                    if (MessageBox.Show("Sei sicuro di voler rimuovere la prenotazione con ID = "
                        + idRimozione + "?", "Rimuovere la prenotazione?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        string response = await apiClient.PostAsync<string>(Prenotazione.DeletePrenotazioneRoute, idRimozione);
                        Console.WriteLine($"DeletePrenotazione: {idRimozione}, {response}");

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
            dataElencoIstanze.Columns.Add("Nome Corso", "Nome Corso");
            dataElencoIstanze.Columns.Add("Data Prenotazione", "Data Prenotazione");
            dataElencoIstanze.Columns.Add("Fascia Oraria", "Fascia Oraria");


            btnAggiungiIstanza.Text = $"Aggiungi {TipoOggetto}";
            btnModificaIstanza.Text = $"Modifica {TipoOggetto}";
            btnRimuoviIstanza.Text = $"Rimuovi {TipoOggetto}";
        }

        protected override void dataElencoIstanze_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataElencoIstanze.Rows[e.RowIndex];
                string idPrenotazione = row.Cells["ID"].Value.ToString(); 
                string nomeCorso = row.Cells["Nome Corso"].Value.ToString();
                string messaggioTooltip = "ID Prenotazione: " + idPrenotazione + ", Lezione di " + nomeCorso;
                row.Cells[e.ColumnIndex].ToolTipText = messaggioTooltip;
            }
        }

        protected override void txtRicerca_TextChanged(object sender, EventArgs e)
        {
            FiltraDataGridView(dataElencoIstanze, txtRicerca.Text, "Nome Corso", "Data Prenotazione", "Fascia Oraria");
        }

        private async Task<Dictionary<int, string>> CaricaCorsi()
        {
            try
            {
                string corsiJson = await apiClient.GetAsync(Corso.GetCorsiRoute);
                Console.WriteLine($"GetCorsi: {corsiJson}");

                // Converti la risposta JSON in un oggetto Corso
                List<Corso> listaCorsi = JsonConvert.DeserializeObject<List<Corso>>(corsiJson);

                // Crea un dizionario con chiavi di tipo stringa e valori di tipo int
                Dictionary<int, string> opzioni = new Dictionary<int, string>();

                // Aggiungi i dati alla griglia
                foreach (var corso in listaCorsi)
                {
                    // Aggiungi il valore nel dizionario con l'ID come chiave
                    opzioni.Add(corso.ValoreId, corso.Nome);
                }

                // Popola il ComboBox con i nomi dei Corsi
                comboBoxCorso.Items.Clear();

                // Ottengo un array di stringhe con i valori del dizionario
                comboBoxCorso.Items.AddRange(opzioni.Values.ToArray());

                return opzioni;
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message + "\nChiudi e riapri il form Prenotazioni in modo da caricare correttamente i corsi.",
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Restituisci un Task con un dizionario vuoto come valore predefinito
                return await Task.FromResult(new Dictionary<int, string>());
            }
        }

        private async Task CaricaLezioni(int idCorso)
        {
            try
            {
                string lezioniJson = await apiClient.PostAsync(Lezione.GetLezioniRoute, idCorso.ToString());
                Console.WriteLine($"GetLezioni: {lezioniJson}");

                // Converti la risposta JSON in un oggetto Lezione
                List<Lezione> listaLezioni = JsonConvert.DeserializeObject<List<Lezione>>(lezioniJson);

                // Pulisce il dizionario esistente
                dizionarioLezioni.Clear();

                // Aggiungi i dati alla griglia
                foreach (var lezione in listaLezioni)
                {
                    string nomeGiornoInglese = lezione.Giorno;
                    string fasciaOraria = $"{lezione.OraInizio.ToString("HH:mm")}-{lezione.OraFine.ToString("HH:mm")}";
                    // Crea una lista di stringhe con il giorno della settimana e la fascia oraria
                    List<string> lezioneInfo = new List<string>
                    {
                        nomeGiornoInglese,
                        fasciaOraria
                    };
                    // Aggiungi il valore nel dizionario con l'ID come chiave
                    dizionarioLezioni.Add(lezione.ValoreId, lezioneInfo);
                }

            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message + "\nImpossibile caricare Data Prenotazione e Fascia Oraria.",
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<Corso> getCorso(int idCorso)
        {
            try
            {
                string corsoJson = await apiClient.PostAsync<string>(Corso.GetCorsoRoute, idCorso.ToString());
                Console.WriteLine($"GetCorso: {corsoJson}");

                // Gestisci il caso in cui non ci sia alcun corso
                if (string.IsNullOrEmpty(corsoJson))
                {
                    // Nessun corso trovato
                    return null;
                }

                // Converti la risposta JSON in un oggetto Corso
                Corso corso = JsonConvert.DeserializeObject<Corso>(corsoJson);
                return corso;
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Restituisci un valore null in caso di errore
                return null;
            }
        }

        private async Task<Lezione> getLezione(int idLezione)
        {
            try
            {
                string lezioneJson = await apiClient.PostAsync<string>(Lezione.GetLezioneRoute, idLezione.ToString());
                Console.WriteLine($"GetCorso: {lezioneJson}");

                // Gestisci il caso in cui non ci sia alcuna lezione
                if (string.IsNullOrEmpty(lezioneJson))
                {
                    // Nessuna lezione trovata
                    return null;
                }

                // Converti la risposta JSON in un oggetto Lezione
                Lezione lezione = JsonConvert.DeserializeObject<Lezione>(lezioneJson);
                return lezione;
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Restituisci un valore null in caso di errore
                return null;
            }
        }

        private async Task<Abbonamento> getAbbonamentoAttivo(int idAbbonamento)
        {
            try
            {
                string abbonamentoJson = await apiClient.PostAsync<string>(Abbonamento.GetAbbonamentoAttivoRoute, idAbbonamento.ToString());
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


        private async void comboBoxCorso_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica se il ComboBox ha un'opzione selezionata
            if (comboBoxCorso.SelectedIndex >= 0)
            {
                // Ottieni la stringa selezionata dal ComboBox
                string corsoSelezionato = comboBoxCorso.SelectedItem.ToString();

                // Cerca la chiave associata alla stringa selezionata nel ComboBox
                // Restituisce il primo elemento della sequenza che soddisfa la condizione 
                // o se l'elemento non è presente ritorna un valore predefinito
                int idCorso = dizionarioCorsi.FirstOrDefault(x => x.Value == corsoSelezionato).Key;

                Console.WriteLine($"Chiave associata a '{corsoSelezionato}': {idCorso}");

                await CaricaLezioni(idCorso);

                // Abilita il dataPrenotazione quando viene selezionata un'opzione nel ComboBox
                dataPrenotazione.Enabled = true;
            }
            else
            {
                // Disabilita il DateTimePicker se non c'è nessuna opzione selezionata nel ComboBox
                dataPrenotazione.Enabled = false;
            }
            if(btnAggiungi.Tag.ToString() == "aggiungi")
            {
                dataPrenotazione.Value = DateTime.Now;
                comboBoxFasciaOraria.SelectedIndex = -1;
                comboBoxFasciaOraria.Text = string.Empty;
            }
            else
            {
                comboBoxCorso.Enabled = false;
                dataPrenotazione.Enabled = false;
            }
        }

        protected override void btnAggiungiIstanza_Click(object sender, EventArgs e)
        {
            base.btnAggiungiIstanza_Click(sender, e);
            comboBoxCorso.Enabled = true;
        }

        private void dataPrenotazione_ValueChanged(object sender, EventArgs e)
        {
            // Gestisci la data selezionata nel DateTimePicker dataPrenotazione
            DateTime selectedDate = dataPrenotazione.Value;

            if (selectedDate >= DateTime.Today) // Controlla se è stata scelta una data valida
            {
                // Riabilita il controllo DropDown del DateTimePicker
                dataPrenotazione.Enabled = true;

                string giornoDaCercare = selectedDate.DayOfWeek.ToString();
                if (dizionarioLezioni.Any(entry => entry.Value.Contains(giornoDaCercare)))
                {
                    // Ottengo le fasce orarie permesse per il giorno selezionato
                    List<string> fasceOrariePermesse = new List<string>();

                    foreach (var entry in dizionarioLezioni)
                    {
                        // Ottengo le righe del giorno della settimana
                        if (entry.Value.Contains(giornoDaCercare))
                        {
                            // Salto il primo elemento della lista di stringhe (giorno della settimana)
                            List<string> fasceOrarie = entry.Value.Skip(1).ToList();

                            // Unisco tutte le fasce orarie in un'unica lista
                            fasceOrariePermesse.AddRange(fasceOrarie);
                        }
                    }
                    // Rimuoviamo eventuali duplicati e porto tutto ad una lista di stringhe se no avrei IEnumerable<string>
                    fasceOrariePermesse = fasceOrariePermesse.Distinct().ToList();

                    // Aggiorna la ComboBox con le fasce orarie permesse
                    comboBoxFasciaOraria.Items.Clear();
                    comboBoxFasciaOraria.Items.AddRange(fasceOrariePermesse.ToArray());

                    // Abilita la ComboBox delle fasce orarie
                    comboBoxFasciaOraria.Enabled = true;
                }
                else
                {
                    comboBoxFasciaOraria.Enabled = false;
                }
            }

            else
            {
                MessageBox.Show("Seleziona una data pari o successiva a quella di oggi.",
                        "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Disabilita la ComboBox delle fasce orarie
                comboBoxFasciaOraria.Enabled = false;
            }
            comboBoxFasciaOraria.SelectedIndex = -1;
            comboBoxFasciaOraria.Text = string.Empty;
        }
    }

}
