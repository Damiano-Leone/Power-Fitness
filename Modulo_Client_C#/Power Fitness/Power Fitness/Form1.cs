using Power_Fitness.Dipendenti;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Power_Fitness
{
    internal partial class MainForm : Form
    {
        private string token;
        private string username;

        public MainForm(string token, string username)
        {
            InitializeComponent();
            this.token = token;
            this.username = username;
            if (username != "admin")
            {
                btnDipendenti.Visible = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // CARICA LE ICONE
            carica_icone();

            // CARICA ORARIO
            carica_ora();
            timerOrario.Enabled = true;
            timerOrario.Start();
        }

        private void carica_icone()
        {
            btnDipendenti.Image = res.Properties.Resources.role_black;
            btnUtenti.Image = res.Properties.Resources.members_icon;
            btnCorsi.Image = res.Properties.Resources.whistle;
            btnAbbonamenti.Image = res.Properties.Resources.chart;
            btnImpostazioni.Image = res.Properties.Resources.info;
            btnLogout.Image = res.Properties.Resources.logout;
            // CARICA ICONA ORARIO STATUS
            lblOrario.Image = res.Properties.Resources.ora;
            lblOrario.ImageAlign = ContentAlignment.MiddleRight;
        }

        private void carica_ora()
        {
            lblOrario.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void timerOrario_Tick(object sender, EventArgs e)
        {
            carica_ora();
        }

        private void lblOrario_Click(object sender, EventArgs e)
        {
            if(timerOrario.Enabled == true)
            {
                timerOrario.Stop();
            }
            else
            {
                timerOrario.Start();
            }
        }

        private void btnCorsi_Click(object sender, EventArgs e)
        {
            Corsi.Corsi FormCorsi = new Corsi.Corsi(token);
            FormCorsi.Show();
        }

        private void btnUtenti_Click(object sender, EventArgs e)
        {
            Utenti.Utenti FormUtenti = new Utenti.Utenti(token);
            FormUtenti.Show();
        }

        private void btnDipendenti_Click(object sender, EventArgs e)
        {
            Dipendenti.Dipendenti FormDipendenti = new Dipendenti.Dipendenti(token);
            FormDipendenti.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void btnAbbonamenti_Click(object sender, EventArgs e)
        {
            Statistica.StatisticaAbbonamenti formStatisticaAbbonamenti = new Statistica.StatisticaAbbonamenti();
            formStatisticaAbbonamenti.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void btnImpostazioni_Click(object sender, EventArgs e)
        {
            new frmInfo.frmInfo().Show();
        }
    }
}
