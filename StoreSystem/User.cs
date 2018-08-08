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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        public int UserKey;
        Kullanici kullanan;
        SqlConnection conn;
        string conStr = "Data Source=SERAPPC;Initial Catalog=DvdStore;Integrated Security=True";
        SqlCommand command;
        VeriTabaniIslemleri islem;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void listele()
        {
            conn = new SqlConnection(conStr);
            conn.Open();
            command = new SqlCommand("select * from dvdBilgiler",conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }
        private void User_Load(object sender, EventArgs e)
        {
            islem = new VeriTabaniIslemleri();
            kullanan = islem.KullaniciAra(UserKey);
            lblAd.Text = kullanan.ad.ToUpper();
            listele();
        }

        private void btnKirala_Click(object sender, EventArgs e)
        {
            try
            {
            int id = Convert.ToInt32(txtIdKirala.Text);
                if (islem.DvdVarMi(id))
                {
                    if (islem.Kiraladimi(kullanan.ad, kullanan.sifre))
                        MessageBox.Show("Kiraladığınız DVD'yi iade etmeden yeni bir DVD kiralayamazsınız!");
                    else
                    {
                        DateTime tarih = DateTime.Now;
                        if (islem.Kirala(id, kullanan.ad, kullanan.sifre,tarih))
                        {
                            lblTarih.Text = tarih.ToString();

                            MessageBox.Show("DVD kiralandı");
                            listele();
                        }
                        else
                        {
                            MessageBox.Show("Stokta DVD kalmamış");
                        }

                    }
                }
                else
                {
                    MessageBox.Show("bu ID ile tanımlanmış DVD bulunmamaktadır");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("uygun bir değer giriniz");
            }
            
        }
    }
}
