using ClosedXML.Excel;
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
    public partial class Muhasebe : Form
    {
        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true"); 
        SqlDataAdapter adapter;
        DataTable tablo;

        public Muhasebe()
        {
            InitializeComponent();
        }

        private void Muhasebe_Load(object sender, EventArgs e)
        {
            KisiGetir();
            DekontGetir();
        } 

        private void dataOgrenci_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTCKimlik.Text.ToString() != "")
            {
                KisiGetir(maskedTCKimlik.Text.ToString());
                DekontGetir(maskedTCKimlik.Text.ToString()); 
            }
            else
            {
                KisiGetir();
                DekontGetir();
            }
        }
        private void KisiGetir(string TC)
        { 
            adapter = new SqlDataAdapter("SELECT ogrID,Ad,Soyad,OdendiMi FROM Ogrenci where TcKimlik = '"+TC+"'", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataOgrenci.DataSource = tablo;
            dataOgrenci.ReadOnly = true;
            baglanti.Close();
        }
        private void KisiGetir()
        { 
            adapter = new SqlDataAdapter("SELECT ogrID,Ad,Soyad,OdendiMi FROM Ogrenci", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataOgrenci.DataSource = tablo;
            dataOgrenci.ReadOnly = true;
            baglanti.Close();
        }

        private void DekontGetir()
        {
            adapter = new SqlDataAdapter("select top (100) Ad,Soyad,odemeTarihi as 'Ödeme Tarihi',tutar as 'Tutar' from Ogrenci full outer join Muhasebe on Ogrenci.ogrID = Muhasebe.ogrID", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataDekontlar.DataSource = tablo;
            dataDekontlar.ReadOnly = true;
            baglanti.Close();
        }
        private void DekontGetir(string TC)
        {
            adapter = new SqlDataAdapter("select top (100) Ad,Soyad,odemeTarihi as 'Ödeme Tarihi',tutar as 'Tutar' from Ogrenci full outer join Muhasebe on Ogrenci.ogrID = Muhasebe.ogrID where Ogrenci.TcKimlik ='" + TC + "'", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataDekontlar.DataSource = tablo;
            dataDekontlar.ReadOnly = true;
            baglanti.Close();
        }
        private void BaglantiAc()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "Insert into Muhasebe(ogrID,tutar) values("+ this.dataOgrenci.CurrentRow.Cells[0].Value + " , "+ textboxTutar.Text.ToString() +")";
            var komut = new SqlCommand(sorgu, baglanti);  
            BaglantiAc();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KisiGetir();
            DekontGetir();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Sayfası| *.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(tablo, "Muhasebe");
                            workbook.SaveAs(sfd.FileName);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("HATA! \n :" + ex.Message);
                    }
                }
            }
        }
    }
}
