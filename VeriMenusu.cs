using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace YurtOtomasyonu
{
    public partial class VeriMenusu : Form
    {

        public VeriMenusu()
        {
            InitializeComponent();
        }
        string path;
        private void VeriMenusu_Load(object sender, EventArgs e)
        {

        }

        public void ImportDataFromExcel(string excelFilePath)
        {
            /*
            //declare variables - edit these based on your particular situation
            string ssqltable = "Calisan";
            // make sure your sheet name is correct, here sheet name is sheet1,  so you can change your sheet name if have    different
            string myexceldataquery = "select id,username,pass,vasif,ad,soyad,tckimlik from [Çalışan$]";
            //create our connection strings
            string sexcelconnectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 12.0;";
            string ssqlconnectionstring = "Data Source=127.0.0.1;Initial Catalog=YurtOtomasyonu;Integrated Security=True"; 
            //series of commands to bulk copy data from the excel file into our sql table
            OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
            OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
            oledbconn.Open();
            OleDbDataReader dr = oledbcmd.ExecuteReader();
            SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
            bulkcopy.DestinationTableName = ssqltable;
            while (dr.Read())
            {
                bulkcopy.WriteToServer(dr);
            }
            dr.Close();
            oledbconn.Close();
            txtKontrol.Text = "File imported into sql server successfully."; */


        }

        public void btngozat_Click(object sender, EventArgs e)
        {
        }

        public void buttonImp_Click(object sender, EventArgs e)
        {
        }

        private void dosyaAdi_Click(object sender, EventArgs e)
        {

        }
    }
}
