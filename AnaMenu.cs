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


        private void AnaMenu_Load(object sender, EventArgs e)
        {
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
            DegisenPanel.Controls.Clear();
            GirisCikis girisCikis = new GirisCikis();
            girisCikis.MdiParent = this;
            girisCikis.FormBorderStyle = FormBorderStyle.None;
            DegisenPanel.Controls.Add(girisCikis);
            girisCikis.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DegisenPanel.Controls.Clear();
            Odalar odalar = new Odalar();
            odalar.MdiParent = this;
            odalar.FormBorderStyle = FormBorderStyle.None;
            DegisenPanel.Controls.Add(odalar);
            odalar.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DegisenPanel.Controls.Clear();
            Muhasebe muh = new Muhasebe();
            muh.MdiParent = this;
            muh.FormBorderStyle = FormBorderStyle.None;
            DegisenPanel.Controls.Add(muh);
            muh.Show();

        }

        private void btnCikisYap_Click(object sender, EventArgs e)
        {
            Giris cikis = new Giris();
            this.Hide();
            cikis.ShowDialog();
            this.Close();


        }

        private void button6_Click(object sender, EventArgs e)
        {

            AdminIslemleri adm = new AdminIslemleri();
            DegisenPanel.Controls.Clear();
            adm.MdiParent = this;
            adm.FormBorderStyle = FormBorderStyle.None;
            DegisenPanel.Controls.Add(adm);
            adm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VeriMenusu veri = new VeriMenusu();
            DegisenPanel.Controls.Clear(); 
            veri.MdiParent = this;
            veri.FormBorderStyle = FormBorderStyle.None;
            DegisenPanel.Controls.Add(veri);
            veri.Show();
        }
    }
}
