using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaKampanyaTalebi : Bilesen
    {

        public FirmaKampanyaTalebi()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {
            if (e_gorulduMu == null)
                e_gorulduMu = false;

            if (girisTarihi == null)
                girisTarihi = DateTime.Now;
        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaKimlik, "Firma", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_firmaKimlik);
        }



        public async static Task<FirmaKampanyaTalebi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaKampanyaTalebi sonuc = new FirmaKampanyaTalebi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaKampanyaTalebis.FirstOrDefaultAsync(p => p.firmaKampanyaTalebikimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.FirmaKampanyaTalebiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.FirmaKampanyaTalebiCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_gorulduMu = false;
            this.i_kampanyaDurumuKimlik = (int)enumref_KampanyaDurumu.Firma_Girdi_Bekliyor;
        }

        public static async Task<List<FirmaKampanyaTalebi>> ara(params Expression<Func<FirmaKampanyaTalebi, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyaTalebiCizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaKampanyaTalebi>> ara(veri.Varlik vari, params Expression<Func<FirmaKampanyaTalebi, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyaTalebiCizelgesi.ara(vari, kosullar);
        }
        public static async Task<FirmaKampanyaTalebi?> bul(veri.Varlik vari, params Expression<Func<FirmaKampanyaTalebi, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyaTalebiCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaKampanyaTalebi";
        }


        public override string _turkceAdi()
        {
            return "Firma Kampanya Talebi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmaKampanyaTalebikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmaKampanyaTalebikimlik;
        }


        #endregion


    }
}

