using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ClosedXML.Excel;

namespace YurtOtomasyonu
{ 
    public partial class OgrenciIslemleri : Form
    {
        public OgrenciIslemleri()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true");
        SqlCommand komut; 
        SqlDataReader oku; 
        SqlDataAdapter adapter; 
        DataTable tablo;
        

        private void OgrenciIslemleri_Load(object sender, EventArgs e)
        { 
            ComboBoxDoldurucu();
            KisiGetir();
            dataOgrenciler.SelectionMode = DataGridViewSelectionMode.CellSelect;
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
        

        private void ComboBoxDoldurucu()
        {
            // Birinci combobox
            BaglantiAc();
            komut = new SqlCommand("select distinct bolumAd from Bolum", baglanti);
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBolum.Items.Add(oku[0].ToString());
            }
            baglanti.Close();
            // İkinci combobox
            BaglantiAc();
            komut = new SqlCommand("select OdaNo from Oda where kalanKisi != kapasite and OdaNo!='000'", baglanti);
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboOda.Items.Add(oku[0].ToString());
            }
            baglanti.Close();
        }
        private void BaglantiAc()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "Insert into Ogrenci (TcKimlik,Ad,Soyad,Telefon,BolumAd,Iban,OdaNo,Adres,VeliAd,VeliSoyad,VeliTelefon) values (@TcKimlik,@Ad,@Soyad,@Telefon,@Bolum,@Iban,@Oda,@Adres,@VeliAd,@VeliSoyad,@VeliTelefon)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@TcKimlik", maskedTCKimlik.Text); 
            komut.Parameters.AddWithValue("@Ad", txtOgrAd.Text); 
            komut.Parameters.AddWithValue("@Soyad", txtOgrSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", maskedOgrTel.Text); 
            komut.Parameters.AddWithValue("@Bolum", comboBolum.Text);
            komut.Parameters.AddWithValue("@Iban", maskedIban.Text); 
            komut.Parameters.AddWithValue("@Oda", comboOda.Text); 
            komut.Parameters.AddWithValue("@Adres", richAdres.Text);
            komut.Parameters.AddWithValue("@VeliAd", txtVeliAd.Text);
            komut.Parameters.AddWithValue("@VeliSoyad", txtVeliSoyad.Text);
            komut.Parameters.AddWithValue("@VeliTelefon", maskedVeliTelefon.Text); 
            BaglantiAc();
            try
            {
                komut.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("HATA! \n" + sqlEx.Message); 
            }
            catch (Exception)
            {
                MessageBox.Show("HATA!");
            }
            baglanti.Close();
            KisiGetir();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE Ogrenci Where OgrId=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", dataOgrenciler.CurrentRow.Cells[0].Value);
            BaglantiAc();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KisiGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "Update Ogrenci Set TcKimlik=@TcKimlik,Ad=@Ad,Soyad=@Soyad,Telefon=@Telefon,BolumAd=@Bolum,Iban=@Iban,OdaNo=@Oda,Adres=@Adres,VeliAd=@VeliAd,VeliSoyad=@VeliSoyad,VeliTelefon=@VeliTelefon Where OgrId=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@TcKimlik", maskedTCKimlik.Text);
            komut.Parameters.AddWithValue("@Ad", txtOgrAd.Text);
            komut.Parameters.AddWithValue("@Soyad", txtOgrSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", maskedOgrTel.Text);
            komut.Parameters.AddWithValue("@Bolum", comboBolum.Text);
            komut.Parameters.AddWithValue("@Iban", maskedIban.Text);
            komut.Parameters.AddWithValue("@Oda", comboOda.Text);
            komut.Parameters.AddWithValue("@Adres", richAdres.Text);
            komut.Parameters.AddWithValue("@VeliAd", txtVeliAd.Text);
            komut.Parameters.AddWithValue("@VeliSoyad", txtVeliSoyad.Text);
            komut.Parameters.AddWithValue("@VeliTelefon", maskedVeliTelefon.Text);
            komut.Parameters.AddWithValue("@id", dataOgrenciler.CurrentRow.Cells[0].Value);
            BaglantiAc();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KisiGetir();
        }

        private void dataOgrenciler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maskedTCKimlik.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[1].Value);
            txtOgrAd.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[2].Value);
            txtOgrSoyad.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[3].Value);
            maskedOgrTel.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[4].Value);
            richAdres.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[5].Value);
            maskedIban.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[6].Value);
            comboOda.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[9].Value);
            comboBolum.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[10].Value);
            txtVeliAd.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[11].Value);
            txtVeliSoyad.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[12].Value);
            maskedVeliTelefon.Text = Convert.ToString(this.dataOgrenciler.CurrentRow.Cells[13].Value);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtAra.Text.ToString() != "")
            { 
                if (comboArama.SelectedIndex == 0)
                {
                    TcAra(txtAra.Text.ToString());
                }
                else if (comboArama.SelectedIndex == 1)
                {
                    AdAra(txtAra.Text.ToString());
                }
                else
                {
                    string sorgu = "select * from Ogrenci where " + SecilenArama(comboArama.SelectedIndex) + " = '" + txtAra.Text.ToString() + "'";
                    adapter = new SqlDataAdapter(sorgu, baglanti);
                    tablo = new DataTable();
                    adapter.Fill(tablo);
                    dataOgrenciler.DataSource = tablo;
                    dataOgrenciler.ReadOnly = true;
                    baglanti.Close(); 
                }
                btnXlsx.Enabled = true;
            }
            else
            {
                MessageBox.Show("Lütfen kutucuğu doldurun.");
            }
        }

        private void TcAra(string dis)
        {
            string sorgu = "exec spTCArama "+ dis;
            adapter = new SqlDataAdapter(sorgu, baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataOgrenciler.DataSource = tablo;
            dataOgrenciler.ReadOnly = true;
            baglanti.Close();
        }
        private void AdAra(string dis)
        {
            string sorgu = "exec spAdlaArama "+ dis;
            adapter = new SqlDataAdapter(sorgu, baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dataOgrenciler.DataSource = tablo;
            dataOgrenciler.ReadOnly = true;
            baglanti.Close();
        } 
        private string SecilenArama(int secilenIndex)
        {
            switch (secilenIndex)
            {
                case 0:
                    return "TcKimlik";
                case 1:
                    return "Ad";
                case 2:
                    return "Soyad";
                case 3:
                    return "Telefon";
                case 4:
                    return "BolumAd"; 
                case 6:
                    return "OdendiMi"; 
                default:
                    return "OdaNo";
            }
        }

        private void dataOgrenciler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["OgrenciListesi"] as OgrenciListesi) != null)
            { 
            }
            else
            { 
                OgrenciListesi ogr = new OgrenciListesi();
                ogr.Show();
            }
        }

        private void comboArama_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXlsx_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Sayfası| *.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(tablo, "Öğrenciler");
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
