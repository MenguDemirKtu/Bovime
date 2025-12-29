using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class MansetAYRINTI : Bilesen
    {

        public MansetAYRINTI()
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
            return bossaDoldur(baslikUst);
        }



        public async static Task<MansetAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                MansetAYRINTI sonuc = new MansetAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.MansetAYRINTIs.FirstOrDefaultAsync(p => p.mansetkimlik == kimlik && p.varmi == true);
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

        public static async Task<List<MansetAYRINTI>> ara(params Expression<Func<MansetAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.MansetAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<MansetAYRINTI>> ara(veri.Varlik vari, params Expression<Func<MansetAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.MansetAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "MansetAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Manset";
        }
        public override string _birincilAnahtarAdi()
        {
            return "mansetkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.mansetkimlik;
        }


        #endregion


    }
}

