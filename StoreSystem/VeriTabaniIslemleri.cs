using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace StoreSystem
{
    public class VeriTabaniIslemleri
    {
        SqlConnection conn;
        SqlCommand komut;
        SqlDataReader dr;
        private void baglan()
        {
            conn = new SqlConnection("Data Source=SERAPPC;Initial Catalog=DvdStore;Integrated Security=True");
            conn.Open();
            komut = new SqlCommand();
            komut.Connection = conn;
 
        }
        public Boolean Kirala(int dvdId, string kullananAd, string kullananSifre,DateTime tarih)
        {
            baglan();
            komut.CommandText = "select dvdStok from dvdBilgiler where dvdId=@id";
            komut.Parameters.AddWithValue("@id", dvdId);
            dr = komut.ExecuteReader();
            dr.Read();
            int stok = (int)dr["dvdStok"];
            conn.Close();
            if (stok == 0) 
                return false;
            else
            {
                baglan();
                komut.CommandText = "update dvdBilgiler set dvdStok=@stok where dvdId=@id";
                komut.Parameters.AddWithValue("@stok", stok - 1);
                komut.Parameters.AddWithValue("@id", dvdId);
                komut.ExecuteNonQuery();
                conn.Close();
                baglan();
                komut.CommandText = "update UserLogin set userDvdId=@id, kiralamaTarihi=@tarih where userName=@name and UserPassword=@password";
                komut.Parameters.AddWithValue("@id", dvdId);
                komut.Parameters.AddWithValue("@tarih", tarih.ToString("yyyy-MM-dd HH:mm:ss"));
                komut.Parameters.AddWithValue("@name", kullananAd);
                komut.Parameters.AddWithValue("@password", kullananSifre);
                komut.ExecuteNonQuery();
                conn.Close();
                return true;
            }
        }
        public Boolean Kiraladimi(string kullananAd,string kullananSifre)
        {
            baglan();
            komut.CommandText = "select userDvdId from UserLogin where userName=@name and UserPassword=@password";
            komut.Parameters.AddWithValue("@name", kullananAd);
            komut.Parameters.AddWithValue("@password", kullananSifre);
            dr = komut.ExecuteReader();
            dr.Read();
            string x=dr["userDvdId"].ToString();
            conn.Close();
            if (x=="")
            {
                return false;
            }
            else
                return true;
        }
        public Yonetici yoneticiAra(int key)
        {
            baglan();
            komut.CommandText = "select * from AdminLogin where adminId=@key";
            komut.Parameters.AddWithValue("@key", key);
            dr = komut.ExecuteReader();
            dr.Read();
            Yonetici y = new Yonetici();
            y.ad = dr["adminName"].ToString();
            y.sifre = dr["adminPassword"].ToString();
            conn.Close();
            return y;
        }
        public Kullanici KullaniciAra(int key)
        {
            baglan();
            komut.CommandText = "select * from UserLogin where userId=@key";
            komut.Parameters.AddWithValue("@key", key);
            dr = komut.ExecuteReader();
            dr.Read();
            Kullanici k = new Kullanici();
            k.ad = dr["userName"].ToString();
            k.sifre = dr["userPassword"].ToString();
            conn.Close();
            return k;

        }
        private void addWithValueHepsi(Dvd dvdGir)
        {
            komut.Parameters.AddWithValue("@name", dvdGir.name);
            komut.Parameters.AddWithValue("@duration", dvdGir.duration);
            komut.Parameters.AddWithValue("@price", dvdGir.price);
            komut.Parameters.AddWithValue("@category", dvdGir.category);
            komut.Parameters.AddWithValue("@stok", dvdGir.stok);

        }
        public void Ekle(Dvd eklenecekDvd)
        {
            baglan();
            komut.CommandText = "insert into dvdBilgiler(dvdName,dvdDuration,dvdPrice,dvdCategory,dvdStok) values(@name,@duration,@price,@category,@stok)";
           
            addWithValueHepsi(eklenecekDvd);
            komut.ExecuteNonQuery();
            conn.Close();
        }
        
        public int Guncelle(Dvd guncellenecekDvd)
        {
            baglan();
            komut.CommandText = "update dvdBilgiler set dvdName=@name, dvdDuration=@duration,dvdPrice=@price,dvdCategory=@category,dvdStok=@stok where dvdId=@id";
            addWithValueHepsi(guncellenecekDvd);
            komut.Parameters.AddWithValue("@id", guncellenecekDvd.id);
            int etkilenen=komut.ExecuteNonQuery();
            conn.Close();
            return etkilenen;
        }
        public int Sil(int id)
        {            
                baglan();
                komut.CommandText = "delete from dvdBilgiler where dvdId=@id";
                komut.Parameters.AddWithValue("@id", id);
               int etkilenen= komut.ExecuteNonQuery();
                conn.Close();
            return etkilenen;
        }
        public bool Ara(string ad)
        {
            baglan();
            komut.CommandText = "select * from dvdBilgiler where dvdName=@name";
            komut.Parameters.AddWithValue("@name", ad);
            dr = komut.ExecuteReader();
            bool sonuc = dr.Read();
            conn.Close();
            return sonuc;
        }
        public Dvd Ara(int id)
        {
            Dvd dvd1 = new Dvd();
            baglan();
            komut.CommandText = "select * from dvdBilgiler where dvdId=@id";
            komut.Parameters.AddWithValue("@id", id);
            dr = komut.ExecuteReader();
            dr.Read();
            dvd1.name = dr.GetString(1);
            dvd1.category = dr.GetString(4);
            dvd1.duration = dr.GetInt32(2);
            dvd1.id = id;
            dvd1.price = dr.GetDouble(3);
            dvd1.stok = dr.GetInt32(5);
            conn.Close();
            return dvd1;
        }
        public Boolean DvdVarMi(int id)
        {
            baglan();
            komut.CommandText = "select dvdId from dvdBilgiler where dvdId=@id";
            komut.Parameters.AddWithValue("@id", id);
            dr = komut.ExecuteReader();
            bool varmi = dr.Read();
            conn.Close();
            if (varmi)
                return true;
            else
                return false;
        }
        
        /*
        public List<Dvd> Listele()
        {
            List<Dvd> liste = new List<Dvd>();
            conn.Open();
            string CommandText = "select * from dvdBilgiler";
            SqlCommand komut = new SqlCommand(CommandText,conn);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Dvd dvd1 = new Dvd();
                dvd1.category = dr["dvdCategory"].ToString();
                dvd1.duration = Convert.ToInt32(dr["dvdDuration"]);
                dvd1.id = Convert.ToInt32(dr["dvdPrice"]);
                dvd1.name = dr["dvdName"].ToString();
                dvd1.price = Convert.ToDouble(dr["dvdPrice"]);
                liste.Add(dvd1);
            }
            return liste;
        }
        */
    }
}
