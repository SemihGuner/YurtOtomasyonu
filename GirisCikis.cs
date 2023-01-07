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
    public partial class GirisCikis : Form
    {
        public GirisCikis()
        {
            InitializeComponent();
        }

        private void GirisCikis_Load(object sender, EventArgs e)
        {
            KisiGetir();
            GCGetir();
            dataOgrencilerTablosu.SelectionMode = DataGridViewSelectionMode.CellSelect;
        } 
        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true"); 
        SqlDataAdapter adapter;
        DataTable tablo;
        SqlCommand komut;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataOgrencilerTablosu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string durum = dataOgrencilerTablosu.CurrentRow.Cells[5].Value.ToString();
            if (durum == "EVET")
            { 
                txtDurum.Text = "Yurtta";
            }
            else
            {
                txtDurum.Text = "Yurtta Değil";
            }
        }

        private void BaglantiAc()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
        }

        private void KisiGetir()
        {
            adapter = new SqlDataAdapter("SELECT OgrID,Ad,Soyad,OdaNo as 'Oda Numarası',TCKimlik, yurttaMi as 'Yurtta Mı' FROM Ogrenci", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataOgrencilerTablosu.DataSource = tablo;
            dataOgrencilerTablosu.ReadOnly = true;
            baglanti.Close();
        } 
        private void GCGetir()
        {
            adapter = new SqlDataAdapter("select top (10) Ogrenci.ogrID,TcKimlik,Ad,Soyad, GirisCikisState, IslemTarihi from Ogrenci right outer join GirisCikis on Ogrenci.ogrID=GirisCikis.ogrID order by IslemTarihi desc", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataVeriler.DataSource = tablo;
            dataVeriler.ReadOnly = true;
            baglanti.Close();
        } 
        private void GCGetir(string ogrID)
        {
            adapter = new SqlDataAdapter("select top (100) Ogrenci.ogrID,TcKimlik,Ad,Soyad, GirisCikisState, IslemTarihi from Ogrenci right outer join GirisCikis on Ogrenci.ogrID=GirisCikis.ogrID order by IslemTarihi desc", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo); 
            dataVeriler.ReadOnly = true;
            baglanti.Close();
            DataView dv = tablo.DefaultView;
            dv.RowFilter = "TCKimlik Like '" + maskedTCKimlik.Text + "%'";
            dataVeriler.DataSource = dv;
        } 
        private void button1_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("SELECT OgrID,Ad,Soyad,OdaNo as 'Oda Numarası',TCKimlik, yurttaMi as 'Yurtta Mı' FROM Ogrenci", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            baglanti.Close();
            DataView dv = tablo.DefaultView;
            dv.RowFilter = "TCKimlik Like '" + maskedTCKimlik.Text + "%'";
            dataOgrencilerTablosu.DataSource = dv;
            GCGetir(maskedTCKimlik.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTCKimlik_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dataGCikis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (dataOgrencilerTablosu.CurrentRow.Cells[5].Value.ToString() == "HAYIR")
            {
                var sorgu = "Insert into GirisCikis(ogrID,GirisCikisState) values (@id,'GİRİŞ')";
                string updSorgu = "Update Ogrenci set yurttaMi='EVET' where OgrID=@id";
                komut = new SqlCommand(sorgu, baglanti);
                var komutIki = new SqlCommand(updSorgu, baglanti);
                komut.Parameters.AddWithValue("@id", dataOgrencilerTablosu.CurrentRow.Cells[0].Value);
                komutIki.Parameters.AddWithValue("@id", dataOgrencilerTablosu.CurrentRow.Cells[0].Value);
                BaglantiAc();
                komut.ExecuteNonQuery();
                komutIki.ExecuteNonQuery();
                baglanti.Close();
                KisiGetir();
                GCGetir();
            }
            else
            {
                MessageBox.Show("Bu öğrenci zaten yurt içerisinde!");
            }
            
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (dataOgrencilerTablosu.CurrentRow.Cells[5].Value.ToString() == "EVET")
            {
                var sorgu = "Insert into GirisCikis(ogrID,GirisCikisState) values (@id,'ÇIKIŞ')";
                string updSorgu = "Update Ogrenci set yurttaMi='HAYIR' where OgrID=@id";
                komut = new SqlCommand(sorgu, baglanti);
                var komutIki = new SqlCommand(updSorgu, baglanti);
                komut.Parameters.AddWithValue("@id", dataOgrencilerTablosu.CurrentRow.Cells[0].Value);
                komutIki.Parameters.AddWithValue("@id", dataOgrencilerTablosu.CurrentRow.Cells[0].Value);
                BaglantiAc();
                komut.ExecuteNonQuery();
                komutIki.ExecuteNonQuery();
                baglanti.Close();
                KisiGetir();
                GCGetir();
            }
            else
            {
                MessageBox.Show("Bu öğrenci zaten yurt dışında!");
            }

        }

        private void dataVeriler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataOgrencilerTablosu_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                            workbook.Worksheets.Add(tablo, "Giriş-Çıkış");
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

        private void btnIzinMenu_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["IzinMenu"] as IzinMenu) != null)
            { 
            }
            else
            { 
                IzinMenu izin = new IzinMenu();
                izin.Show();
            }
        }
    }
}
