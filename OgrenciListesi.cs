using iText.Layout.Element;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Windows.Forms;
using System.Diagnostics;
using ClosedXML.Excel;

namespace YurtOtomasyonu
{
    public partial class OgrenciListesi : Form
    {
        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true"); 
        SqlDataAdapter adapter;
        DataTable tablo;
        DataSet ds = new DataSet();

        public OgrenciListesi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog sfd = new SaveFileDialog() { Filter="Excel Sayfası| *.xlsx" })
            {
                if (sfd.ShowDialog()==DialogResult.OK)
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
        private void dataOgrenciler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
