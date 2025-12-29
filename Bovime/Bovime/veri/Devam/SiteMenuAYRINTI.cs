using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SiteMenuAYRINTI : Bilesen
    {

        public SiteMenuAYRINTI()
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



        public async static Task<SiteMenuAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SiteMenuAYRINTI sonuc = new SiteMenuAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SiteMenuAYRINTIs.FirstOrDefaultAsync(p => p.siteMenukimlik == kimlik && p.varmi == true);
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

        public static async Task<List<SiteMenuAYRINTI>> ara(params Expression<Func<SiteMenuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SiteMenuAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<SiteMenuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SiteMenuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SiteMenuAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SiteMenuAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SiteMenu";
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

