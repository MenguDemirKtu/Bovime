using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SektorAYRINTI : Bilesen
    {

        public SektorAYRINTI()
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
            return bossaDoldur(sektorAdi);
        }



        public async static Task<SektorAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SektorAYRINTI sonuc = new SektorAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SektorAYRINTIs.FirstOrDefaultAsync(p => p.sektorkimlik == kimlik && p.varmi == true);
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

        public static async Task<List<SektorAYRINTI>> ara(params Expression<Func<SektorAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SektorAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<SektorAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SektorAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SektorAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SektorAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Sektor";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sektorkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sektorkimlik;
        }


        #endregion


    }
}

