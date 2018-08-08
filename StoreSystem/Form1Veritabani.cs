using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace StoreSystem
{
    public class Form1Veritabani
    {
       /* SqlConnection conn;
        SqlCommand command;
        string commandAdmin = "select * from AdminLogin where adminName=@ad and adminPassword=@sifre";
        string commandUser = "select * from UserLogin where userName=@ad and userPassword=@sifre";
        private void baglan()
        {
            conn = new SqlConnection("Data Source=SERAPPC; Initial Catalog=DvdStore;Integrated Security=True");
            conn.Open();
        }
        public bool GirisYap(string ad,string sifre,bool chk)
        {
            baglan();
            command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = (chk == true) ? commandAdmin : commandUser;
            command.Parameters.AddWithValue("@ad", ad);
            command.Parameters.AddWithValue("@sifre", sifre);
            SqlDataReader dr = command.ExecuteReader();
            if(dr.Read())
            {
                conn.Close();
                if(chk==true)
                {
                    Admin Yonetici = new Admin();
                    Yonetici.Show();
                }
                else
                {
                    User Kullanici = new User();
                    Kullanici.Show();
                }
            }
            else
            {
                conn.Close();
                return false;
            }
            return true;
        }
    */
    }
}
