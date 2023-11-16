using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Power_Fitness.Abbonamenti
{
    internal partial class Abbonamenti : Power_Fitness.GestionePalestraBase.GestionePalestraBase
    {
        // utente[0] sarà il codice fiscale
        // utente[1] e utente[2] saranno rispettivamente nome e cognome
        // utente[3] email
        private List<string> utente;

        public Abbonamenti(string token, List <string> utente)
        {
            InitializeComponent();
            this.apiClient.SetAuthToken(token);
            TipoOggetto = "Abbonamento";
            this.utente = new List<string>(utente);
            this.Text = "Abbonamenti di: " + utente[1] + " " + utente[2];
            // Aggiungi le colonne alla DataGridView
            InizializzaDataGridView();
        }

        protected override void GestionePalestraBase_Load(object sender, EventArgs e)
        {
            btnAggiungiIstanza.Visible = false;
            txtDataInizio.Value = DateTime.Today;

            base.GestionePalestraBase_Load(sender, e);

            btnGestisci.Visible = false;
            btnPrenotazioni.Visible = false;

            // Visualizza Nome e Cognome dell'utente:
            lblUtente.Text = utente[1] + " " + utente[2];


            CenterLabelHorizontally(lblUtente, pnlAbbonamenti);
            CenterLabelHorizontally(userBox, pnlAbbonamenti);
            CenterLabelHorizontally(lblStatoAbbonamento, pnlAbbonamenti);
            CenterLabelHorizontally(btnAggiungiAbb, pnlAbbonamenti);
            
            

            // Imposta il flag per indicare che l'evento Load è stato completato
            isLoadCompleted = true;

            // Imposta le larghezze delle colonne in base al numero di colonne del form
            SetColumnWidths();
        }

        protected override void caricaIcone()
        {
            base.caricaIcone();
            userBox.Image = res.Properties.Resources.user_icon_orange2;
            userBox.SizeMode = PictureBoxSizeMode.Zoom;
            btnRefresh.BackgroundImage = res.Properties.Resources.rotate_icon;
            btnRefresh.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void aggiornaStatoAbbonamento()
        {
            DateTime? dataInizioAbbonamento = null;
            DateTime? dataFineAbbonamento = null;
            string tipoAbbonamento = string.Empty;
            int verifica = VerificaStatoAbbonamento(dataElencoIstanze, DateTime.Now, 
                out dataInizioAbbonamento, out dataFineAbbonamento, out tipoAbbonamento);
            if (verifica == 0)
            {
                lblStatoAbbonamento.BackColor = Color.Green;
                lblStatoAbbonamento.ForeColor = Color.White;
                lblStatoAbbonamento.Text = "Abbonamento: Attivo\n" + "Durata:"+ tipoAbbonamento 
                    + "\nAttivo dal: " + dataInizioAbbonamento.Value.ToShortDateString() + " al: " 
                    + dataFineAbbonamento.Value.ToShortDateString();
                lblAggiungiAbbonamento.Text = "Rinnova Abbonamento";
                btnAggiungiAbb.Text = "Rinnova";
                btnAggiungiAbb.Tag = "rinnova";
            }
            else if (verifica == 1)
            {
                lblStatoAbbonamento.BackColor = Color.Yellow;
                lblStatoAbbonamento.ForeColor = Color.Black;
                lblStatoAbbonamento.Text = "Abbonamento: Non Attivo\n" + "Durata:" + tipoAbbonamento
                    + "\nAttivo dal: " + dataInizioAbbonamento.Value.ToShortDateString() + " al: "
                    + dataFineAbbonamento.Value.ToShortDateString();
                lblAggiungiAbbonamento.Text = "Rinnova Abbonamento";
                btnAggiungiAbb.Text = "Rinnova";
                btnAggiungiAbb.Tag = "rinnova";
            }
            else
            {
                lblStatoAbbonamento.BackColor = Color.Red;
                lblStatoAbbonamento.ForeColor = Color.White;
                lblStatoAbbonamento.Text = "Abbonamento: Non Attivo";
                //lblStatoAbbonamento.Text = "Abbonamento: Attivo\nDurata: 1 Mese\nAttivo dal: 12/09/2023 al: 12/10/2023";
                lblAggiungiAbbonamento.Text = "Crea Abbonamento";
                btnAggiungiAbb.Text = "Crea";
                btnAggiungiAbb.Tag = "crea";
            }
            CenterLabelHorizontally(lblStatoAbbonamento, pnlAbbonamenti);
            CenterLabelHorizontally(lblAggiungiAbbonamento, pnlAbbonamenti);
        }

        private async void btnAggiungiAbb_Click(object sender, EventArgs e)
        {
            int result = DateTime.Compare(txtDataInizio.Value, DateTime.Today);
            if (result < 0)
            {
                MessageBox.Show("Impostare una Data di Inizio che sia pari o successiva a quella di oggi");
                return;
            }

            // Verifica se è stato selezionato un abbonamento
            if (comboBoxAbbonamento.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona un Tipo Abbonamento");
                return;
            }

            if (ControlloRighe(dataElencoIstanze, Color.Yellow))
            {
                MessageBox.Show("Non è possibile rinnovare l'abbonamento poiché è già stato rinnovato in precedenza",
                    $"Imposssibile rinnovare l'abbonamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime? dataInizioAbbonamento = null;
            DateTime? dataFineAbbonamento = null;
            string tipoAbbonamento = string.Empty;
            int verificaOperazione = VerificaStatoAbbonamento(dataElencoIstanze, txtDataInizio.Value,
                out dataInizioAbbonamento, out dataFineAbbonamento, out tipoAbbonamento);
            if (verificaOperazione == 0)
            {
                MessageBox.Show("Non è possibile rinnovare l'abbonamento, poiché esiste già un abbonamento dal: "
                    + dataInizioAbbonamento.Value.ToShortDateString() + " al: " + dataFineAbbonamento.Value.ToShortDateString()
                    + ".\nPer favore, seleziona un'altra data.",
                     $"Imposssibile rinnovare l'abbonamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (verificaOperazione == 1)
            {
                MessageBox.Show("Non è possibile rinnovare l'abbonamento poiché è già stato rinnovato in precedenza ed è attivo dal: "
                    + dataInizioAbbonamento.Value.ToShortDateString() + " al: " + dataFineAbbonamento.Value.ToShortDateString()
                    + ".\nSe desideri effettuare un nuovo rinnovo con la data specificata, modifica la data di rinnovo precedentemente impostata.",
                    $"Imposssibile rinnovare l'abbonamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Abbonamento abbonamento = new Abbonamento(utente[0], txtDataInizio.Value,
                    comboBoxAbbonamento.Text);
                //btnAggiungiAbb.Tag = "crea";
                if (btnAggiungiAbb.Tag != null && btnAggiungiAbb.Tag.ToString() == "crea")
                {
                    string response = await apiClient.PostAsync<Abbonamento>(Abbonamento.AddAbbonamentoRoute, abbonamento);
                    Console.WriteLine($"AddAbbonamento: {comboBoxAbbonamento.Text}, {response}");
                }
                else
                {
                    string response = await apiClient.PostAsync<Abbonamento>(Abbonamento.RinnovaAbbonamentoRoute, abbonamento);
                    Console.WriteLine($"RinnovaAbbonamento: {comboBoxAbbonamento.Text}, {response}");
                }

                    
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Per non far chiudere il pannello azzerando i dati immessi mettiamo un return
                return;
            }

            btnAggiungiAbb.Tag = "";

            // Aggiorna la vista del Database
            CaricaDatabase();
            refreshPnlAbbonamenti();

            // Coloro le righe degli abbonamenti
            ColoraRigheDataGridView(dataElencoIstanze, DateTime.Now);

            // Controllo se l'utente ha un abbonamento attivo
            aggiornaStatoAbbonamento();
        }


        // Tasto Modifica, dentro btnModificaIstanza
        protected override async void btnAggiungi_Click(object sender, EventArgs e)
        {
            int result = DateTime.Compare(txtDataInizioM.Value, DateTime.Today);
            if (result < 0)
            {
                MessageBox.Show("Impostare una Data di Inizio che sia pari o successiva a quella di oggi");
                return;
            }

            // Verifica se è stato selezionato un abbonamento
            if (comboBoxAbbonamentoM.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona un Tipo Abbonamento");
                return;
            }


            // Verifico se la riga selezionata è un rinnovo
            if (ControlloRiga(dataElencoIstanze.SelectedRows[0], Color.Yellow))
            {
                DateTime? dataInizioAbbonamento = null;
                DateTime? dataFineAbbonamento = null;
                // Verifico se esiste già un eventuale abbonamento attivo
                if (ControlloRighe(dataElencoIstanze, Color.Green, out dataInizioAbbonamento, out dataFineAbbonamento))
                {
                    // Consento la modifica solo se la data di inizio desiderata è successiva alla data di scadenza dell'abbonamento attivo
                    if (txtDataInizioM.Value <= dataFineAbbonamento)
                    {
                        MessageBox.Show("Non è possibile modificare l'abbonamento, poiché esiste già un abbonamento attivo dal: "
                        + dataInizioAbbonamento.Value.ToShortDateString() + "al: " + dataFineAbbonamento.Value.ToShortDateString()
                        + ".\nPer favore, seleziona un'altra data.",
                        $"Imposssibile modificare l'abbonamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            // Se l'abbonamento non ha ancora un rinnovo ed è attivo da meno di 10 giorni posso modificarlo.

            try
            {
                int id = Convert.ToInt32(dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value);
                Abbonamento abbonamento = new Abbonamento(id, utente[0], txtDataInizioM.Value, comboBoxAbbonamentoM.Text);

                string response = await apiClient.PostAsync<Abbonamento>(Abbonamento.UpdateAbbonamentoRoute, abbonamento);
                Console.WriteLine($"UpdateAbbonamento: {comboBoxAbbonamento.Text}, {response}");
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Per non far chiudere il pannello azzerando i dati immessi mettiamo un return
                return;
            }

            base.btnAggiungi_Click(sender, e);

            ColoraRigheDataGridView(dataElencoIstanze, DateTime.Now);

            // Controllo se l'utente ha un abbonamento attivo
            aggiornaStatoAbbonamento();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshPnlAbbonamenti();
        }

        private void refreshPnlAbbonamenti()
        {
            txtDataInizio.Value = DateTime.Today;
            comboBoxAbbonamento.SelectedIndex = -1;
        }

        protected override void ResetPanel()
        {
            txtIdAbbonamento.Text = "";
            txtDataInizioM.Value = DateTime.Today;
            comboBoxAbbonamentoM.SelectedIndex = -1;
            base.ResetPanel();
            pnlAbbonamenti.Visible = true;
        }

        protected override async void CaricaDatabase()
        {
            base.CaricaDatabase();

            try
            {

                string abbonamentiJson = await apiClient.PostAsync<string>(Abbonamento.GetAbbonamentiRoute, utente[0]);
                Console.WriteLine($"GetAbbonamenti: {abbonamentiJson}");

                // Converti la risposta JSON in un oggetto Utente
                List<Abbonamento> listaAbbonamenti = JsonConvert.DeserializeObject<List<Abbonamento>>(abbonamentiJson);

                int numeroRighe = 0;

                // Aggiungi i dati alla griglia
                foreach (var abbonamento in listaAbbonamenti)
                {
                    dataElencoIstanze.Rows.Add(abbonamento.ValoreId.ToString(),
                        abbonamento.DataIscrizione.ToString("dd MMMM yyyy"), abbonamento.DurataAbbonamento);

                    // Incrementa il contatore delle righe
                    numeroRighe++;

                    Console.WriteLine($"ValoreId: {abbonamento.ValoreId}");
                    Console.WriteLine($"CodiceFiscale: {abbonamento.CodiceFiscale}");
                    Console.WriteLine($"DataIscrizione: {abbonamento.DataIscrizione.ToString("dd MMMM yyyy")}");
                    Console.WriteLine($"DurataAbbonamento: {abbonamento.DurataAbbonamento}");
                    Console.WriteLine("--------------------------------------");
                }
                // Visualizza il numero totale di righe
                lblNumeroTotale.Text = $"{numeroRighe}";
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Al momento, nessun abbonamento è registrato per questo utente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ColoraRigheDataGridView(dataElencoIstanze, DateTime.Now);

            // Controllo se l'utente ha un abbonamento attivo
            aggiornaStatoAbbonamento();
        }


        protected override void CaricaDatiIstanzaSelezionata()
        {
            // Modifica campi
            txtIdAbbonamento.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value.ToString();
            txtDataInizioM.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[1].Value.ToString();
            comboBoxAbbonamentoM.Text = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[2].Value.ToString();

        }


        protected override async void btnRimuoviIstanza_Click(object sender, EventArgs e)
        {
            base.btnRimuoviIstanza_Click(sender, e);
            if (dataElencoIstanze.SelectedRows.Count > 0)
            {
                if (ControlloRiga(dataElencoIstanze.SelectedRows[0], Color.Red))
                {
                    // l'abbonamento è scaduto e non posso rimuoverlo perché voglio tenerne traccia
                    MessageBox.Show("L'abbonamento è scaduto e non può essere rimosso.",
                                $"Impossibile rimuovere l'abbonamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            
                string idRimozione = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value.ToString();
                try
                {
                    if (MessageBox.Show("Sei sicuro di voler rimuovere l'abbonamento con ID = "
                        + idRimozione + "?", "Rimuovere l'abbonamento?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        string response = await apiClient.PostAsync<string>(Abbonamento.DeleteAbbonamentoRoute, idRimozione);
                        Console.WriteLine($"DeleteAbbonamento: {idRimozione}, {response}");

                        // Aggiorna la vista del Database
                        CaricaDatabase();

                        // Controllo se l'utente ha un abbonamento attivo
                        aggiornaStatoAbbonamento();
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


        protected override void btnModificaIstanza_Click(object sender, EventArgs e)
        {
            if (pnlAggiungi.Visible == false)
            {
                // Verifica se ci sono righe selezionate nella datagridview
                if (dataElencoIstanze.SelectedRows.Count > 0)
                {
                    // Verifico se la riga selezionata è un abbonamento in corso di validità
                    if(ControlloRiga(dataElencoIstanze.SelectedRows[0], Color.Green))
                    {
                        DateTime? dataInizioAbbonamento = null;
                        DateTime? dataFineAbbonamento = null;
                        // Valuto se esiste già un eventuale rinnovo
                        if (ControlloRighe(dataElencoIstanze, Color.Yellow, 
                            out dataInizioAbbonamento, out dataFineAbbonamento))
                        {
                            // Non posso modificare l'abbonamento perché è già presente un rinnovo addirittura
                            MessageBox.Show("L'abbonamento non può essere modificato poiché è già stato effettuato un rinnovo dal: " 
                                + dataInizioAbbonamento.Value.ToShortDateString() + "al: " + dataFineAbbonamento.Value.ToShortDateString() + ".",
                                $"Impossibile modificare l'abbonamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        
                    }

                    // Se un abbonamento è scaduto non posso più modificarlo
                    else if(ControlloRiga(dataElencoIstanze.SelectedRows[0], Color.Red))
                    {
                        MessageBox.Show("La modifica non è consentita poiché l'abbonamento è già scaduto.",
                            $"Impossibile modificare l'abbonamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    // Se invece ho un rinnovo posso sempre modificarlo
                    btnAggiungi.Text = "Modifica";
                    pnlAbbonamenti.Visible = false;
                    pnlAggiungi.Visible = true;
                    pnlAggiungi.Dock = DockStyle.Left;
                    btnAggiungi.Tag = "modifica";
                    CaricaDatiIstanzaSelezionata();
                }
                else
                {
                    MessageBox.Show($"Seleziona un {TipoOggetto} dalla lista prima di modificare.",
                        $"Nessun {TipoOggetto} Selezionato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                pnlAggiungi.Visible = false;
                pnlAbbonamenti.Visible = true;
            }
        }


        protected override void InizializzaDataGridView()
        {
            // Aggiungi le colonne alla DataGridView
            dataElencoIstanze.Columns.Add("ID Abbonamento", "ID Abbonamento");
            dataElencoIstanze.Columns.Add("Data di Inizio", "Data di Inizio");
            dataElencoIstanze.Columns.Add("Tipo di Abbonamento", "Tipo di Abbonamento");

            btnAggiungiIstanza.Text = $"Aggiungi {TipoOggetto}";
            btnModificaIstanza.Text = $"Modifica {TipoOggetto}";
            btnRimuoviIstanza.Text = $"Rimuovi {TipoOggetto}";
        }


        protected override void dataElencoIstanze_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataElencoIstanze.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string messaggioTooltip = "ID Abbonamento: " + utente[0] + ", Abbonamento di " + utente[1] + " " + utente[2];
                cell.ToolTipText = messaggioTooltip;
            }
        }


        protected override void txtRicerca_TextChanged(object sender, EventArgs e)
        {
            FiltraDataGridView(dataElencoIstanze, txtRicerca.Text, "Data di Inizio", "Tipo di Abbonamento");
        }


        private void ColoraRigheDataGridView(DataGridView dataGridView, DateTime dataDaVerificare)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Assicurati che la DataGridView abbia colonne per data di iscrizione e tipo di abbonamento
                if (row.Cells["Data di Inizio"].Value != null && row.Cells["Tipo di Abbonamento"].Value != null)
                {
                    // Estrai la data di inizio e il tipo di abbonamento dalla riga corrente
                    DateTime dataInizio = DateTime.Parse(row.Cells["Data di Inizio"].Value.ToString());
                    string tipoAbbonamentoRiga = row.Cells["Tipo di Abbonamento"].Value.ToString();

                    // Calcola la data di fine in base al tipo di abbonamento
                    DateTime dataFine;
                    dataFine = Abbonamento.calcoloDataFineAbbonamento(dataInizio, tipoAbbonamentoRiga);

                    // Controlla se la data inserita è compresa tra data di inizio e data di fine
                    bool match = dataDaVerificare >= dataInizio && dataDaVerificare <= dataFine;

                    if (match)
                    {
                        // Se è un match, colora la riga di verde
                        row.DefaultCellStyle.BackColor = Color.Green;
                    }
                    else if (dataInizio > dataDaVerificare)
                    {
                        // Se dataInizio è futuro rispetto a dataDaVerificare, colora la riga di giallo
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        // Se non è un match né futuro, colora la riga di rosso
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
        

        private bool ControlloRiga(DataGridViewRow row, Color coloreDaCercare)
        {
            // Ottieni il colore di sfondo predefinito della riga
            Color rowBackgroundColor = row.DefaultCellStyle.BackColor;

            // Controlla se il colore di sfondo è uguale a quello cercato
            return rowBackgroundColor == coloreDaCercare;
        }


        private bool ControlloRighe(DataGridView dataGridView, Color coloreDaCercare)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Ottieni il colore di sfondo predefinito della riga
                Color rowBackgroundColor = row.DefaultCellStyle.BackColor;

                // Controlla se il colore di sfondo è giallo
                if (rowBackgroundColor == coloreDaCercare)
                {
                    return true;
                }
            }
            return false;
        }


        private bool ControlloRighe(DataGridView dataGridView, Color coloreDaCercare, 
            out DateTime? dataInizioAbbonamento, out DateTime? dataFineAbbonamento)
        {
            dataInizioAbbonamento = null;
            dataFineAbbonamento = null;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Ottieni il colore di sfondo predefinito della riga
                Color rowBackgroundColor = row.DefaultCellStyle.BackColor;

                // Controlla se il colore di sfondo è giallo
                if (rowBackgroundColor == coloreDaCercare)
                {
                    // Ci assicuriamo che la DataGridView abbia colonne per data di iscrizione e tipo di abbonamento
                    if (row.Cells["Data di Inizio"].Value != null && row.Cells["Tipo di Abbonamento"].Value != null)
                    {
                        // Estrae la data di inizio e il tipo di abbonamento dalla riga corrente
                        DateTime dataInizio = DateTime.Parse(row.Cells["Data di Inizio"].Value.ToString());
                        dataInizioAbbonamento = dataInizio;
                        string tipoAbbonamentoRiga = row.Cells["Tipo di Abbonamento"].Value.ToString();
                        dataFineAbbonamento = Abbonamento.calcoloDataFineAbbonamento(dataInizio, tipoAbbonamentoRiga);
                        // Calcola la data di fine in base al tipo di abbonamento
                        
                        return true;
                    }
                }
            }
            return false;
        }

        // DateTime? è un tipo di valore nullable, che consente di rappresentare DateTime sia come data che null
        private int VerificaStatoAbbonamento(DataGridView dataGridView, DateTime dataDaVerificare,
            out DateTime? dataInizioAbbonamento, out DateTime? dataFineAbbonamento, out string tipoAbbonamento)
        {
            dataInizioAbbonamento = null;
            dataFineAbbonamento = null;
            tipoAbbonamento = string.Empty;

            // Itera su tutte le righe della DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Ci assicuriamo che la DataGridView abbia colonne per data di iscrizione e tipo di abbonamento
                if (row.Cells["Data di Inizio"].Value != null && row.Cells["Tipo di Abbonamento"].Value != null)
                {
                    // Estrae la data di inizio e il tipo di abbonamento dalla riga corrente
                    DateTime dataInizio = DateTime.Parse(row.Cells["Data di Inizio"].Value.ToString());
                    string tipoAbbonamentoRiga = row.Cells["Tipo di Abbonamento"].Value.ToString();

                    // Calcola la data di fine in base al tipo di abbonamento
                    DateTime dataFine = Abbonamento.calcoloDataFineAbbonamento(dataInizio, tipoAbbonamentoRiga);
                   
                    // Controlla se la data inserita è compresa tra data di inizio e data di fine
                    bool match = dataDaVerificare >= dataInizio && dataDaVerificare <= dataFine;

                    if (match)
                    {
                        // Almeno una corrispondenza è stata trovata
                        dataInizioAbbonamento = dataInizio;
                        dataFineAbbonamento = dataFine;
                        tipoAbbonamento = tipoAbbonamentoRiga;
                        return 0;
                    }
                    else if (dataInizio > dataDaVerificare)
                    {
                        // La data di inizio è futura rispetto a dataDaVerificare
                        dataInizioAbbonamento = dataInizio;
                        dataFineAbbonamento = dataFine;
                        tipoAbbonamento = tipoAbbonamentoRiga;
                        return 1;
                    }
                }
            }
            return -1;
        }

        protected override void FiltraDataGridView(DataGridView dataGridView, string testoInserito, params string[] colonne)
        {
            // Converte la data inserita in un oggetto DateTime per il confronto
            if (!DateTime.TryParse(testoInserito, out DateTime dataDaCercare))
            {
                // Ricerca il testo in modo classico
                base.FiltraDataGridView(dataGridView, testoInserito, colonne);
                return;
            }

            // Itera su tutte le righe della DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Assicurati che la DataGridView abbia colonne per data di iscrizione e tipo di abbonamento
                if (row.Cells["Data di Inizio"].Value != null && row.Cells["Tipo di Abbonamento"].Value != null)
                {
                    // Estrai la data di inizio e il tipo di abbonamento dalla riga corrente
                    DateTime dataInizio = DateTime.Parse(row.Cells["Data di Inizio"].Value.ToString());
                    string tipoAbbonamentoRiga = row.Cells["Tipo di Abbonamento"].Value.ToString();

                    // Calcola la data di fine in base al tipo di abbonamento
                    DateTime dataFine;
                    dataFine = Abbonamento.calcoloDataFineAbbonamento(dataInizio, tipoAbbonamentoRiga);

                    // Controlla se la data inserita è compresa tra data di inizio e data di fine
                    bool match = dataDaCercare >= dataInizio && dataDaCercare <= dataFine;

                    // Imposta la visibilità della riga in base al risultato del filtro
                    row.Visible = match;
                }
            }
        }


        private void txtIdAbbonamento_Enter(object sender, EventArgs e)
        {
            // Evita che la TextBox ottenga il focus quando viene selezionata.
            this.ActiveControl = null;
        }

        
    }
}
