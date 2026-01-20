using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class BovimisayfasiAYRINTI : Bilesen
    {

        public BovimisayfasiAYRINTI()
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



        public async static Task<BovimisayfasiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                BovimisayfasiAYRINTI sonuc = new BovimisayfasiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.BovimisayfasiAYRINTIs.FirstOrDefaultAsync(p => p.bovimisayfasikimlik == kimlik && p.varmi == true);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
        }

        public static async Task<List<BovimisayfasiAYRINTI>> ara(params Expression<Func<BovimisayfasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.BovimisayfasiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<BovimisayfasiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<BovimisayfasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.BovimisayfasiAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<BovimisayfasiAYRINTI?> bul(veri.Varlik vari, params Expression<Func<BovimisayfasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.BovimisayfasiAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "BovimisayfasiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Bovimisayfasi";
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

