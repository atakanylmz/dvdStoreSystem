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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        public int AdminKey;
        Yonetici YonetenBirey;
        SqlDataAdapter da;
        SqlConnection con;
        SqlCommand comm;
        DataSet ds;
        DataTable bulunan;
        VeriTabaniIslemleri islem = new VeriTabaniIslemleri();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Dvd dvd1 = new Dvd();
                dvd1.category = txtCategory.Text;
                dvd1.duration = Convert.ToInt32(txtDuration.Text);
                dvd1.name = txtAd.Text;
                dvd1.price = Convert.ToInt32(txtPrice.Text);
                dvd1.stok = Convert.ToInt32(txtStok.Text);
                if (islem.Ara(dvd1.name))
                    MessageBox.Show("bu isimle dvd kaydı daha önceden oluşturulmuş");
                else
                    islem.Ekle(dvd1);
            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı alan girişi", "Hata");
            }
            listele();
        }

       
        public void listele()
        {
            con = new SqlConnection("Data Source=SERAPPC; Initial Catalog=DvdStore; Integrated Security=true");
            con.Open();
            comm = new SqlCommand("select * from dvdBilgiler", con);
            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds,"dvdBilgiler");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["dvdId"].HeaderText = "ID";
            dataGridView1.Columns["dvdName"].HeaderText = "AD";
            dataGridView1.Columns["dvdDuration"].HeaderText = "SÜRE";
            dataGridView1.Columns["dvdPrice"].HeaderText = "FİYAT";
            dataGridView1.Columns["dvdCategory"].HeaderText = "KATEGORİ";
            dataGridView1.Columns["dvdStok"].HeaderText = "STOK";
            con.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele(); 
/*          
            string temp = "";
            List < Dvd > listem= islem.Listele();
            foreach (Dvd item in listem)
            {
                temp += " name: " + item.name + " id: " + item.id + " category: " + item.category + " duration: " + item.duration + " price: " + item.price + Environment.NewLine;
            }
            txtListe.Text = temp;
*/
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                Dvd gnclDvd = new Dvd();
                gnclDvd.category = txtCategory.Text;
                gnclDvd.duration = Convert.ToInt32(txtDuration.Text);
                gnclDvd.id = Convert.ToInt32(txtId.Text);
                gnclDvd.name = txtAd.Text;
                gnclDvd.price = Convert.ToDouble(txtPrice.Text);
                gnclDvd.stok = Convert.ToInt32(txtStok.Text);
                int guncellenen=islem.Guncelle(gnclDvd);
                if (guncellenen > 0)
                    MessageBox.Show("kayıt güncellendi");
                else
                    MessageBox.Show("kayıt bulunamadı");
            }
            catch (Exception)
            {
                MessageBox.Show("güncellenecek bilgiler hatalı girildi");
            }
            listele();
        }
       
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int silinen=islem.Sil(Convert.ToInt32(txtId.Text));
                if (silinen > 0)
                    MessageBox.Show("kayıt silindi");
                else
                    MessageBox.Show("id kaydı bulunamadı");
            }
            catch (Exception)
            {

                MessageBox.Show("hatalı id girişi");
            }
            listele();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            try
            {
                bulunan = new DataTable();
                int id = Convert.ToInt32(txtId.Text);
                Dvd bulunanDvd = islem.Ara(id);
                dataTableOlustur();
                DataRow satir = bulunan.NewRow();
                satir["ID"] = id.ToString();
                satir["AD"] = bulunanDvd.name;
                satir["KATEGORİ"] = bulunanDvd.category;
                satir["SÜRE"] = bulunanDvd.duration.ToString();
                satir["FİYAT"] = bulunanDvd.price.ToString();
                satir["STOK"] = bulunanDvd.stok.ToString();
                bulunan.Rows.Add(satir);
                dataGridView1.DataSource = bulunan;
            }
            catch (Exception)
            {

                MessageBox.Show("hatalı id girişi") ;
            }
        }
        private void dataTableOlustur()
        {
            bulunan.Columns.Add("ID");
            bulunan.Columns.Add("AD");
            bulunan.Columns.Add("KATEGORİ");
            bulunan.Columns.Add("SÜRE");
            bulunan.Columns.Add("FİYAT");
            bulunan.Columns.Add("STOK");
        }

        private void btnSecilenSil_Click(object sender, EventArgs e)
        {
            int silinenTop = 0;
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                int SilinecekId = Convert.ToInt16(item.Cells[0].Value);
                silinenTop+=islem.Sil(SilinecekId);
            }
            listele();
            MessageBox.Show(silinenTop.ToString() + " tane kayıt silindi");
        }

        public void Admin_Load(object sender, EventArgs e)
        {
            YonetenBirey = islem.yoneticiAra(AdminKey);
            lblYoneticiAd.Text = YonetenBirey.ad.ToUpper();
            listele();
        }

        
        
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
                Dvd dvd1 = new Dvd();
                dvd1.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                dvd1.name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                dvd1.duration = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                dvd1.price = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
                dvd1.category = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                dvd1.stok = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
                islem.Guncelle(dvd1);
            MessageBox.Show("kayıtlar güncellendi");
            
        }
    }
}
