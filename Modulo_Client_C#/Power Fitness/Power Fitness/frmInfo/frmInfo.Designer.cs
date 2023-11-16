namespace Power_Fitness.frmInfo
{
    partial class frmInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfo));
            this.lblDev = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.pnlUtente = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblContatti = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlUtente.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDev
            // 
            this.lblDev.AutoSize = true;
            this.lblDev.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDev.Location = new System.Drawing.Point(232, 98);
            this.lblDev.Name = "lblDev";
            this.lblDev.Size = new System.Drawing.Size(617, 140);
            this.lblDev.TabIndex = 1;
            this.lblDev.Text = "Power Fitness è un progetto realizzato dagli studenti:\r\n\r\nDamiano Leone\r\n\r\nLuca C" +
    "irrone";
            this.lblDev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMail.Location = new System.Drawing.Point(392, 391);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(298, 84);
            this.lblMail.TabIndex = 3;
            this.lblMail.Text = "damy.leone@gmail.com\r\n\r\nlucacirrone@gmail.com";
            // 
            // pnlUtente
            // 
            this.pnlUtente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(119)))), ((int)(((byte)(38)))));
            this.pnlUtente.Controls.Add(this.lblInfo);
            this.pnlUtente.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUtente.Location = new System.Drawing.Point(0, 0);
            this.pnlUtente.Name = "pnlUtente";
            this.pnlUtente.Size = new System.Drawing.Size(1082, 58);
            this.pnlUtente.TabIndex = 16;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(498, 10);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(86, 37);
            this.lblInfo.TabIndex = 15;
            this.lblInfo.Text = "INFO";
            // 
            // lblContatti
            // 
            this.lblContatti.AutoSize = true;
            this.lblContatti.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.lblContatti.ForeColor = System.Drawing.Color.White;
            this.lblContatti.Location = new System.Drawing.Point(463, 10);
            this.lblContatti.Name = "lblContatti";
            this.lblContatti.Size = new System.Drawing.Size(156, 37);
            this.lblContatti.TabIndex = 15;
            this.lblContatti.Text = "CONTATTI";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(119)))), ((int)(((byte)(38)))));
            this.panel1.Controls.Add(this.lblContatti);
            this.panel1.Location = new System.Drawing.Point(0, 293);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 58);
            this.panel1.TabIndex = 17;
            // 
            // frmInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1082, 598);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlUtente);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.lblDev);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Info e contatti";
            this.pnlUtente.ResumeLayout(false);
            this.pnlUtente.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDev;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Panel pnlUtente;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblContatti;
        private System.Windows.Forms.Panel panel1;
    }
}