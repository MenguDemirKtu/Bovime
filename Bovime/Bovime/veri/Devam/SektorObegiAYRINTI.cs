using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SektorObegiAYRINTI : Bilesen
    {

        public SektorObegiAYRINTI()
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
            return bossaDoldur(sektorObegiAdi);
        }



        public async static Task<SektorObegiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SektorObegiAYRINTI sonuc = new SektorObegiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SektorObegiAYRINTIs.FirstOrDefaultAsync(p => p.sektorObegikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<SektorObegiAYRINTI>> ara(params Expression<Func<SektorObegiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SektorObegiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<SektorObegiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SektorObegiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SektorObegiAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SektorObegiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SektorObegi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sektorObegikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sektorObegikimlik;
        }


        #endregion


    }
}

