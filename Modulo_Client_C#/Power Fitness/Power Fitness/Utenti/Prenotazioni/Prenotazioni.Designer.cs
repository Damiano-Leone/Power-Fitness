namespace Power_Fitness.Utenti.Prenotazioni
{
    partial class Prenotazioni
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prenotazioni));
            this.comboBoxCorso = new System.Windows.Forms.ComboBox();
            this.lblCorso = new System.Windows.Forms.Label();
            this.lblDataPrenotazione = new System.Windows.Forms.Label();
            this.dataPrenotazione = new System.Windows.Forms.DateTimePicker();
            this.lblFasciaOraria = new System.Windows.Forms.Label();
            this.comboBoxFasciaOraria = new System.Windows.Forms.ComboBox();
            this.pnlAggiungi.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(360, 0);
            this.pnl.Size = new System.Drawing.Size(718, 594);
            // 
            // pnlAggiungi
            // 
            this.pnlAggiungi.Controls.Add(this.lblFasciaOraria);
            this.pnlAggiungi.Controls.Add(this.comboBoxFasciaOraria);
            this.pnlAggiungi.Controls.Add(this.dataPrenotazione);
            this.pnlAggiungi.Controls.Add(this.lblDataPrenotazione);
            this.pnlAggiungi.Controls.Add(this.lblCorso);
            this.pnlAggiungi.Controls.Add(this.comboBoxCorso);
            this.pnlAggiungi.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAggiungi.Location = new System.Drawing.Point(0, 0);
            this.pnlAggiungi.Size = new System.Drawing.Size(360, 594);
            this.pnlAggiungi.Controls.SetChildIndex(this.comboBoxCorso, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblCorso, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblDataPrenotazione, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.dataPrenotazione, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.comboBoxFasciaOraria, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblFasciaOraria, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.btnAggiungi, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.btnAnnulla, 0);
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Location = new System.Drawing.Point(264, 539);
            // 
            // btnAggiungi
            // 
            this.btnAggiungi.Location = new System.Drawing.Point(178, 539);
            // 
            // comboBoxCorso
            // 
            this.comboBoxCorso.FormattingEnabled = true;
            this.comboBoxCorso.Location = new System.Drawing.Point(144, 58);
            this.comboBoxCorso.Name = "comboBoxCorso";
            this.comboBoxCorso.Size = new System.Drawing.Size(200, 24);
            this.comboBoxCorso.TabIndex = 14;
            this.comboBoxCorso.SelectedIndexChanged += new System.EventHandler(this.comboBoxCorso_SelectedIndexChanged);
            // 
            // lblCorso
            // 
            this.lblCorso.AutoSize = true;
            this.lblCorso.Location = new System.Drawing.Point(89, 58);
            this.lblCorso.Name = "lblCorso";
            this.lblCorso.Size = new System.Drawing.Size(49, 17);
            this.lblCorso.TabIndex = 15;
            this.lblCorso.Text = "Corso:";
            // 
            // lblDataPrenotazione
            // 
            this.lblDataPrenotazione.AutoSize = true;
            this.lblDataPrenotazione.Location = new System.Drawing.Point(8, 158);
            this.lblDataPrenotazione.Name = "lblDataPrenotazione";
            this.lblDataPrenotazione.Size = new System.Drawing.Size(130, 17);
            this.lblDataPrenotazione.TabIndex = 17;
            this.lblDataPrenotazione.Text = "Data Prenotazione:";
            // 
            // dataPrenotazione
            // 
            this.dataPrenotazione.Location = new System.Drawing.Point(144, 158);
            this.dataPrenotazione.Name = "dataPrenotazione";
            this.dataPrenotazione.Size = new System.Drawing.Size(200, 22);
            this.dataPrenotazione.TabIndex = 18;
            this.dataPrenotazione.Value = new System.DateTime(2023, 9, 1, 0, 0, 0, 0);
            this.dataPrenotazione.ValueChanged += new System.EventHandler(this.dataPrenotazione_ValueChanged);
            // 
            // lblFasciaOraria
            // 
            this.lblFasciaOraria.AutoSize = true;
            this.lblFasciaOraria.Location = new System.Drawing.Point(41, 246);
            this.lblFasciaOraria.Name = "lblFasciaOraria";
            this.lblFasciaOraria.Size = new System.Drawing.Size(97, 17);
            this.lblFasciaOraria.TabIndex = 20;
            this.lblFasciaOraria.Text = "Fascia Oraria:";
            // 
            // comboBoxFasciaOraria
            // 
            this.comboBoxFasciaOraria.FormattingEnabled = true;
            this.comboBoxFasciaOraria.Location = new System.Drawing.Point(144, 246);
            this.comboBoxFasciaOraria.Name = "comboBoxFasciaOraria";
            this.comboBoxFasciaOraria.Size = new System.Drawing.Size(200, 24);
            this.comboBoxFasciaOraria.TabIndex = 19;
            // 
            // Prenotazioni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1078, 594);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Prenotazioni";
            this.Text = "Prenotazioni";
            this.pnlAggiungi.ResumeLayout(false);
            this.pnlAggiungi.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCorso;
        private System.Windows.Forms.ComboBox comboBoxCorso;
        private System.Windows.Forms.DateTimePicker dataPrenotazione;
        private System.Windows.Forms.Label lblDataPrenotazione;
        private System.Windows.Forms.Label lblFasciaOraria;
        private System.Windows.Forms.ComboBox comboBoxFasciaOraria;
    }
}
