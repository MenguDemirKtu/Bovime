using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaGrubuReklamiAYRINTI : Bilesen
    {

        public FirmaGrubuReklamiAYRINTI()
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
            return bossaDoldur(firmaGrubuBaslik);
        }



        public async static Task<FirmaGrubuReklamiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaGrubuReklamiAYRINTI sonuc = new FirmaGrubuReklamiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaGrubuReklamiAYRINTIs.FirstOrDefaultAsync(p => p.firmaGrubuReklamikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<FirmaGrubuReklamiAYRINTI>> ara(params Expression<Func<FirmaGrubuReklamiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaGrubuReklamiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaGrubuReklamiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaGrubuReklamiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaGrubuReklamiAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaGrubuReklamiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "FirmaGrubuReklami";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmaGrubuReklamikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmaGrubuReklamikimlik;
        }


        #endregion


    }
}

