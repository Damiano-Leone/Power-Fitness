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

namespace Power_Fitness.Dipendenti
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }
        private void frmRegister_Load(object sender, EventArgs e)
        {
            CenterLabelHorizontally(pictureBox1);
            CenterLabelHorizontally(label1);
            CenterLabelHorizontally(label5);
            CenterLabelHorizontally(lblBacktoLogin);
            CenterLabelHorizontally(label6);
        }

        private void CenterLabelHorizontally(Control control)
        {
            // Calcola la posizione orizzontale per centrare l'elemento di controllo
            int centerX = (this.ClientSize.Width - control.Width) / 2;

            // Imposta la posizione X dell'elemento
            control.Location = new Point(centerX, control.Location.Y);
        }

        //connessione al db

        private async void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtConPassword.Text == "")
            {
                MessageBox.Show("I campi Username e Password sono vuoti.", "Registrazione Fallita", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtRole.SelectedIndex == -1)
            {
                MessageBox.Show("Il campo Role è vuoto.", "Registrazione Fallita", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtPassword.Text == txtConPassword.Text)
            {
                try
                {
                    CrowApiClient apiClient = new CrowApiClient();

                    Dipendente dipendente = new Dipendente(txtUsername.Text, txtPassword.Text, txtRole.Text.ToLower(), "", "");
                    string response = await apiClient.PostAsync<Dipendente>(Dipendente.SiginRoute, dipendente);
                    Console.WriteLine($"Sigin: {response}");
                }

                catch (Exception ex)
                {
                    // Gestisci eventuali errori di connessione o elaborazione
                    MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtUsername.Text = "";
                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtRole.SelectedIndex = -1;

                MessageBox.Show("Il tuo Account è stato creato con successo.\nOra attendi l'approvazione da parte dell'amministratore prima di poter effettuare l'accesso.", "Registrazione avvenuta con Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new frmLogin().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("La password non corrisponde, si prega di reinserirla.", "Registrazione Fallita", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void checkBoxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConPassword.PasswordChar = '•';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtRole.SelectedIndex = -1;
            txtUsername.Focus();
        }

        private void lblBacktoLogin_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
