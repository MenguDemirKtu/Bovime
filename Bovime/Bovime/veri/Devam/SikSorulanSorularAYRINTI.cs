using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SikSorulanSorularAYRINTI : Bilesen
    {

        public SikSorulanSorularAYRINTI()
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
            return bossaDoldur(soru);
        }



        public async static Task<SikSorulanSorularAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SikSorulanSorularAYRINTI sonuc = new SikSorulanSorularAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SikSorulanSorularAYRINTIs.FirstOrDefaultAsync(p => p.sikSorulanSorularkimlik == kimlik && p.varmi == true);
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

        public static async Task<List<SikSorulanSorularAYRINTI>> ara(params Expression<Func<SikSorulanSorularAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SikSorulanSorularAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<SikSorulanSorularAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SikSorulanSorularAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SikSorulanSorularAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<SikSorulanSorularAYRINTI?> bul(veri.Varlik vari, params Expression<Func<SikSorulanSorularAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SikSorulanSorularAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SikSorulanSorularAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SikSorulanSorular";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sikSorulanSorularkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sikSorulanSorularkimlik;
        }


        #endregion


    }
}

