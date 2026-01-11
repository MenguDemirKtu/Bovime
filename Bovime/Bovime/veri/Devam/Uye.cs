using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class Uye : Bilesen
    {

        public Uye()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return bossaDoldur(adi);
        }



        public async static Task<Uye?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Uye sonuc = new Uye();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Uyes.FirstOrDefaultAsync(p => p.uyekimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.UyeCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.UyeCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<Uye>> ara(params Expression<Func<Uye, bool>>[] kosullar)
        {
            return await veriTabani.UyeCizelgesi.ara(kosullar);
        }
        public static async Task<List<Uye>> ara(veri.Varlik vari, params Expression<Func<Uye, bool>>[] kosullar)
        {
            return await veriTabani.UyeCizelgesi.ara(vari, kosullar);
        }
        public static async Task<Uye?> bul(veri.Varlik vari, params Expression<Func<Uye, bool>>[] kosullar)
        {
            return await veriTabani.UyeCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Uye";
        }


        public override string _turkceAdi()
        {
            return "Üye";
        }
        public override string _birincilAnahtarAdi()
        {
            return "uyekimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.uyekimlik;
        }


        #endregion


    }
}

