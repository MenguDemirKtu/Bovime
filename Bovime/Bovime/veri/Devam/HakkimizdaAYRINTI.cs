using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class HakkimizdaAYRINTI : Bilesen
    {

        public HakkimizdaAYRINTI()
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
            return bossaDoldur(firmaAdi);
        }



        public async static Task<HakkimizdaAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                HakkimizdaAYRINTI sonuc = new HakkimizdaAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.HakkimizdaAYRINTIs.FirstOrDefaultAsync(p => p.hakkimizdakimlik == kimlik && p.varmi == true);
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

        public static async Task<List<HakkimizdaAYRINTI>> ara(params Expression<Func<HakkimizdaAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.HakkimizdaAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<HakkimizdaAYRINTI>> ara(veri.Varlik vari, params Expression<Func<HakkimizdaAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.HakkimizdaAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "HakkimizdaAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "HakkimizdaAY";
        }
        public override string _birincilAnahtarAdi()
        {
            return "hakkimizdakimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.hakkimizdakimlik;
        }


        #endregion


    }
}

