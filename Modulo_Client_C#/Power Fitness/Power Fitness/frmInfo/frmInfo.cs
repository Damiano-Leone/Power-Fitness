using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Power_Fitness.frmInfo
{
    public partial class frmInfo : Form
    {
        public frmInfo()
        {
            InitializeComponent();
            CenterLabelHorizontally(lblInfo);
            CenterLabelHorizontally(lblContatti);
            CenterLabelHorizontally(lblDev);
            CenterLabelHorizontally(lblMail);

        }



        private void CenterLabelHorizontally(Control control)
        {
            // Calcola la posizione orizzontale per centrare l'elemento di controllo
            int centerX = (this.ClientSize.Width - control.Width) / 2;

            // Imposta la posizione X dell'elemento
            control.Location = new Point(centerX, control.Location.Y);
        }

    }
}
