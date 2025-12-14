using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SiteDuyuruAYRINTI : Bilesen
    {

        public SiteDuyuruAYRINTI()
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
            return bossaDoldur(siteDuyuruBasligi);
        }



        public async static Task<SiteDuyuruAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SiteDuyuruAYRINTI sonuc = new SiteDuyuruAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SiteDuyuruAYRINTIs.FirstOrDefaultAsync(p => p.siteDuyurukimlik == kimlik && p.varmi == true);
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

        public static async Task<List<SiteDuyuruAYRINTI>> ara(params Expression<Func<SiteDuyuruAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SiteDuyuruAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<SiteDuyuruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SiteDuyuruAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SiteDuyuruAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SiteDuyuruAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SiteDuyuruAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "siteDuyurukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.siteDuyurukimlik;
        }


        #endregion


    }
}

