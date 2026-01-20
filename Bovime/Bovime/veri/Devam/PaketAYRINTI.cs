using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class PaketAYRINTI : Bilesen
    {

        public PaketAYRINTI()
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
            return bossaDoldur(paketAdi);
        }



        public async static Task<PaketAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                PaketAYRINTI sonuc = new PaketAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.PaketAYRINTIs.FirstOrDefaultAsync(p => p.paketKimlik == kimlik && p.varmi == true);
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

        public static async Task<List<PaketAYRINTI>> ara(params Expression<Func<PaketAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.PaketAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<PaketAYRINTI>> ara(veri.Varlik vari, params Expression<Func<PaketAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.PaketAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<PaketAYRINTI?> bul(veri.Varlik vari, params Expression<Func<PaketAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.PaketAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "PaketAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "PaketAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "paketKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.paketKimlik;
        }


        #endregion


    }
}

