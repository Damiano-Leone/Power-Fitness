namespace Power_Fitness.Corsi
{
    partial class Corsi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Corsi));
            this.lblDataFineCorso = new System.Windows.Forms.Label();
            this.txtDataFineCorso = new System.Windows.Forms.DateTimePicker();
            this.txtDataInizioCorso = new System.Windows.Forms.DateTimePicker();
            this.txtDescrizione = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblDescrizione = new System.Windows.Forms.Label();
            this.lblDataInizioCorso = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.pnlAggiungi.SuspendLayout();
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
            this.pnlAggiungi.Controls.Add(this.lblDataFineCorso);
            this.pnlAggiungi.Controls.Add(this.txtDataFineCorso);
            this.pnlAggiungi.Controls.Add(this.txtDataInizioCorso);
            this.pnlAggiungi.Controls.Add(this.txtDescrizione);
            this.pnlAggiungi.Controls.Add(this.txtNome);
            this.pnlAggiungi.Controls.Add(this.lblDescrizione);
            this.pnlAggiungi.Controls.Add(this.lblDataInizioCorso);
            this.pnlAggiungi.Controls.Add(this.lblNome);
            this.pnlAggiungi.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAggiungi.Location = new System.Drawing.Point(0, 0);
            this.pnlAggiungi.Size = new System.Drawing.Size(360, 594);
            this.pnlAggiungi.Controls.SetChildIndex(this.btnAggiungi, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.btnAnnulla, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblNome, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblDataInizioCorso, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblDescrizione, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.txtNome, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.txtDescrizione, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.txtDataInizioCorso, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.txtDataFineCorso, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblDataFineCorso, 0);
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Location = new System.Drawing.Point(264, 539);
            // 
            // btnAggiungi
            // 
            this.btnAggiungi.Location = new System.Drawing.Point(178, 539);
            // 
            // lblDataFineCorso
            // 
            this.lblDataFineCorso.AutoSize = true;
            this.lblDataFineCorso.Location = new System.Drawing.Point(3, 121);
            this.lblDataFineCorso.Name = "lblDataFineCorso";
            this.lblDataFineCorso.Size = new System.Drawing.Size(119, 17);
            this.lblDataFineCorso.TabIndex = 24;
            this.lblDataFineCorso.Text = "Data Fine Corso*:";
            // 
            // txtDataFineCorso
            // 
            this.txtDataFineCorso.CustomFormat = "yyyy-MM-dd";
            this.txtDataFineCorso.Location = new System.Drawing.Point(128, 121);
            this.txtDataFineCorso.MinDate = new System.DateTime(2023, 5, 20, 0, 0, 0, 0);
            this.txtDataFineCorso.Name = "txtDataFineCorso";
            this.txtDataFineCorso.Size = new System.Drawing.Size(216, 22);
            this.txtDataFineCorso.TabIndex = 21;
            this.txtDataFineCorso.Value = new System.DateTime(2023, 9, 1, 0, 0, 0, 0);
            // 
            // txtDataInizioCorso
            // 
            this.txtDataInizioCorso.CustomFormat = "yyyy-MM-dd";
            this.txtDataInizioCorso.Location = new System.Drawing.Point(128, 71);
            this.txtDataInizioCorso.MinDate = new System.DateTime(2023, 5, 20, 0, 0, 0, 0);
            this.txtDataInizioCorso.Name = "txtDataInizioCorso";
            this.txtDataInizioCorso.Size = new System.Drawing.Size(216, 22);
            this.txtDataInizioCorso.TabIndex = 20;
            this.txtDataInizioCorso.Value = new System.DateTime(2023, 9, 1, 0, 0, 0, 0);
            // 
            // txtDescrizione
            // 
            this.txtDescrizione.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDescrizione.Location = new System.Drawing.Point(128, 171);
            this.txtDescrizione.Multiline = true;
            this.txtDescrizione.Name = "txtDescrizione";
            this.txtDescrizione.Size = new System.Drawing.Size(216, 312);
            this.txtDescrizione.TabIndex = 23;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(128, 20);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(216, 22);
            this.txtNome.TabIndex = 19;
            // 
            // lblDescrizione
            // 
            this.lblDescrizione.AutoSize = true;
            this.lblDescrizione.Location = new System.Drawing.Point(36, 171);
            this.lblDescrizione.Name = "lblDescrizione";
            this.lblDescrizione.Size = new System.Drawing.Size(86, 17);
            this.lblDescrizione.TabIndex = 18;
            this.lblDescrizione.Text = "Descrizione:";
            // 
            // lblDataInizioCorso
            // 
            this.lblDataInizioCorso.AutoSize = true;
            this.lblDataInizioCorso.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.lblDataInizioCorso.Location = new System.Drawing.Point(3, 71);
            this.lblDataInizioCorso.Name = "lblDataInizioCorso";
            this.lblDataInizioCorso.Size = new System.Drawing.Size(124, 17);
            this.lblDataInizioCorso.TabIndex = 16;
            this.lblDataInizioCorso.Text = "Data Inizio Corso*:";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(68, 20);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(54, 17);
            this.lblNome.TabIndex = 15;
            this.lblNome.Text = "Nome*:";
            // 
            // Corsi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1078, 594);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Corsi";
            this.Text = "Gestione Corsi";
            this.pnlAggiungi.ResumeLayout(false);
            this.pnlAggiungi.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDataFineCorso;
        private System.Windows.Forms.DateTimePicker txtDataFineCorso;
        private System.Windows.Forms.DateTimePicker txtDataInizioCorso;
        private System.Windows.Forms.TextBox txtDescrizione;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblDescrizione;
        private System.Windows.Forms.Label lblDataInizioCorso;
        private System.Windows.Forms.Label lblNome;
    }
}
