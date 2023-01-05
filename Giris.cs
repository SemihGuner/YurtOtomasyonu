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
    public partial class Giris : Form
    {
        SqlConnection baglanti = new SqlConnection(@"server=127.0.0.1;initial catalog=YurtOtomasyonu;integrated security=true");  
        SqlDataReader reader; 

        public Giris()
        {
            InitializeComponent();
        }

        private void BaglantiAc()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
        }

        private void girisButonu_Click(object sender, EventArgs e)
        {
            // Stored Procedure'suz

            SqlCommand sqlCommand = new SqlCommand("SELECT * from Calisan where username='" + textBox1.Text + "' and pass='" + textBox2.Text + "'", baglanti);
            BaglantiAc();
            reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                AnaMenu anamenu = new AnaMenu();
                this.Hide();
                anamenu.ShowDialog();
                this.Close();
            }
            else 
            {
                MessageBox.Show("Hatalı giriş yaptınız");
            }
            baglanti.Close(); 

            //Stored Procedure'lu
            /*
            SqlCommand sqlCommand = new SqlCommand("girisyap", baglanti);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@username",textBox1.Text);
            sqlCommand.Parameters.AddWithValue("@pw",textBox2.Text);
            BaglantiAc();
            SqlParameter r = sqlCommand.Parameters.Add("@return", SqlDbType.Int);
            r.Direction = ParameterDirection.ReturnValue; 
            reader = sqlCommand.ExecuteReader();
            if (Convert.ToInt32( r.Value) == 0)
            {
                AnaMenu anamenu = new AnaMenu();
                this.Hide();
                anamenu.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hatalı giriş yaptınız");
            }
            reader.Close();
            baglanti.Close();
            */
        }

        private void girisButonu_Click_1(object sender, EventArgs e)
        {

        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}
