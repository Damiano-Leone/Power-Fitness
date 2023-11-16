namespace Power_Fitness.Corsi.Lezioni
{
    partial class Lezioni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lezioni));
            this.lblGiornoSettimana = new System.Windows.Forms.Label();
            this.comboBoxGiorno = new System.Windows.Forms.ComboBox();
            this.lblOraInizio = new System.Windows.Forms.Label();
            this.lblOraFine = new System.Windows.Forms.Label();
            this.lblMaxNumPart = new System.Windows.Forms.Label();
            this.numMaxPart = new System.Windows.Forms.NumericUpDown();
            this.oraInizio = new System.Windows.Forms.DateTimePicker();
            this.oraFine = new System.Windows.Forms.DateTimePicker();
            this.pnlAggiungi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPart)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Size = new System.Drawing.Size(1078, 594);
            // 
            // pnlAggiungi
            // 
            this.pnlAggiungi.Controls.Add(this.oraFine);
            this.pnlAggiungi.Controls.Add(this.oraInizio);
            this.pnlAggiungi.Controls.Add(this.numMaxPart);
            this.pnlAggiungi.Controls.Add(this.lblMaxNumPart);
            this.pnlAggiungi.Controls.Add(this.lblOraFine);
            this.pnlAggiungi.Controls.Add(this.lblOraInizio);
            this.pnlAggiungi.Controls.Add(this.comboBoxGiorno);
            this.pnlAggiungi.Controls.Add(this.lblGiornoSettimana);
            this.pnlAggiungi.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAggiungi.Location = new System.Drawing.Point(0, 0);
            this.pnlAggiungi.Size = new System.Drawing.Size(360, 594);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblGiornoSettimana, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.btnAggiungi, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.btnAnnulla, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.comboBoxGiorno, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblOraInizio, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblOraFine, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblMaxNumPart, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.numMaxPart, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.oraInizio, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.oraFine, 0);
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Location = new System.Drawing.Point(264, 539);
            // 
            // btnAggiungi
            // 
            this.btnAggiungi.Location = new System.Drawing.Point(178, 539);
            // 
            // lblGiornoSettimana
            // 
            this.lblGiornoSettimana.AutoSize = true;
            this.lblGiornoSettimana.Location = new System.Drawing.Point(21, 48);
            this.lblGiornoSettimana.Name = "lblGiornoSettimana";
            this.lblGiornoSettimana.Size = new System.Drawing.Size(156, 17);
            this.lblGiornoSettimana.TabIndex = 14;
            this.lblGiornoSettimana.Text = "Giorno della Settimana:";
            // 
            // comboBoxGiorno
            // 
            this.comboBoxGiorno.FormattingEnabled = true;
            this.comboBoxGiorno.Items.AddRange(new object[] {
            "Lunedì",
            "Martedì",
            "Mercoledì",
            "Giovedì",
            "Venerdì",
            "Sabato",
            "Domenica"});
            this.comboBoxGiorno.Location = new System.Drawing.Point(200, 48);
            this.comboBoxGiorno.Name = "comboBoxGiorno";
            this.comboBoxGiorno.Size = new System.Drawing.Size(144, 24);
            this.comboBoxGiorno.TabIndex = 15;
            // 
            // lblOraInizio
            // 
            this.lblOraInizio.AutoSize = true;
            this.lblOraInizio.Location = new System.Drawing.Point(21, 128);
            this.lblOraInizio.Name = "lblOraInizio";
            this.lblOraInizio.Size = new System.Drawing.Size(87, 17);
            this.lblOraInizio.TabIndex = 16;
            this.lblOraInizio.Text = "Ora di Inizio:";
            // 
            // lblOraFine
            // 
            this.lblOraFine.AutoSize = true;
            this.lblOraFine.Location = new System.Drawing.Point(21, 208);
            this.lblOraFine.Name = "lblOraFine";
            this.lblOraFine.Size = new System.Drawing.Size(82, 17);
            this.lblOraFine.TabIndex = 17;
            this.lblOraFine.Text = "Ora di Fine:";
            // 
            // lblMaxNumPart
            // 
            this.lblMaxNumPart.AutoSize = true;
            this.lblMaxNumPart.Location = new System.Drawing.Point(21, 288);
            this.lblMaxNumPart.Name = "lblMaxNumPart";
            this.lblMaxNumPart.Size = new System.Drawing.Size(214, 17);
            this.lblMaxNumPart.TabIndex = 26;
            this.lblMaxNumPart.Text = "Numero massimo di partecipanti:";
            // 
            // numMaxPart
            // 
            this.numMaxPart.Location = new System.Drawing.Point(241, 288);
            this.numMaxPart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxPart.Name = "numMaxPart";
            this.numMaxPart.Size = new System.Drawing.Size(103, 22);
            this.numMaxPart.TabIndex = 27;
            this.numMaxPart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // oraInizio
            // 
            this.oraInizio.CustomFormat = "HH:mm";
            this.oraInizio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.oraInizio.Location = new System.Drawing.Point(241, 128);
            this.oraInizio.Name = "oraInizio";
            this.oraInizio.ShowUpDown = true;
            this.oraInizio.Size = new System.Drawing.Size(103, 22);
            this.oraInizio.TabIndex = 28;
            this.oraInizio.Value = new System.DateTime(2023, 9, 1, 15, 0, 0, 0);
            // 
            // oraFine
            // 
            this.oraFine.CustomFormat = "HH:mm";
            this.oraFine.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.oraFine.Location = new System.Drawing.Point(241, 208);
            this.oraFine.Name = "oraFine";
            this.oraFine.ShowUpDown = true;
            this.oraFine.Size = new System.Drawing.Size(102, 22);
            this.oraFine.TabIndex = 29;
            this.oraFine.Value = new System.DateTime(2023, 9, 1, 17, 0, 0, 0);
            // 
            // Lezioni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1078, 594);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Lezioni";
            this.Text = "Gestione Lezioni";
            this.pnlAggiungi.ResumeLayout(false);
            this.pnlAggiungi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxGiorno;
        private System.Windows.Forms.Label lblGiornoSettimana;
        private System.Windows.Forms.Label lblOraFine;
        private System.Windows.Forms.Label lblOraInizio;
        private System.Windows.Forms.NumericUpDown numMaxPart;
        private System.Windows.Forms.Label lblMaxNumPart;
        private System.Windows.Forms.DateTimePicker oraFine;
        private System.Windows.Forms.DateTimePicker oraInizio;
    }
}
