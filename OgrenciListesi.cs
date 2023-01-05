using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YurtOtomasyonu
{
    public partial class OgrenciListesi : Form
    {
        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true"); 
        SqlDataAdapter adapter;
        DataTable tablo;

        public OgrenciListesi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void OgrenciListesi_Load(object sender, EventArgs e)
        {
            KisiGetir();
        }

        private void KisiGetir()
        {
            adapter = new SqlDataAdapter("SELECT * FROM Ogrenci", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataOgrenciler.DataSource = tablo;
            dataOgrenciler.ReadOnly = true;
            baglanti.Close();
        }

        private void BaglantiAc()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
        }
    }
}
