using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class Hakkimizda : Bilesen
    {

        public Hakkimizda()
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



        public async static Task<Hakkimizda?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Hakkimizda sonuc = new Hakkimizda();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Hakkimizdas.FirstOrDefaultAsync(p => p.hakkimizdakimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.HakkimizdaCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.HakkimizdaCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<Hakkimizda>> ara(params Expression<Func<Hakkimizda, bool>>[] kosullar)
        {
            return await veriTabani.HakkimizdaCizelgesi.ara(kosullar);
        }
        public static async Task<List<Hakkimizda>> ara(veri.Varlik vari, params Expression<Func<Hakkimizda, bool>>[] kosullar)
        {
            return await veriTabani.HakkimizdaCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Hakkimizda";
        }


        public override string _turkceAdi()
        {
            return "Hakkımızda";
        }
        public override string _birincilAnahtarAdi()
        {
            return "hakkimizdakimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.hakkimizdakimlik;
        }


        #endregion


    }
}

