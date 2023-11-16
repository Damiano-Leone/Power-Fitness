namespace Power_Fitness.GestionePalestraBase
{
    partial class GestionePalestraBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionePalestraBase));
            this.pnl = new System.Windows.Forms.Panel();
            this.dataElencoIstanze = new System.Windows.Forms.DataGridView();
            this.status = new System.Windows.Forms.StatusStrip();
            this.lblTotale = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumeroTotale = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAggiungiIstanza = new System.Windows.Forms.ToolStripButton();
            this.btnModificaIstanza = new System.Windows.Forms.ToolStripButton();
            this.btnRimuoviIstanza = new System.Windows.Forms.ToolStripButton();
            this.txtRicerca = new System.Windows.Forms.ToolStripTextBox();
            this.btnPrenotazioni = new System.Windows.Forms.ToolStripButton();
            this.btnGestisci = new System.Windows.Forms.ToolStripButton();
            this.pnlAggiungi = new System.Windows.Forms.Panel();
            this.btnAnnulla = new System.Windows.Forms.Button();
            this.btnAggiungi = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataElencoIstanze)).BeginInit();
            this.status.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlAggiungi.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.dataElencoIstanze);
            this.pnl.Controls.Add(this.status);
            this.pnl.Controls.Add(this.toolStrip1);
            this.pnl.Location = new System.Drawing.Point(402, 133);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(667, 385);
            this.pnl.TabIndex = 2;
            // 
            // dataElencoIstanze
            // 
            this.dataElencoIstanze.AllowUserToAddRows = false;
            this.dataElencoIstanze.AllowUserToDeleteRows = false;
            this.dataElencoIstanze.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataElencoIstanze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataElencoIstanze.Location = new System.Drawing.Point(0, 27);
            this.dataElencoIstanze.MultiSelect = false;
            this.dataElencoIstanze.Name = "dataElencoIstanze";
            this.dataElencoIstanze.ReadOnly = true;
            this.dataElencoIstanze.RowHeadersWidth = 51;
            this.dataElencoIstanze.RowTemplate.Height = 24;
            this.dataElencoIstanze.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataElencoIstanze.Size = new System.Drawing.Size(667, 333);
            this.dataElencoIstanze.TabIndex = 1;
            this.dataElencoIstanze.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataElencoIstanze_CellDoubleClick);
            this.dataElencoIstanze.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataElencoIstanze_CellMouseEnter);
            this.dataElencoIstanze.SelectionChanged += new System.EventHandler(this.dataElencoIstanze_SelectionChanged);
            this.dataElencoIstanze.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataElencoIstanze_KeyDown);
            // 
            // status
            // 
            this.status.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotale,
            this.lblNumeroTotale});
            this.status.Location = new System.Drawing.Point(0, 360);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(667, 25);
            this.status.SizingGrip = false;
            this.status.TabIndex = 2;
            this.status.Text = "status";
            // 
            // lblTotale
            // 
            this.lblTotale.Name = "lblTotale";
            this.lblTotale.Size = new System.Drawing.Size(53, 20);
            this.lblTotale.Text = "Totale:";
            // 
            // lblNumeroTotale
            // 
            this.lblNumeroTotale.Name = "lblNumeroTotale";
            this.lblNumeroTotale.Size = new System.Drawing.Size(17, 20);
            this.lblNumeroTotale.Text = "0";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAggiungiIstanza,
            this.btnModificaIstanza,
            this.btnRimuoviIstanza,
            this.txtRicerca,
            this.btnPrenotazioni,
            this.btnGestisci});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(667, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolCorsi";
            // 
            // btnAggiungiIstanza
            // 
            this.btnAggiungiIstanza.Image = ((System.Drawing.Image)(resources.GetObject("btnAggiungiIstanza.Image")));
            this.btnAggiungiIstanza.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAggiungiIstanza.Name = "btnAggiungiIstanza";
            this.btnAggiungiIstanza.Size = new System.Drawing.Size(94, 24);
            this.btnAggiungiIstanza.Text = "Aggiungi";
            this.btnAggiungiIstanza.ToolTipText = "Aggiungi";
            this.btnAggiungiIstanza.Click += new System.EventHandler(this.btnAggiungiIstanza_Click);
            this.btnAggiungiIstanza.MouseEnter += new System.EventHandler(this.btnAggiungiIstanza_MouseEnter);
            this.btnAggiungiIstanza.MouseLeave += new System.EventHandler(this.btnAggiungiIstanza_MouseLeave);
            // 
            // btnModificaIstanza
            // 
            this.btnModificaIstanza.Image = ((System.Drawing.Image)(resources.GetObject("btnModificaIstanza.Image")));
            this.btnModificaIstanza.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificaIstanza.Name = "btnModificaIstanza";
            this.btnModificaIstanza.Size = new System.Drawing.Size(92, 24);
            this.btnModificaIstanza.Text = "Modifica";
            this.btnModificaIstanza.ToolTipText = "Modifica";
            this.btnModificaIstanza.Click += new System.EventHandler(this.btnModificaIstanza_Click);
            this.btnModificaIstanza.MouseEnter += new System.EventHandler(this.btnModificaIstanza_MouseEnter);
            this.btnModificaIstanza.MouseLeave += new System.EventHandler(this.btnModificaIstanza_MouseLeave);
            // 
            // btnRimuoviIstanza
            // 
            this.btnRimuoviIstanza.Image = ((System.Drawing.Image)(resources.GetObject("btnRimuoviIstanza.Image")));
            this.btnRimuoviIstanza.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRimuoviIstanza.Name = "btnRimuoviIstanza";
            this.btnRimuoviIstanza.Size = new System.Drawing.Size(87, 24);
            this.btnRimuoviIstanza.Text = "Rimuovi";
            this.btnRimuoviIstanza.ToolTipText = "Rimuovi";
            this.btnRimuoviIstanza.Click += new System.EventHandler(this.btnRimuoviIstanza_Click);
            this.btnRimuoviIstanza.MouseEnter += new System.EventHandler(this.btnRimuoviIstanza_MouseEnter);
            this.btnRimuoviIstanza.MouseLeave += new System.EventHandler(this.btnRimuoviIstanza_MouseLeave);
            // 
            // txtRicerca
            // 
            this.txtRicerca.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtRicerca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRicerca.Name = "txtRicerca";
            this.txtRicerca.Size = new System.Drawing.Size(250, 27);
            this.txtRicerca.Enter += new System.EventHandler(this.txtRicerca_Enter);
            this.txtRicerca.Leave += new System.EventHandler(this.txtRicerca_Leave);
            this.txtRicerca.TextChanged += new System.EventHandler(this.txtRicerca_TextChanged);
            // 
            // btnPrenotazioni
            // 
            this.btnPrenotazioni.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPrenotazioni.Image = ((System.Drawing.Image)(resources.GetObject("btnPrenotazioni.Image")));
            this.btnPrenotazioni.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrenotazioni.Name = "btnPrenotazioni";
            this.btnPrenotazioni.Size = new System.Drawing.Size(84, 24);
            this.btnPrenotazioni.Text = "Prenota";
            this.btnPrenotazioni.Click += new System.EventHandler(this.btnPrenotazioni_Click);
            // 
            // btnGestisci
            // 
            this.btnGestisci.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnGestisci.Image = ((System.Drawing.Image)(resources.GetObject("btnGestisci.Image")));
            this.btnGestisci.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGestisci.Name = "btnGestisci";
            this.btnGestisci.Size = new System.Drawing.Size(83, 24);
            this.btnGestisci.Text = "Gestisci";
            this.btnGestisci.Click += new System.EventHandler(this.btnGestisci_Click);
            // 
            // pnlAggiungi
            // 
            this.pnlAggiungi.Controls.Add(this.btnAnnulla);
            this.pnlAggiungi.Controls.Add(this.btnAggiungi);
            this.pnlAggiungi.Location = new System.Drawing.Point(12, 12);
            this.pnlAggiungi.Name = "pnlAggiungi";
            this.pnlAggiungi.Size = new System.Drawing.Size(360, 570);
            this.pnlAggiungi.TabIndex = 3;
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnulla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnnulla.Location = new System.Drawing.Point(264, 515);
            this.btnAnnulla.Name = "btnAnnulla";
            this.btnAnnulla.Size = new System.Drawing.Size(80, 30);
            this.btnAnnulla.TabIndex = 13;
            this.btnAnnulla.Text = "Annulla";
            this.btnAnnulla.UseVisualStyleBackColor = true;
            this.btnAnnulla.Click += new System.EventHandler(this.btnAnnulla_Click);
            // 
            // btnAggiungi
            // 
            this.btnAggiungi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAggiungi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAggiungi.Location = new System.Drawing.Point(178, 515);
            this.btnAggiungi.Name = "btnAggiungi";
            this.btnAggiungi.Size = new System.Drawing.Size(80, 30);
            this.btnAggiungi.TabIndex = 12;
            this.btnAggiungi.Text = "Aggiungi";
            this.btnAggiungi.UseVisualStyleBackColor = true;
            this.btnAggiungi.Click += new System.EventHandler(this.btnAggiungi_Click);
            // 
            // GestionePalestraBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1078, 594);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.pnlAggiungi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "GestionePalestraBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GestionePalestraBase";
            this.Load += new System.EventHandler(this.GestionePalestraBase_Load);
            this.SizeChanged += new System.EventHandler(this.GestionePalestraBase_SizeChanged);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataElencoIstanze)).EndInit();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlAggiungi.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel pnl;
        protected System.Windows.Forms.DataGridView dataElencoIstanze;
        protected System.Windows.Forms.StatusStrip status;
        protected System.Windows.Forms.ToolStripStatusLabel lblTotale;
        protected System.Windows.Forms.ToolStripStatusLabel lblNumeroTotale;
        protected System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton btnAggiungiIstanza;
        protected System.Windows.Forms.ToolStripButton btnModificaIstanza;
        protected System.Windows.Forms.ToolStripButton btnRimuoviIstanza;
        protected System.Windows.Forms.Panel pnlAggiungi;
        protected System.Windows.Forms.Button btnAnnulla;
        protected System.Windows.Forms.Button btnAggiungi;
        private System.Windows.Forms.ToolTip toolTip1;
        protected System.Windows.Forms.ToolStripTextBox txtRicerca;
        protected System.Windows.Forms.ToolStripButton btnGestisci;
        protected System.Windows.Forms.ToolStripButton btnPrenotazioni;
    }
}