using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Power_Fitness.CrowApi;

namespace Power_Fitness.Dipendenti
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            CenterLabelHorizontally(pictureBox1);
            CenterLabelHorizontally(label1);
            CenterLabelHorizontally(label5);
            CenterLabelHorizontally(lblCreateAcc);
            CenterLabelHorizontally(label4);
        }

        private void CenterLabelHorizontally(Control control)
        {
            // Calcola la posizione orizzontale per centrare l'elemento di controllo
            int centerX = (this.ClientSize.Width - control.Width) / 2;

            // Imposta la posizione X dell'elemento
            control.Location = new Point(centerX, control.Location.Y);
        }

        // Connessione al db

        private async void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("I campi Username e/o Password sono vuoti.", "Login Fallito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                CrowApiClient apiClient = new CrowApiClient();
                string tokenValue;
                try
                {
                    Dipendente dipendente = new Dipendente(txtUsername.Text, txtPassword.Text);
                    string response = await apiClient.PostAsync<Dipendente>(Dipendente.LoginRoute, dipendente);
                    Console.WriteLine($"Login: {response}");

                    // Essendo che 'response 200' è una stringa nel formato "Token:valore"
                    string[] responseParts = response.Split(':');

                    if (responseParts.Length == 2 && responseParts[0].Trim() == "Token")
                    {
                        tokenValue = responseParts[1].Trim();
                        Console.WriteLine($"Token: {tokenValue}");
                    }
                    else
                    {
                        MessageBox.Show("Formato di risposta non valido.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                catch (Exception ex)
                {
                    // Gestisci eventuali errori di connessione o elaborazione
                    MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MainForm mainForm = new MainForm(tokenValue, txtUsername.Text);
                mainForm.Show(); 
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void checkboxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void lblCreateAcc_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
