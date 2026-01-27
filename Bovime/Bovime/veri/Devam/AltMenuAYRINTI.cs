using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class AltMenuAYRINTI : Bilesen
    {

        public AltMenuAYRINTI()
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



        public async static Task<AltMenuAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                AltMenuAYRINTI sonuc = new AltMenuAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.AltMenuAYRINTIs.FirstOrDefaultAsync(p => p.altMenukimlik == kimlik && p.varmi == true);
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

        public static async Task<List<AltMenuAYRINTI>> ara(params Expression<Func<AltMenuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.AltMenuAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<AltMenuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<AltMenuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.AltMenuAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<AltMenuAYRINTI?> bul(veri.Varlik vari, params Expression<Func<AltMenuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.AltMenuAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "AltMenuAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "AltMenu";
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

