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
            List<string> lines = new List<string>();

            string headerLine = "";
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns[i] = reader.GetName(i);
                }
                headerLine = string.Join(";",columns);
                lines.Add(headerLine);
            }
            while (reader.Read())
            {
                object[] val = new object[reader.FieldCount];
                reader.GetValues(val);
                lines.Add(string.Join(";", val));
            }
            try
            { 
                System.IO.File.WriteAllLines(expYol, lines);
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata: " + e.Message);
            }
            return expYol;
        }
         
        private string getCSV()
        {
            using (dbConnection)
            {
                BaglantiAc();
                return CSVOlustur(new SqlCommand("select * from Ogrenci", dbConnection).ExecuteReader()); 
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
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbVTI.Text = fbd.SelectedPath;
                btnVTYedekle.Enabled = true;
            }
        }

        private void btnVTYedekle_Click(object sender, EventArgs e)
        {
            /*
            string database = dbConnection.Database.ToString();
            if (tbVTI.Text==string.Empty)
            {
                MessageBox.Show("Lütfen lokasyon seçiniz.");
            }
            else
            {
                string cmd = "BACKUP DATABASE [" + database + "] TO DISK '" + tbVTI.Text + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH--mm--ss")+".bak'" ;
                BaglantiAc();
                SqlCommand command = new SqlCommand(cmd, dbConnection);
                command.ExecuteNonQuery();
                txtKontrol.Text = "Yedekleme tamamlandı.";
                dbConnection.Close();
                btnVTIImport.Enabled = false;
            }
            */
        }

        private void btnVTYDGozat_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL Server Database Backup File|.bak";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbVTE.Text = dlg.FileName;
                btnVTYedektenDon.Enabled = true;
            }
        }

        private void btnVTYedektenDon_Click(object sender, EventArgs e)
        {

        }

        private void txtExportDizin_Click(object sender, EventArgs e){}
    } 
}
