namespace Power_Fitness.Abbonamenti
{
    partial class Abbonamenti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Abbonamenti));
            this.pnlAbbonamenti = new System.Windows.Forms.Panel();
            this.pnlCreaAbbonamento = new System.Windows.Forms.Panel();
            this.pnlAggiungiAbbonamento = new System.Windows.Forms.Panel();
            this.lblAggiungiAbbonamento = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAggiungiAbb = new System.Windows.Forms.Button();
            this.comboBoxAbbonamento = new System.Windows.Forms.ComboBox();
            this.lblDurataAbbonamento = new System.Windows.Forms.Label();
            this.txtDataInizio = new System.Windows.Forms.DateTimePicker();
            this.lblDataInizio = new System.Windows.Forms.Label();
            this.lblStatoAbbonamento = new System.Windows.Forms.Label();
            this.userBox = new System.Windows.Forms.PictureBox();
            this.pnlUtente = new System.Windows.Forms.Panel();
            this.lblUtente = new System.Windows.Forms.Label();
            this.comboBoxAbbonamentoM = new System.Windows.Forms.ComboBox();
            this.lblTipoAbbonamentoM = new System.Windows.Forms.Label();
            this.txtDataInizioM = new System.Windows.Forms.DateTimePicker();
            this.lblDataInizioM = new System.Windows.Forms.Label();
            this.lblIdAbbonamento = new System.Windows.Forms.Label();
            this.txtIdAbbonamento = new System.Windows.Forms.TextBox();
            this.pnlAggiungi.SuspendLayout();
            this.pnlAbbonamenti.SuspendLayout();
            this.pnlCreaAbbonamento.SuspendLayout();
            this.pnlAggiungiAbbonamento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userBox)).BeginInit();
            this.pnlUtente.SuspendLayout();
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
            this.pnlAggiungi.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAggiungi.Controls.Add(this.txtIdAbbonamento);
            this.pnlAggiungi.Controls.Add(this.lblIdAbbonamento);
            this.pnlAggiungi.Controls.Add(this.comboBoxAbbonamentoM);
            this.pnlAggiungi.Controls.Add(this.lblTipoAbbonamentoM);
            this.pnlAggiungi.Controls.Add(this.txtDataInizioM);
            this.pnlAggiungi.Controls.Add(this.lblDataInizioM);
            this.pnlAggiungi.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAggiungi.Location = new System.Drawing.Point(360, 0);
            this.pnlAggiungi.Size = new System.Drawing.Size(360, 594);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblDataInizioM, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.txtDataInizioM, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblTipoAbbonamentoM, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.comboBoxAbbonamentoM, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.btnAggiungi, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.btnAnnulla, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.lblIdAbbonamento, 0);
            this.pnlAggiungi.Controls.SetChildIndex(this.txtIdAbbonamento, 0);
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Location = new System.Drawing.Point(264, 539);
            // 
            // btnAggiungi
            // 
            this.btnAggiungi.Location = new System.Drawing.Point(178, 539);
            // 
            // pnlAbbonamenti
            // 
            this.pnlAbbonamenti.BackColor = System.Drawing.Color.White;
            this.pnlAbbonamenti.Controls.Add(this.pnlCreaAbbonamento);
            this.pnlAbbonamenti.Controls.Add(this.lblStatoAbbonamento);
            this.pnlAbbonamenti.Controls.Add(this.userBox);
            this.pnlAbbonamenti.Controls.Add(this.pnlUtente);
            this.pnlAbbonamenti.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAbbonamenti.Location = new System.Drawing.Point(0, 0);
            this.pnlAbbonamenti.Name = "pnlAbbonamenti";
            this.pnlAbbonamenti.Size = new System.Drawing.Size(360, 594);
            this.pnlAbbonamenti.TabIndex = 37;
            // 
            // pnlCreaAbbonamento
            // 
            this.pnlCreaAbbonamento.Controls.Add(this.pnlAggiungiAbbonamento);
            this.pnlCreaAbbonamento.Controls.Add(this.btnRefresh);
            this.pnlCreaAbbonamento.Controls.Add(this.btnAggiungiAbb);
            this.pnlCreaAbbonamento.Controls.Add(this.comboBoxAbbonamento);
            this.pnlCreaAbbonamento.Controls.Add(this.lblDurataAbbonamento);
            this.pnlCreaAbbonamento.Controls.Add(this.txtDataInizio);
            this.pnlCreaAbbonamento.Controls.Add(this.lblDataInizio);
            this.pnlCreaAbbonamento.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCreaAbbonamento.Location = new System.Drawing.Point(0, 268);
            this.pnlCreaAbbonamento.Name = "pnlCreaAbbonamento";
            this.pnlCreaAbbonamento.Size = new System.Drawing.Size(360, 326);
            this.pnlCreaAbbonamento.TabIndex = 43;
            // 
            // pnlAggiungiAbbonamento
            // 
            this.pnlAggiungiAbbonamento.BackColor = System.Drawing.Color.Orange;
            this.pnlAggiungiAbbonamento.Controls.Add(this.lblAggiungiAbbonamento);
            this.pnlAggiungiAbbonamento.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAggiungiAbbonamento.Location = new System.Drawing.Point(0, 0);
            this.pnlAggiungiAbbonamento.Name = "pnlAggiungiAbbonamento";
            this.pnlAggiungiAbbonamento.Size = new System.Drawing.Size(360, 58);
            this.pnlAggiungiAbbonamento.TabIndex = 43;
            // 
            // lblAggiungiAbbonamento
            // 
            this.lblAggiungiAbbonamento.AutoSize = true;
            this.lblAggiungiAbbonamento.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAggiungiAbbonamento.ForeColor = System.Drawing.Color.White;
            this.lblAggiungiAbbonamento.Location = new System.Drawing.Point(47, 10);
            this.lblAggiungiAbbonamento.Name = "lblAggiungiAbbonamento";
            this.lblAggiungiAbbonamento.Size = new System.Drawing.Size(266, 38);
            this.lblAggiungiAbbonamento.TabIndex = 15;
            this.lblAggiungiAbbonamento.Text = "Crea Abbonamento";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(314, 212);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 49;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAggiungiAbb
            // 
            this.btnAggiungiAbb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAggiungiAbb.Location = new System.Drawing.Point(89, 282);
            this.btnAggiungiAbb.Name = "btnAggiungiAbb";
            this.btnAggiungiAbb.Size = new System.Drawing.Size(182, 32);
            this.btnAggiungiAbb.TabIndex = 48;
            this.btnAggiungiAbb.Tag = "crea";
            this.btnAggiungiAbb.Text = "Crea";
            this.btnAggiungiAbb.UseVisualStyleBackColor = true;
            this.btnAggiungiAbb.Click += new System.EventHandler(this.btnAggiungiAbb_Click);
            // 
            // comboBoxAbbonamento
            // 
            this.comboBoxAbbonamento.FormattingEnabled = true;
            this.comboBoxAbbonamento.Items.AddRange(new object[] {
            "1 Mese",
            "2 Mesi",
            "3 Mesi",
            "6 Mesi",
            "12 Mesi"});
            this.comboBoxAbbonamento.Location = new System.Drawing.Point(153, 162);
            this.comboBoxAbbonamento.Name = "comboBoxAbbonamento";
            this.comboBoxAbbonamento.Size = new System.Drawing.Size(193, 24);
            this.comboBoxAbbonamento.TabIndex = 47;
            // 
            // lblDurataAbbonamento
            // 
            this.lblDurataAbbonamento.AutoSize = true;
            this.lblDurataAbbonamento.Location = new System.Drawing.Point(15, 162);
            this.lblDurataAbbonamento.Name = "lblDurataAbbonamento";
            this.lblDurataAbbonamento.Size = new System.Drawing.Size(132, 17);
            this.lblDurataAbbonamento.TabIndex = 46;
            this.lblDurataAbbonamento.Text = "Tipo Abbonamento:";
            // 
            // txtDataInizio
            // 
            this.txtDataInizio.CustomFormat = "yyyy-MM-dd";
            this.txtDataInizio.Location = new System.Drawing.Point(126, 102);
            this.txtDataInizio.MinDate = new System.DateTime(1930, 1, 1, 0, 0, 0, 0);
            this.txtDataInizio.Name = "txtDataInizio";
            this.txtDataInizio.Size = new System.Drawing.Size(220, 22);
            this.txtDataInizio.TabIndex = 45;
            this.txtDataInizio.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // lblDataInizio
            // 
            this.lblDataInizio.AutoSize = true;
            this.lblDataInizio.Location = new System.Drawing.Point(15, 102);
            this.lblDataInizio.Name = "lblDataInizio";
            this.lblDataInizio.Size = new System.Drawing.Size(93, 17);
            this.lblDataInizio.TabIndex = 44;
            this.lblDataInizio.Text = "Data di Inizio:";
            // 
            // lblStatoAbbonamento
            // 
            this.lblStatoAbbonamento.AutoSize = true;
            this.lblStatoAbbonamento.BackColor = System.Drawing.Color.Orange;
            this.lblStatoAbbonamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatoAbbonamento.ForeColor = System.Drawing.Color.White;
            this.lblStatoAbbonamento.Location = new System.Drawing.Point(108, 180);
            this.lblStatoAbbonamento.Name = "lblStatoAbbonamento";
            this.lblStatoAbbonamento.Size = new System.Drawing.Size(140, 24);
            this.lblStatoAbbonamento.TabIndex = 18;
            this.lblStatoAbbonamento.Text = "Abbonamento: ";
            this.lblStatoAbbonamento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userBox
            // 
            this.userBox.Location = new System.Drawing.Point(126, 75);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(100, 90);
            this.userBox.TabIndex = 17;
            this.userBox.TabStop = false;
            // 
            // pnlUtente
            // 
            this.pnlUtente.BackColor = System.Drawing.Color.Orange;
            this.pnlUtente.Controls.Add(this.lblUtente);
            this.pnlUtente.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUtente.Location = new System.Drawing.Point(0, 0);
            this.pnlUtente.Name = "pnlUtente";
            this.pnlUtente.Size = new System.Drawing.Size(360, 58);
            this.pnlUtente.TabIndex = 15;
            // 
            // lblUtente
            // 
            this.lblUtente.AutoSize = true;
            this.lblUtente.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUtente.ForeColor = System.Drawing.Color.White;
            this.lblUtente.Location = new System.Drawing.Point(129, 10);
            this.lblUtente.Name = "lblUtente";
            this.lblUtente.Size = new System.Drawing.Size(103, 38);
            this.lblUtente.TabIndex = 15;
            this.lblUtente.Text = "Utente";
            // 
            // comboBoxAbbonamentoM
            // 
            this.comboBoxAbbonamentoM.FormattingEnabled = true;
            this.comboBoxAbbonamentoM.Items.AddRange(new object[] {
            "1 Mese",
            "2 Mesi",
            "3 Mesi",
            "6 Mesi",
            "12 Mesi"});
            this.comboBoxAbbonamentoM.Location = new System.Drawing.Point(152, 255);
            this.comboBoxAbbonamentoM.Name = "comboBoxAbbonamentoM";
            this.comboBoxAbbonamentoM.Size = new System.Drawing.Size(192, 24);
            this.comboBoxAbbonamentoM.TabIndex = 44;
            // 
            // lblTipoAbbonamentoM
            // 
            this.lblTipoAbbonamentoM.AutoSize = true;
            this.lblTipoAbbonamentoM.Location = new System.Drawing.Point(14, 255);
            this.lblTipoAbbonamentoM.Name = "lblTipoAbbonamentoM";
            this.lblTipoAbbonamentoM.Size = new System.Drawing.Size(132, 17);
            this.lblTipoAbbonamentoM.TabIndex = 43;
            this.lblTipoAbbonamentoM.Text = "Tipo Abbonamento:";
            // 
            // txtDataInizioM
            // 
            this.txtDataInizioM.CustomFormat = "yyyy-MM-dd";
            this.txtDataInizioM.Location = new System.Drawing.Point(125, 155);
            this.txtDataInizioM.MinDate = new System.DateTime(1930, 1, 1, 0, 0, 0, 0);
            this.txtDataInizioM.Name = "txtDataInizioM";
            this.txtDataInizioM.Size = new System.Drawing.Size(219, 22);
            this.txtDataInizioM.TabIndex = 42;
            this.txtDataInizioM.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // lblDataInizioM
            // 
            this.lblDataInizioM.AutoSize = true;
            this.lblDataInizioM.Location = new System.Drawing.Point(14, 155);
            this.lblDataInizioM.Name = "lblDataInizioM";
            this.lblDataInizioM.Size = new System.Drawing.Size(93, 17);
            this.lblDataInizioM.TabIndex = 41;
            this.lblDataInizioM.Text = "Data di Inizio:";
            // 
            // lblIdAbbonamento
            // 
            this.lblIdAbbonamento.AutoSize = true;
            this.lblIdAbbonamento.Location = new System.Drawing.Point(14, 55);
            this.lblIdAbbonamento.Name = "lblIdAbbonamento";
            this.lblIdAbbonamento.Size = new System.Drawing.Size(117, 17);
            this.lblIdAbbonamento.TabIndex = 45;
            this.lblIdAbbonamento.Text = "ID Abbonamento:";
            // 
            // txtIdAbbonamento
            // 
            this.txtIdAbbonamento.Cursor = System.Windows.Forms.Cursors.No;
            this.txtIdAbbonamento.Location = new System.Drawing.Point(152, 55);
            this.txtIdAbbonamento.Name = "txtIdAbbonamento";
            this.txtIdAbbonamento.ReadOnly = true;
            this.txtIdAbbonamento.Size = new System.Drawing.Size(192, 22);
            this.txtIdAbbonamento.TabIndex = 46;
            this.txtIdAbbonamento.TabStop = false;
            this.txtIdAbbonamento.Enter += new System.EventHandler(this.txtIdAbbonamento_Enter);
            // 
            // Abbonamenti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1078, 594);
            this.Controls.Add(this.pnlAbbonamenti);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Abbonamenti";
            this.Text = "Abbonamenti";
            this.Controls.SetChildIndex(this.pnlAbbonamenti, 0);
            this.Controls.SetChildIndex(this.pnlAggiungi, 0);
            this.Controls.SetChildIndex(this.pnl, 0);
            this.pnlAggiungi.ResumeLayout(false);
            this.pnlAggiungi.PerformLayout();
            this.pnlAbbonamenti.ResumeLayout(false);
            this.pnlAbbonamenti.PerformLayout();
            this.pnlCreaAbbonamento.ResumeLayout(false);
            this.pnlCreaAbbonamento.PerformLayout();
            this.pnlAggiungiAbbonamento.ResumeLayout(false);
            this.pnlAggiungiAbbonamento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userBox)).EndInit();
            this.pnlUtente.ResumeLayout(false);
            this.pnlUtente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAbbonamenti;
        private System.Windows.Forms.Label lblStatoAbbonamento;
        private System.Windows.Forms.PictureBox userBox;
        private System.Windows.Forms.Panel pnlUtente;
        private System.Windows.Forms.Label lblUtente;
        private System.Windows.Forms.ComboBox comboBoxAbbonamentoM;
        private System.Windows.Forms.Label lblTipoAbbonamentoM;
        private System.Windows.Forms.DateTimePicker txtDataInizioM;
        private System.Windows.Forms.Label lblDataInizioM;
        private System.Windows.Forms.TextBox txtIdAbbonamento;
        private System.Windows.Forms.Label lblIdAbbonamento;
        private System.Windows.Forms.Panel pnlCreaAbbonamento;
        private System.Windows.Forms.Panel pnlAggiungiAbbonamento;
        private System.Windows.Forms.Label lblAggiungiAbbonamento;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAggiungiAbb;
        private System.Windows.Forms.ComboBox comboBoxAbbonamento;
        private System.Windows.Forms.Label lblDurataAbbonamento;
        private System.Windows.Forms.DateTimePicker txtDataInizio;
        private System.Windows.Forms.Label lblDataInizio;
    }
}
