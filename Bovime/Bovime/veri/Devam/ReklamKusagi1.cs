using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class ReklamKusagi1 : Bilesen
    {

        public ReklamKusagi1()
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
            return bossaDoldur(baslik);
        }



        public async static Task<ReklamKusagi1?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ReklamKusagi1 sonuc = new ReklamKusagi1();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ReklamKusagi1s.FirstOrDefaultAsync(p => p.reklamKusagi1kimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.ReklamKusagi1Cizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.ReklamKusagi1Cizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<ReklamKusagi1>> ara(params Expression<Func<ReklamKusagi1, bool>>[] kosullar)
        {
            return await veriTabani.ReklamKusagi1Cizelgesi.ara(kosullar);
        }
        public static async Task<List<ReklamKusagi1>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi1, bool>>[] kosullar)
        {
            return await veriTabani.ReklamKusagi1Cizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ReklamKusagi1";
        }


        public override string _turkceAdi()
        {
            return "Reklam Kuşağı 1";
        }
        public override string _birincilAnahtarAdi()
        {
            return "reklamKusagi1kimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.reklamKusagi1kimlik;
        }


        #endregion


    }
}

