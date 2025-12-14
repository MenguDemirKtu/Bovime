using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class ReklamKusagi1AYRINTI : Bilesen
    {

        public ReklamKusagi1AYRINTI()
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



        public async static Task<ReklamKusagi1AYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ReklamKusagi1AYRINTI sonuc = new ReklamKusagi1AYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ReklamKusagi1AYRINTIs.FirstOrDefaultAsync(p => p.reklamKusagi1kimlik == kimlik && p.varmi == true);
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

        public static async Task<List<ReklamKusagi1AYRINTI>> ara(params Expression<Func<ReklamKusagi1AYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.ReklamKusagi1AYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<ReklamKusagi1AYRINTI>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi1AYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.ReklamKusagi1AYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ReklamKusagi1AYRINTI";
        }


        public override string _turkceAdi()
        {
            return "ReklamKusagi1";
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

