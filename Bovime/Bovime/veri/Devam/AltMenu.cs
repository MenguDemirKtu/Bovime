using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class AltMenu : Bilesen
    {

        public AltMenu()
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
            return bossaDoldur(altMenuBaslik);
        }



        public async static Task<AltMenu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                AltMenu sonuc = new AltMenu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.AltMenus.FirstOrDefaultAsync(p => p.altMenukimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.AltMenuCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.AltMenuCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<AltMenu>> ara(params Expression<Func<AltMenu, bool>>[] kosullar)
        {
            return await veriTabani.AltMenuCizelgesi.ara(kosullar);
        }
        public static async Task<List<AltMenu>> ara(veri.Varlik vari, params Expression<Func<AltMenu, bool>>[] kosullar)
        {
            return await veriTabani.AltMenuCizelgesi.ara(vari, kosullar);
        }
        public static async Task<AltMenu?> bul(veri.Varlik vari, params Expression<Func<AltMenu, bool>>[] kosullar)
        {
            return await veriTabani.AltMenuCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "AltMenu";
        }


        public override string _turkceAdi()
        {
            return "Alt Menü";
        }
        public override string _birincilAnahtarAdi()
        {
            return "altMenukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.altMenukimlik;
        }


        #endregion


    }
}

