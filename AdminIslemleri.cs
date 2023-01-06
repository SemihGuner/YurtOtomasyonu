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
    public partial class AdminIslemleri : Form
    {
        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true");
        DataTable tablo;
        public AdminIslemleri()
        {
            InitializeComponent();
            KisiGetir(); 
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
            var adapter = new SqlDataAdapter("SELECT * FROM Calisan", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dgvCalisanlar.DataSource = tablo;
            dgvCalisanlar.ReadOnly = true;
            baglanti.Close();
        }
        private void AdminIslemleri_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboYetki_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCalEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "Insert into Calisan (ad,soyad,tckimlik,vasif,username,pass) values (@ad,@soyad,@tckimlik,@vasif,@username,@pass)";
            var komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@tckimlik", maskedTCKimlik.Text); 
            komut.Parameters.AddWithValue("@ad", txtAd.Text); 
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text); 
            komut.Parameters.AddWithValue("@vasif", comboYetki.Text); 
            komut.Parameters.AddWithValue("@username", txtUsername.Text); 
            komut.Parameters.AddWithValue("@pass", txtPass.Text); 
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

        private void btnCalCikar_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE Calisan Where id=@id";
            var komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", dgvCalisanlar.CurrentRow.Cells[0].Value);
            BaglantiAc();
            komut.ExecuteNonQuery();
            baglanti.Close();
            KisiGetir();
        }

        private void btnCalDuzenle_Click(object sender, EventArgs e)
        {
            string sorgu = "Update Calisan set ad=@ad,soyad=@soyad,tckimlik=@tckimlik,vasif=@vasif,username=@username,pass=@pass where id=@id";
            var komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", dgvCalisanlar.CurrentRow.Cells[0].Value);
            komut.Parameters.AddWithValue("@tckimlik", maskedTCKimlik.Text);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@vasif", comboYetki.Text);
            komut.Parameters.AddWithValue("@username", txtUsername.Text);
            komut.Parameters.AddWithValue("@pass", txtPass.Text);
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

        private void dgvCalisanlar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUsername.Text = Convert.ToString(this.dgvCalisanlar.CurrentRow.Cells[1].Value); 
            txtPass.Text = Convert.ToString(this.dgvCalisanlar.CurrentRow.Cells[2].Value); 
            comboYetki.Text = Convert.ToString(this.dgvCalisanlar.CurrentRow.Cells[3].Value); 
            txtAd.Text = Convert.ToString(this.dgvCalisanlar.CurrentRow.Cells[4].Value); 
            txtSoyad.Text = Convert.ToString(this.dgvCalisanlar.CurrentRow.Cells[5].Value);
            maskedTCKimlik.Text = Convert.ToString(this.dgvCalisanlar.CurrentRow.Cells[6].Value);  
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Sayfası| *.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(tablo, "Çalışanlar");
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
