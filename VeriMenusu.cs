using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace YurtOtomasyonu
{
    public partial class VeriMenusu : Form
    {
        string yol,expYol;
        string yolbackup, yolrestore;
        SqlCommand komut;
        SqlConnection dbConnection = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true");

        private void BaglantiAc()
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
        }

        public VeriMenusu()
        {
            InitializeComponent();
        }

        public void btngozat_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "CSV Dosyaları|*.csv";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                yol = openFileDialog1.FileName;
                inputDizini.Text = yol;
                buttonImp.Enabled = true;
            }
        }
         

        public void buttonImp_Click(object sender, EventArgs e)
        {
            TabledanSQLe(CSVdenTable(yol));
        }


        private void btnVTIGozat_Click(object sender, EventArgs e)
        {
           
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                getCSV();
                txtKontrol.Text = "Export tamamlandı.";
            }
            catch (Exception exp)
            {
                MessageBox.Show("Hata: " + exp.Message);
            }
        }

        private void btnVTIImport_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExpGozat_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tbExportPath.Text = fbd.SelectedPath + @"\exportOgrenci.csv";
                    expYol = tbExportPath.Text;
                    btnExport.Enabled = true;
                }
            }
        }

        private static DataTable CSVdenTable(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { ";" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields(); 
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
                
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }
         
        public void TabledanSQLe(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"Data Source=127.0.0.1; Initial Catalog=YurtOtomasyonu; Integrated Security=true;"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "Ogrenci";

                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString()); 
                    s.WriteToServer(csvFileData);
                }
            } 
            txtKontrol.Text = "Import tamamlandı.";
        }

        private string CSVOlustur(IDataReader reader)
        { 
            StreamWriter sw = new StreamWriter(expYol);
            object[] output = new object[reader.FieldCount]; 
            try
            { 
                for (int i = 0; i < reader.FieldCount; i++)
                    output[i] = reader.GetName(i);

                sw.WriteLine(string.Join(";", output));

                while (reader.Read())
                {
                    reader.GetValues(output);
                    sw.WriteLine(string.Join(";", output));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata: " + e.Message);
            } 
            sw.Close();
            reader.Close();
            dbConnection.Close();
            return expYol;
        }
         
        private string getCSV()
        {
            using (dbConnection)
            {
                BaglantiAc();
                komut = new SqlCommand("select * from Ogrenci", dbConnection);
                return CSVOlustur(komut.ExecuteReader()); 
            }
        }

        private void VeriMenusu_Load(object sender, EventArgs e){} 
        private void label5_Click(object sender, EventArgs e)
        { 
        }

        private void tbExportPath_TextChanged(object sender, EventArgs e)
        { 
        }

        private void inputDizini_TextChanged(object sender, EventArgs e)
        { 
        }

        private void label3_Click(object sender, EventArgs e)
        { 
        } 

        private void btnVTYGozat_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog = new FolderBrowserDialog();

            if (folderbrowserdialog.ShowDialog() == DialogResult.OK)
            {
                yolbackup = folderbrowserdialog.SelectedPath;
                tbVTI.Text = yolbackup;
                btnVTYedekle.Enabled = true;
            }
        }

        private void btnVTYedekle_Click(object sender, EventArgs e)
        {
            try
            { 
                if (tbVTI.Text != "")
                {
                    DateTime now = DateTime.Now;  
                    SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true");
                    baglanti.Open();
                    string backupyol = @"BACKUP DATABASE [YurtOtomasyonu] TO  DISK ='" + yolbackup + @"\YurtOtomasyonu " + now.ToString("dd-MM-yyyy HH;mm;ss") + ".bak'";
                    komut = new SqlCommand(backupyol, baglanti);
                    komut.Connection = baglanti;
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    txtKontrol.Text = "Yedekleme Tamamlandı!";
                }
                else
                {
                    MessageBox.Show("Yedekle alınacak klasörü seçin!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void btnVTYDGozat_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                yolrestore = openFileDialog1.FileName;
                tbVTE.Text = yolrestore;
                btnVTYedektenDon.Enabled = true;

            }
        }

        private void inputDizini_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnVTYedektenDon_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbVTE.Text != "")
                {
                    SqlConnection baglantirestore = new SqlConnection(@"server=127.0.0.1;initial catalog=master;integrated security=true");
                    string restoreyol = @"alter database YurtOtomasyonu set offline with rollback immediate " +" \n "+ @"RESTORE DATABASE [YurtOtomasyonu] FROM  DISK  ='" + yolrestore + "' with replace" + "\n" + @"alter database YurtOtomasyonu set online";
                    komut = new SqlCommand(restoreyol, baglantirestore);
                    baglantirestore.Open();
                    komut.ExecuteNonQuery();
                    baglantirestore.Close();
                    txtKontrol.Text = "Yedekten dönme başarılı!";
                }
                else
                {
                    MessageBox.Show("Yedekten dönmek için dosya seçiniz!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtExportDizin_Click(object sender, EventArgs e){}
    } 
}
