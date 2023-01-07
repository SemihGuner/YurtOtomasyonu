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
    public partial class IzinMenu : Form
    {
        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true");
        SqlDataAdapter adapter;
        DataTable tablo;
        DataSet ds = new DataSet();

        private void BaglantiAc()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
        }

        private void KisiGetir()
        {
            adapter = new SqlDataAdapter("select top (50) Ogrenci.ogrID,TcKimlik,Ad,Soyad, girisTarih,cikisTarih from Ogrenci right outer join Izin on Ogrenci.ogrID=Izin.ogrID order by girisTarih desc", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dgvIzinler.DataSource = tablo;
            dgvIzinler.ReadOnly = true;
            baglanti.Close();
        } 
        private void KisiGetir(string TC)
        {
            adapter = new SqlDataAdapter("select top (50) Ogrenci.ogrID,TcKimlik,Ad,Soyad, girisTarih,cikisTarih from Ogrenci right outer join Izin on Ogrenci.ogrID=Izin.ogrID where TcKimlik =" + TC + "order by girisTarih desc", baglanti);
            tablo = new DataTable();
            BaglantiAc();
            adapter.Fill(tablo);
            dgvIzinler.DataSource = tablo;
            dgvIzinler.ReadOnly = true;
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (maskedTCKimlik.Text != "")
            {
                KisiGetir(maskedTCKimlik.Text.ToString());
                groupBox1.Visible = true;
            }
            else
            {
                KisiGetir();
            }
        }

        public IzinMenu()
        {
            InitializeComponent();
        }

        private void IzinMenu_Load(object sender, EventArgs e)
        {
            KisiGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTCKimlik.Text.ToString() != "")
            {
                var cmd = new SqlCommand("insert into Izin values((select ogrId from Ogrenci where TcKimlik=@tckimlik), @girisTarih, @cikisTarih)", baglanti);
                cmd.Parameters.AddWithValue("@tckimlik", maskedTCKimlik.Text.ToString());
                cmd.Parameters.AddWithValue("@girisTarih", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@cikisTarih", dateTimePicker2.Value.Date);
                BaglantiAc();
                cmd.ExecuteNonQuery();
                baglanti.Close();
                KisiGetir(maskedTCKimlik.Text);
            }
            else
            {
                MessageBox.Show("TC kimlik kutusu boş!");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void maskedTCKimlik_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
