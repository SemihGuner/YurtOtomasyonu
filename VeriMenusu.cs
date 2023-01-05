using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace YurtOtomasyonu
{
    public partial class VeriMenusu : Form
    {
        string yol; 
        SqlConnection dbConnection = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true");

        public VeriMenusu()
        {
            InitializeComponent();
        }
        private void VeriMenusu_Load(object sender, EventArgs e)
        {

        }

        public void ImportDataFromExcel(string excelFilePath)
        {
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
                dosyaAdi.Text = yol;
            }
        }
         

        public void buttonImp_Click(object sender, EventArgs e)
        {
            TabledanSQLe(CSVdenTable(yol));
        }

        private void dosyaAdi_Click(object sender, EventArgs e)
        {

        }

        

        private void txtExportDizin_Click(object sender, EventArgs e)
        {

        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            string expYol = tbExportPath.Text.ToString(); 
            
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
                    s.DestinationTableName = "Calisan";

                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());

                    s.WriteToServer(csvFileData);
                }
            } 
            txtKontrol.Text = "Import tamamlandı.";
        }
    }
}
