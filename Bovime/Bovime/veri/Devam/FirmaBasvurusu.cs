using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaBasvurusu : Bilesen
    {

        public FirmaBasvurusu()
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
            return bossaDoldur(firmaAdi);
        }



        public async static Task<FirmaBasvurusu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaBasvurusu sonuc = new FirmaBasvurusu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaBasvurusus.FirstOrDefaultAsync(p => p.firmaBasvurusukimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.FirmaBasvurusuCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.FirmaBasvurusuCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_gorulduMu = false;
            this.varmi = true;
        }

        public static async Task<List<FirmaBasvurusu>> ara(params Expression<Func<FirmaBasvurusu, bool>>[] kosullar)
        {
            return await veriTabani.FirmaBasvurusuCizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaBasvurusu>> ara(veri.Varlik vari, params Expression<Func<FirmaBasvurusu, bool>>[] kosullar)
        {
            return await veriTabani.FirmaBasvurusuCizelgesi.ara(vari, kosullar);
        }
        public static async Task<FirmaBasvurusu?> bul(veri.Varlik vari, params Expression<Func<FirmaBasvurusu, bool>>[] kosullar)
        {
            return await veriTabani.FirmaBasvurusuCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaBasvurusu";
        }


        public override string _turkceAdi()
        {
            return "Firma Basvurusu";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmaBasvurusukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmaBasvurusukimlik;
        }


        #endregion


    }
}

