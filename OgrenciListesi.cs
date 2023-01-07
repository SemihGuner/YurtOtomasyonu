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

        private void PDFCikisi()
        {
            try
            {
                int i = 0;
                string sql = "select * from Ogrenci";
                int yPoint = 0;
                string tckimlik = null;
                string ad = null;
                string soyad = null;
                string telefon = null;
                string adres = null;
                string iban = null;
                string kayittarihi = null;
                string odendimi = null;
                string odano = null;
                string bolumad = null;
                string veliad = null;
                string velisoyad = null;
                string velitelefon = null;
                string yurttami = null;
                BaglantiAc();
                var command = new SqlCommand(sql, baglanti);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                baglanti.Close();

                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "Database to PDF";
                PdfPage pdfPage = pdf.AddPage();
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                XFont font = new XFont("Times New Roman", 4, XFontStyle.Regular);

                yPoint = yPoint + 100;

                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    tckimlik = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                    ad = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    soyad = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    telefon = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                    adres = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                    iban = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                    kayittarihi = ds.Tables[0].Rows[i].ItemArray[6].ToString();
                    odendimi = ds.Tables[0].Rows[i].ItemArray[7].ToString();
                    odano = ds.Tables[0].Rows[i].ItemArray[8].ToString();
                    bolumad = ds.Tables[0].Rows[i].ItemArray[9].ToString();
                    veliad = ds.Tables[0].Rows[i].ItemArray[10].ToString();
                    velisoyad = ds.Tables[0].Rows[i].ItemArray[11].ToString();
                    velitelefon = ds.Tables[0].Rows[i].ItemArray[12].ToString();
                    yurttami = ds.Tables[0].Rows[i].ItemArray[13].ToString();

                    graph.DrawString(tckimlik, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(ad, font, XBrushes.Black, new XRect(80, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(soyad, font, XBrushes.Black, new XRect(120, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(telefon, font, XBrushes.Black, new XRect(160, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(adres, font, XBrushes.Black, new XRect(200, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(iban, font, XBrushes.Black, new XRect(240, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(kayittarihi, font, XBrushes.Black, new XRect(280, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(odendimi, font, XBrushes.Black, new XRect(320, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(odano, font, XBrushes.Black, new XRect(360, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(bolumad, font, XBrushes.Black, new XRect(400, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(veliad, font, XBrushes.Black, new XRect(440, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(velisoyad, font, XBrushes.Black, new XRect(480, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(velitelefon, font, XBrushes.Black, new XRect(520, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(yurttami, font, XBrushes.Black, new XRect(560, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    yPoint = yPoint + 40;
                }


                string pdfFilename = "dbtopdf.pdf";
                pdf.Save(pdfFilename);
                Process.Start(pdfFilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataOgrenciler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
