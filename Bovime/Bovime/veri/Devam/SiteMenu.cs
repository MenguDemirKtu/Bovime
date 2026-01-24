using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SiteMenu : Bilesen
    {

        public SiteMenu()
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
            return bossaDoldur(siteMenuAdi);
        }



        public async static Task<SiteMenu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SiteMenu sonuc = new SiteMenu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SiteMenus.FirstOrDefaultAsync(p => p.siteMenukimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.SiteMenuCizelgesi.kaydetKos(this, vari, yedeklensinmi);

        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SiteMenuCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_altMenuMu = true;
            this.varmi = true;
        }

        public static async Task<List<SiteMenu>> ara(params Expression<Func<SiteMenu, bool>>[] kosullar)
        {
            return await veriTabani.SiteMenuCizelgesi.ara(kosullar);
        }
        public static async Task<List<SiteMenu>> ara(veri.Varlik vari, params Expression<Func<SiteMenu, bool>>[] kosullar)
        {
            return await veriTabani.SiteMenuCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SiteMenu";
        }


        public override string _turkceAdi()
        {
            return "Site Menü";
        }
        public override string _birincilAnahtarAdi()
        {
            return "siteMenukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.siteMenukimlik;
        }


        #endregion


    }
}

