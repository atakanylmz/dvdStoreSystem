using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem
{
    public class MultiMedyaListe
    {
        public List<MultiMedya> Medyalar = new List<MultiMedya>();
        public void MedyaEkle(MultiMedya Medya)
        {
            Medyalar.Add(Medya);
        }
        public void MedyaSil(int MedyaId)
        {
            Boolean durum = false;
            MultiMedya SilinecekMedya;
            foreach (MultiMedya item in Medyalar)
            {
                if (item.id == MedyaId)
                {
                    SilinecekMedya = item;
                    durum = true;
                    Medyalar.Remove(SilinecekMedya);
                    break;
                }
            }
            if (durum == false)
                throw new Exception("medya bulunamadı");
        }
        public string ListeyiGoster()
        {
            string temp = "";
            foreach (MultiMedya item in Medyalar)
            {
                temp += " ad: " + item.name + " id: " + item.name + " katagori: " + item.category + " süre: " + item.duration + " fiyat: " + item.price
                    + Environment.NewLine + Environment.NewLine;
            }
            return temp;
        }
        public void UpdateMedya()
        {

        }
    }
}
