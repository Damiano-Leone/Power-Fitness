using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Power_Fitness.Dipendenti
{
    internal partial class Dipendenti : Power_Fitness.GestionePalestraBase.GestionePalestraBase
    {
        private string token;
        public Dipendenti(string token)
        {
            InitializeComponent();
            this.token = token;
            this.apiClient.SetAuthToken(token);
            TipoOggetto = "Dipendente";
            // Aggiungi le colonne alla DataGridView
            InizializzaDataGridView();
        }

        protected override void GestionePalestraBase_Load(object sender, EventArgs e)
        {
            base.GestionePalestraBase_Load(sender, e);
            btnAggiungiIstanza.Visible = false;
            btnModificaIstanza.Visible = false;
            btnRimuoviIstanza.Visible = false;
            btnPrenotazioni.Visible = false;
            btnGestisci.Text = "Approva Dipendente";
            btnGestisci.Alignment = ToolStripItemAlignment.Left;

            // Imposta il flag per indicare che l'evento Load è stato completato
            isLoadCompleted = true;

            // Imposta le larghezze delle colonne in base al numero di colonne del form
            SetColumnWidths();
        }

        protected override void caricaIcone()
        {
            base.caricaIcone();
            btnGestisci.Image = res.Properties.Resources.approva_icon;
        }

        protected override async void CaricaDatabase()
        {
            base.CaricaDatabase();

            try
            {
                string dipendentiJson = await apiClient.GetAsync(Dipendente.GetDipendentiRoute);
                Console.WriteLine($"GetDipendenti: {dipendentiJson}");

                // Converti la risposta JSON in un oggetto Utente
                List<Dipendente> listaDipendenti = JsonConvert.DeserializeObject<List<Dipendente>>(dipendentiJson);

                int numeroRighe = 0;

                // Aggiungi i dati alla griglia
                foreach (var dipendente in listaDipendenti)
                {
                    if (dipendente.Confirmed == "0")
                    {
                        dipendente.Confirmed = "Non Approvato"; 
                    }
                    else
                    {
                        dipendente.Confirmed = "Approvato";
                    }

                    dataElencoIstanze.Rows.Add(dipendente.ValoreId.ToString(),
                        dipendente.Mail, dipendente.Ruolo, dipendente.Confirmed);

                    // Incrementa il contatore delle righe
                    numeroRighe++;

                    Console.WriteLine($"ValoreId: {dipendente.ValoreId}");
                    Console.WriteLine($"Username: {dipendente.Mail}");
                    Console.WriteLine($"Ruolo: {dipendente.Ruolo}");
                    Console.WriteLine($"Stato di Validazione: {dipendente.Confirmed}");
                    Console.WriteLine("--------------------------------------");
                }
                // Visualizza il numero totale di righe
                lblNumeroTotale.Text = $"{numeroRighe}";
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori di connessione o elaborazione
                MessageBox.Show(ex.Message, "Al momento, non ci sono dipendenti registrati", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override async void EseguiAzioneSullaRigaSelezionata(string clickedBtn = "")
        {
            if (clickedBtn == "btnGestisci")
            {
                string id = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[0].Value.ToString();
                string confirmed = dataElencoIstanze.Rows[dataElencoIstanze.SelectedRows[0].Index].Cells[3].Value.ToString();
                if (confirmed == "Approvato")
                {
                    MessageBox.Show($"{TipoOggetto} già Approvato, Scegli un {TipoOggetto} in attesa di approvazione.",
                        $"{TipoOggetto} già Approvato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    try
                    {
                        if (MessageBox.Show("Sei sicuro di voler approvare il dipendente con ID = "
                            + id + "?", "Approvare il dipendente?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                            string response = await apiClient.PostAsync<string>(Dipendente.ValidateRoute, id);
                            Console.WriteLine($"Validate: {id}, {response}");

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
        }

        protected override void InizializzaDataGridView()
        {
            // Aggiungi le colonne alla DataGridView
            dataElencoIstanze.Columns.Add("ID", "ID");
            dataElencoIstanze.Columns.Add("Username", "Username");
            dataElencoIstanze.Columns.Add("Ruolo", "Ruolo");
            dataElencoIstanze.Columns.Add("Stato di Validazione", "Stato di Validazione");
        }

        protected override void dataElencoIstanze_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataElencoIstanze.Rows[e.RowIndex];
                string idDipendente = row.Cells["ID"].Value.ToString();
                string username = row.Cells["Username"].Value.ToString();
                string ruolo = row.Cells["Ruolo"].Value.ToString();
                string confermato = row.Cells["Stato di Validazione"].Value.ToString();
                string messaggioTooltip = "ID Dipendente: " + idDipendente + ", Username: " + username 
                    + ", Ruolo: " + ruolo + ", " + confermato;
                row.Cells[e.ColumnIndex].ToolTipText = messaggioTooltip;
            }
        }

        protected override void txtRicerca_TextChanged(object sender, EventArgs e)
        {
            FiltraDataGridView(dataElencoIstanze, txtRicerca.Text, "ID", "Username", "Ruolo", "Confermato");
        }
    }
}
