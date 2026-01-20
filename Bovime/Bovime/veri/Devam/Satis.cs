using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class Satis : Bilesen
    {

        public Satis()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

            if (string.IsNullOrEmpty(this.kodu))
                this.kodu = Guid.NewGuid().ToString();
        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaKimlik, "Firma", dilKimlik);
        }




        [NotMapped]
        public enumref_SatisDurumu _Satisdurumu
        {
            get
            {
                return (enumref_SatisDurumu)this.i_satisDurumuKimlik;
            }
            set
            {
                i_satisDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_firmaKimlik);
        }



        public async static Task<Satis?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Satis sonuc = new Satis();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Satiss.FirstOrDefaultAsync(p => p.satisKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.SatisCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SatisCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.varmi = true;
        }

        public static async Task<List<Satis>> ara(params Expression<Func<Satis, bool>>[] kosullar)
        {
            return await veriTabani.SatisCizelgesi.ara(kosullar);
        }
        public static async Task<List<Satis>> ara(veri.Varlik vari, params Expression<Func<Satis, bool>>[] kosullar)
        {
            return await veriTabani.SatisCizelgesi.ara(vari, kosullar);
        }
        public static async Task<Satis?> bul(veri.Varlik vari, params Expression<Func<Satis, bool>>[] kosullar)
        {
            return await veriTabani.SatisCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Satis";
        }


        public override string _turkceAdi()
        {
            return "Satış";
        }
        public override string _birincilAnahtarAdi()
        {
            return "satisKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.satisKimlik;
        }


        #endregion


    }
}

