namespace Power_Fitness
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnDipendenti = new System.Windows.Forms.Button();
            this.btnCorsi = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblOrario = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnImpostazioni = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.timerOrario = new System.Windows.Forms.Timer(this.components);
            this.btnAbbonamenti = new System.Windows.Forms.Button();
            this.btnUtenti = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDipendenti
            // 
            this.btnDipendenti.BackColor = System.Drawing.Color.White;
            this.btnDipendenti.FlatAppearance.BorderSize = 2;
            this.btnDipendenti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDipendenti.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnDipendenti.Location = new System.Drawing.Point(615, 260);
            this.btnDipendenti.Name = "btnDipendenti";
            this.btnDipendenti.Size = new System.Drawing.Size(200, 140);
            this.btnDipendenti.TabIndex = 5;
            this.btnDipendenti.Text = "Gestione Dipendenti";
            this.btnDipendenti.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDipendenti.UseVisualStyleBackColor = false;
            this.btnDipendenti.Click += new System.EventHandler(this.btnDipendenti_Click);
            // 
            // btnCorsi
            // 
            this.btnCorsi.BackColor = System.Drawing.Color.White;
            this.btnCorsi.FlatAppearance.BorderSize = 2;
            this.btnCorsi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorsi.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnCorsi.Location = new System.Drawing.Point(32, 44);
            this.btnCorsi.Name = "btnCorsi";
            this.btnCorsi.Size = new System.Drawing.Size(200, 140);
            this.btnCorsi.TabIndex = 0;
            this.btnCorsi.Text = "Gestione Corsi";
            this.btnCorsi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCorsi.UseVisualStyleBackColor = false;
            this.btnCorsi.Click += new System.EventHandler(this.btnCorsi_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblOrario});
            this.statusStrip1.Location = new System.Drawing.Point(0, 438);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(860, 23);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblOrario
            // 
            this.lblOrario.ActiveLinkColor = System.Drawing.Color.Red;
            this.lblOrario.BackColor = System.Drawing.Color.White;
            this.lblOrario.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrario.Name = "lblOrario";
            this.lblOrario.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOrario.Size = new System.Drawing.Size(845, 18);
            this.lblOrario.Spring = true;
            this.lblOrario.Text = "toolStripStatusLabel1";
            this.lblOrario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOrario.Click += new System.EventHandler(this.lblOrario_Click);
            // 
            // btnImpostazioni
            // 
            this.btnImpostazioni.BackColor = System.Drawing.Color.White;
            this.btnImpostazioni.FlatAppearance.BorderSize = 2;
            this.btnImpostazioni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImpostazioni.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnImpostazioni.Location = new System.Drawing.Point(32, 260);
            this.btnImpostazioni.Name = "btnImpostazioni";
            this.btnImpostazioni.Size = new System.Drawing.Size(200, 140);
            this.btnImpostazioni.TabIndex = 3;
            this.btnImpostazioni.Text = "Info e Contatti";
            this.btnImpostazioni.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImpostazioni.UseVisualStyleBackColor = false;
            this.btnImpostazioni.Click += new System.EventHandler(this.btnImpostazioni_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.White;
            this.btnLogout.FlatAppearance.BorderSize = 2;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnLogout.Location = new System.Drawing.Point(326, 260);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 140);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // timerOrario
            // 
            this.timerOrario.Interval = 1000;
            this.timerOrario.Tick += new System.EventHandler(this.timerOrario_Tick);
            // 
            // btnAbbonamenti
            // 
            this.btnAbbonamenti.BackColor = System.Drawing.Color.White;
            this.btnAbbonamenti.FlatAppearance.BorderSize = 2;
            this.btnAbbonamenti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbbonamenti.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbbonamenti.Location = new System.Drawing.Point(615, 44);
            this.btnAbbonamenti.Name = "btnAbbonamenti";
            this.btnAbbonamenti.Size = new System.Drawing.Size(200, 140);
            this.btnAbbonamenti.TabIndex = 2;
            this.btnAbbonamenti.Text = "Statistica Abbonamenti";
            this.btnAbbonamenti.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAbbonamenti.UseVisualStyleBackColor = false;
            this.btnAbbonamenti.Click += new System.EventHandler(this.btnAbbonamenti_Click);
            // 
            // btnUtenti
            // 
            this.btnUtenti.BackColor = System.Drawing.Color.White;
            this.btnUtenti.FlatAppearance.BorderSize = 2;
            this.btnUtenti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUtenti.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUtenti.Location = new System.Drawing.Point(326, 44);
            this.btnUtenti.Name = "btnUtenti";
            this.btnUtenti.Size = new System.Drawing.Size(200, 140);
            this.btnUtenti.TabIndex = 1;
            this.btnUtenti.Text = "Gestione Utenti";
            this.btnUtenti.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUtenti.UseVisualStyleBackColor = false;
            this.btnUtenti.Click += new System.EventHandler(this.btnUtenti_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(119)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(860, 461);
            this.Controls.Add(this.btnUtenti);
            this.Controls.Add(this.btnAbbonamenti);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnImpostazioni);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCorsi);
            this.Controls.Add(this.btnDipendenti);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Power Fitness";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDipendenti;
        private System.Windows.Forms.Button btnCorsi;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnImpostazioni;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.ToolStripStatusLabel lblOrario;
        private System.Windows.Forms.Timer timerOrario;
        private System.Windows.Forms.Button btnAbbonamenti;
        private System.Windows.Forms.Button btnUtenti;
    }
}

