using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Power_Fitness.CrowApi;

namespace Power_Fitness.GestionePalestraBase
{
    internal partial class GestionePalestraBase : Form
    {
        protected CrowApiClient apiClient;  // Dichiarazione della variabile di istanza
        protected string TipoOggetto { get; set; } = "Oggetto";
        protected bool isLoadCompleted = false; // Flag per indicare se l'evento Load è stato completato

        public GestionePalestraBase()
        {
            InitializeComponent();
            apiClient = new CrowApiClient();
        }

        protected virtual void GestionePalestraBase_Load(object sender, EventArgs e)
        {
            // Nascondiamo il panel Aggiungi Corso di default
            pnlAggiungi.Visible = false;
            //pnlAggiungi.Dock = DockStyle.Left;
            // Riempiamo l'intero form con il panel Corsi

            pnl.Dock = DockStyle.Fill;

            // Imposta il testo suggerito iniziale
            txtRicerca.Text = $"Cerca {TipoOggetto}";

            // Imposta il colore del testo suggerito in grigio
            txtRicerca.ForeColor = Color.Gray;

            // Carica icone
            caricaIcone();

            // Carica i dati dei corsi dal database
            CaricaDatabase();
        }

        protected virtual void btnAggiungiIstanza_Click(object sender, EventArgs e)
        {
            if (pnlAggiungi.Visible == false)
            {
                // btnAggiungi.Tag != null serve la prima volta che viene fatto il click sul bottone
                // per evitare che il metodo ToString() sia chiamato su btnAggiungi.Tag che vale null la prima volta
                // ToString() serve per dare la certezza al compilatore che il tag sia effettivamente una stringa
                if (btnAggiungi.Tag != null && btnAggiungi.Tag.ToString() == "modifica")
                {
                    ResetPanel();
                }
                btnAggiungi.Text = "Aggiungi";
                pnlAggiungi.Visible = true;
                pnlAggiungi.Dock = DockStyle.Left;
                btnAggiungi.Tag = "aggiungi";
            }
            else
            {
                pnlAggiungi.Visible = false;
            }
        }

        protected virtual void btnAggiungi_Click(object sender, EventArgs e)
        {
            // Una volta eseguita l'aggiunta dell'istanza viene eseguita la restante parte che segue:

            // btnAggiungi.Tag = "";
            // Serve per non fare rieseguire il codice presente in dataElenco_SelectionChanged
            // nella funzione caricaDatiIstanzaSelezionata();
            // altrimenti nel caricare nel db crasha perché non riesce a trovare la riga selezionata a causa
            // della funzione clear() posta all'inizio che va a deselezionare la riga di interesse da modificare,
            // andando quindi ad inserire un indice nnn valido all'interno del db.
            btnAggiungi.Tag = "";

            // Aggiorna la vista del Database
            CaricaDatabase();

            // Nascondi Panel
            ResetPanel();
        }

        protected virtual void ResetPanel()
        {
            pnlAggiungi.Visible = false;
        }

        protected void btnAnnulla_Click(object sender, EventArgs e)
        {
            ResetPanel();
        }

        protected virtual void CaricaDatabase()
        {
            // Serve per aggiornare la vista del database in maniera corretta senza duplicati,
            // quando si fa click su Aggiungi
            dataElencoIstanze.Rows.Clear();
        }

        protected virtual void btnModificaIstanza_Click(object sender, EventArgs e)
        {
            if (pnlAggiungi.Visible == false)
            {
                // Verifica se ci sono righe selezionate nella datagridview
                if (dataElencoIstanze.SelectedRows.Count > 0)
                {
                    btnAggiungi.Text = "Modifica";
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
            }
        }

        protected virtual void btnRimuoviIstanza_Click(object sender, EventArgs e)
        {
            if (dataElencoIstanze.SelectedRows.Count == 0)
            {
                MessageBox.Show($"Seleziona un {TipoOggetto} dalla lista prima di rimuovere.",
                    $"Nessun {TipoOggetto} Selezionato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ResetPanel();
        }

        protected void dataElencoIstanze_SelectionChanged(object sender, EventArgs e)
        {
            if (pnlAggiungi.Visible == true)
            {
                if (btnAggiungi.Tag.ToString() == "modifica")
                {
                    CaricaDatiIstanzaSelezionata();
                }
            }
        }

        protected virtual void caricaIcone()
        {
            this.Icon = res.Properties.Resources.Power_Fitness_logo_design;
            btnAggiungiIstanza.Image = res.Properties.Resources.add_icon;
            btnModificaIstanza.Image = res.Properties.Resources.edit_icon;
            btnRimuoviIstanza.Image = res.Properties.Resources.remove_icon;
        }

        protected void SetColumnWidths()
        {
            dataElencoIstanze.RowHeadersWidth = 25;
            // Calcola le nuove larghezze delle colonne in base alla larghezza attuale della DataGridView
            int totalWidth = dataElencoIstanze.Width - dataElencoIstanze.RowHeadersWidth;
            int numColumns = dataElencoIstanze.Columns.Count;
            int columnWidth = totalWidth / numColumns;

            // Imposta le nuove larghezze delle colonne
            for (int i = 0; i < numColumns; i++)
            {
                dataElencoIstanze.Columns[i].Width = columnWidth;
            }
        }

        protected void GestionePalestraBase_SizeChanged(object sender, EventArgs e)
        {
            // Chiamata SetColumnWidths solo se Load è completato
            if (isLoadCompleted)
            {
                SetColumnWidths();
            }
        }

        protected void dataElencoIstanze_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se il doppio click è avvenuto su una riga valida e non sull'intestazione delle colonne
            if (e.RowIndex >= 0)
            {
                EseguiAzioneSullaRigaSelezionata("btnGestisci");
            }
        }

        protected void dataElencoIstanze_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica se si tratta del tasto Invio
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Impedisce l'azione predefinita del tasto Invio
                EseguiAzioneSullaRigaSelezionata("btnGestisci");
            }
        }

