using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class Manset : Bilesen
    {

        public Manset()
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
            return bossaDoldur(baslikUst);
        }



        public async static Task<Manset?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Manset sonuc = new Manset();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Mansets.FirstOrDefaultAsync(p => p.mansetkimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.MansetCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.MansetCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_yayindami = true;
        }

        public static async Task<List<Manset>> ara(params Expression<Func<Manset, bool>>[] kosullar)
        {
            return await veriTabani.MansetCizelgesi.ara(kosullar);
        }
        public static async Task<List<Manset>> ara(veri.Varlik vari, params Expression<Func<Manset, bool>>[] kosullar)
        {
            return await veriTabani.MansetCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Manset";
        }


        public override string _turkceAdi()
        {
            return "Manşet";
        }
        public override string _birincilAnahtarAdi()
        {
            return "mansetkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.mansetkimlik;
        }


        #endregion


    }
}

