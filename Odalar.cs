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
    public partial class Odalar : Form
    {
        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true"); 
        SqlDataAdapter adapter;
        DataTable tablo;
        string sorgu;
        List<string> odalar = new List<string>();
        private string OdaNoTutucu;

        public Odalar()
        {
            InitializeComponent();
        }

        OgrenciIslemleri ogrenciIslemleri = new OgrenciIslemleri();

        private void Odalar_Load(object sender, EventArgs e)
        {
            KisiGetir();
        }
        private void KisiGetir()
        {
            sorgu = "SELECT OdaNo as 'Oda No.', kapasite as 'Kapasite', KalanKisi as 'Kalan Kişi' FROM Oda where OdaNo!='000'";
            adapter = new SqlDataAdapter(sorgu, baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            tableOdalar.DataSource = tablo;
            tableOdalar.ReadOnly = true;
            baglanti.Close();
        }
        private void BaglantiAc()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
        } 

        private void tableOdalar_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            OdaNoTutucu = Convert.ToString(this.tableOdalar.CurrentRow.Cells[0].Value);
            sorgu = "Select Ad,Soyad,TcKimlik from Ogrenci where OdaNo='"+ OdaNoTutucu +"'"; 
            adapter = new SqlDataAdapter(sorgu, baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataOdadakiler.DataSource = tablo;
            dataOdadakiler.ReadOnly = true;
            baglanti.Close();
        }

        private void tableOdadakiler_CellClick(object sender, DataGridViewCellEventArgs e)
        { 

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnHata_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select OdaNo from Oda", baglanti);
            BaglantiAc();
            var oku= komut.ExecuteReader();
            while (oku.Read())
            {
                odalar.Add(oku.GetString(0)); 
            }
            oku.Close(); 
            for (int i = 0; i < odalar.Count(); i++)
            {
                //MessageBox.Show(odalar[i]);
                SqlCommand komutIki = new SqlCommand("update Oda set kalanKisi=(select Count(Ad) from Ogrenci where odaNo='" + odalar[i] + "') where odaNo='"+odalar[i]+"'", baglanti);
                komutIki.ExecuteNonQuery();
            }
        }
    }
}
