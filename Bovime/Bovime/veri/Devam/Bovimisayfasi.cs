using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class Bovimisayfasi : Bilesen
    {

        public Bovimisayfasi()
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
            return bossaDoldur(bovimiSayfasiBaslik);
        }



        public async static Task<Bovimisayfasi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Bovimisayfasi sonuc = new Bovimisayfasi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Bovimisayfasis.FirstOrDefaultAsync(p => p.bovimisayfasikimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.BovimisayfasiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.BovimisayfasiCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_yayindaMi = true;
            this.varmi = true;
        }

        public static async Task<List<Bovimisayfasi>> ara(params Expression<Func<Bovimisayfasi, bool>>[] kosullar)
        {
            return await veriTabani.BovimisayfasiCizelgesi.ara(kosullar);
        }
        public static async Task<List<Bovimisayfasi>> ara(veri.Varlik vari, params Expression<Func<Bovimisayfasi, bool>>[] kosullar)
        {
            return await veriTabani.BovimisayfasiCizelgesi.ara(vari, kosullar);
        }
        public static async Task<Bovimisayfasi?> bul(veri.Varlik vari, params Expression<Func<Bovimisayfasi, bool>>[] kosullar)
        {
            return await veriTabani.BovimisayfasiCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Bovimisayfasi";
        }


        public override string _turkceAdi()
        {
            return "BovimiSayfasi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "bovimisayfasikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.bovimisayfasikimlik;
        }


        #endregion


    }
}