        private void btnGestisci_Click(object sender, EventArgs e)
        {
            if (dataElencoIstanze.SelectedRows.Count > 0)
            {
                EseguiAzioneSullaRigaSelezionata("btnGestisci");
            }
            else
            {
                MessageBox.Show($"Seleziona un {TipoOggetto} dalla lista prima di procedere.",
                    $"Nessun {TipoOggetto} Selezionato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrenotazioni_Click(object sender, EventArgs e)
        {
            if (dataElencoIstanze.SelectedRows.Count > 0)
            {
                EseguiAzioneSullaRigaSelezionata("btnPrenotazioni");
            }
            else
            {
                MessageBox.Show($"Seleziona un {TipoOggetto} dalla lista prima di procedere.",
                    $"Nessun {TipoOggetto} Selezionato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtRicerca_Enter(object sender, EventArgs e)
        {
            // Rimuovi il testo suggerito quando la TextBox ottiene il focus
            if (txtRicerca.Text == $"Cerca {TipoOggetto}")
            {
                txtRicerca.Text = "";
                txtRicerca.ForeColor = SystemColors.WindowText; // Ripristina il colore del testo normale
            }
        }

        private void txtRicerca_Leave(object sender, EventArgs e)
        {
            // Ripristina il testo suggerito quando la TextBox perde il focus e non ha testo
            if (string.IsNullOrWhiteSpace(txtRicerca.Text))
            {
                txtRicerca.Text = $"Cerca {TipoOggetto}";
                txtRicerca.ForeColor = Color.Gray;

                // Se il campo di ricerca è vuoto, mostra tutte le righe
                foreach (DataGridViewRow row in dataElencoIstanze.Rows)
                {
                    row.Visible = true;
                }
            }
        }

        protected virtual void FiltraDataGridView(DataGridView dataGridView, string testo, params string[] colonne)
        {
            // Convertiamo il testo inserito in minuscolo 
            // in modo da ricercare senza distinzione tra maiuscole e minuscole
            string searchText = testo.ToLower();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                bool match = false;

                foreach (var colonna in colonne)
                {
                    // Ci assicuriamo che la DataGridView abbia una colonna in cui cercare
                    if (row.Cells[colonna].Value != null)
                    {
                        // Confronta il valore nella colonna con il testo inserito nella barra di ricerca
                        string cellValue = row.Cells[colonna].Value.ToString().ToLower();
                        match |= cellValue.StartsWith(searchText);
                    }
                }

                // Imposta la visibilità della riga in base al risultato del filtro
                row.Visible = match;
            }
        }

        protected List<string> OtteniContenutoCelleSelezionate(params string[] colonne)
        {
            List<string> contenutoColonne = new List<string>();

            if (dataElencoIstanze.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataElencoIstanze.SelectedRows[0];

                foreach (string colonna in colonne)
                {
                    if (selectedRow.Cells[colonna].Value != null)
                    {
                        contenutoColonne.Add(selectedRow.Cells[colonna].Value.ToString());
                    }
                    else
                    {
                        // Aggiungi una stringa vuota se la cella è nulla.
                        contenutoColonne.Add(string.Empty);
                    }
                }
            }

            return contenutoColonne;
        }


        protected void CenterLabelHorizontally(Control control, Panel panel)
        {
            // Calcola la posizione orizzontale per centrare l'elemento di controllo
            int centerX = (panel.Width - control.Width) / 2;

            // Imposta la posizione X dell'elemento
            control.Location = new Point(centerX, control.Location.Y);
        }


        private void btnAggiungiIstanza_MouseEnter(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Hand;
        }

        private void btnAggiungiIstanza_MouseLeave(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Default;
        }

        private void btnModificaIstanza_MouseEnter(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Hand;
        }

        private void btnModificaIstanza_MouseLeave(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Default;
        }

        private void btnRimuoviIstanza_MouseEnter(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Hand;
        }

        private void btnRimuoviIstanza_MouseLeave(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Default;
        }


        protected virtual void EseguiAzioneSullaRigaSelezionata(string clickedBtn = "")
        {

        }

        protected virtual void InizializzaDataGridView()
        {

        }

        protected virtual void CaricaDatiIstanzaSelezionata()
        {

        }

        protected virtual void dataElencoIstanze_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        protected virtual void txtRicerca_TextChanged(object sender, EventArgs e)
        {
            
        }

    }

}
