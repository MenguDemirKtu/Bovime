using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class ReklamKusagi2AYRINTI : Bilesen
    {

        public ReklamKusagi2AYRINTI()
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



        public async static Task<ReklamKusagi2AYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ReklamKusagi2AYRINTI sonuc = new ReklamKusagi2AYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ReklamKusagi2AYRINTIs.FirstOrDefaultAsync(p => p.reklamKusagi2kimlik == kimlik && p.varmi == true);
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

        public static async Task<List<ReklamKusagi2AYRINTI>> ara(params Expression<Func<ReklamKusagi2AYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.ReklamKusagi2AYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<ReklamKusagi2AYRINTI>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi2AYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.ReklamKusagi2AYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ReklamKusagi2AYRINTI";
        }


        public override string _turkceAdi()
        {
            return "ReklamKusagi2";
        }
        public override string _birincilAnahtarAdi()
        {
            return "reklamKusagi2kimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.reklamKusagi2kimlik;
        }


        #endregion


    }
}

