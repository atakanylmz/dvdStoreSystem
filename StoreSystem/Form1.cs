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

namespace StoreSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdminGiris_Click(object sender, EventArgs e)
        {
            string ad = txtUserName.Text;
            string sifre = txtSifre.Text;
            bool check = chBoxAdmin.Checked;
          /*  Form1Veritabani frmIslem = new Form1Veritabani();
            if (!frmIslem.GirisYap(ad, sifre, check))
                MessageBox.Show("kayıt bulunamadı!");
            else
                this.Hide();
           */ if(check==false)
            {
                SqlConnection connUser = new SqlConnection("Data Source=SERAPPC; Initial Catalog=DvdStore;Integrated Security=True");
                connUser.Open();
                string commandTextUser = "select * from UserLogin where userName=@ad and userPassword=@sifre";
                SqlCommand commandUser = new SqlCommand();
                commandUser.CommandText = commandTextUser;
                commandUser.Connection = connUser;
                commandUser.Parameters.AddWithValue("@ad", ad);
                commandUser.Parameters.AddWithValue("@sifre", sifre);
                SqlDataReader dr = commandUser.ExecuteReader();
                if (dr.Read())
                {
                    User Kullanici = new User();
                    Kullanici.UserKey = Convert.ToInt32(dr["userId"]);
                    Kullanici.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("kayıt bulunamadı");
                connUser.Close();
            }
            else if(check == true)
            {
                SqlConnection connAdmin = new SqlConnection(" Data Source=SERAPPC;Initial Catalog=DvdStore;Integrated Security=True");
                connAdmin.Open();
                string commandTextAdmin = "select * from AdminLogin where adminName=@adi and adminPassword=@sifresi";
                SqlCommand commandAdmin = new SqlCommand(commandTextAdmin, connAdmin);
                commandAdmin.Parameters.AddWithValue("@adi", ad);
                commandAdmin.Parameters.AddWithValue("@sifresi", sifre);
                SqlDataReader dr = commandAdmin.ExecuteReader();
                if (dr.Read())
                {
                    Admin Yonetici = new Admin();
                    Yonetici.AdminKey=Convert.ToInt32(dr["adminId"]);
                    Yonetici.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("kayıt bulunamadı");
                connAdmin.Close();
            }
        }
    }
}
