using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YurtOtomasyonu
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DegisenPanel.Controls.Clear();
            OgrenciIslemleri ogrenciIslemleri = new OgrenciIslemleri();
            ogrenciIslemleri.MdiParent = this;
            ogrenciIslemleri.FormBorderStyle = FormBorderStyle.None;
            DegisenPanel.Controls.Add(ogrenciIslemleri);
            ogrenciIslemleri.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
