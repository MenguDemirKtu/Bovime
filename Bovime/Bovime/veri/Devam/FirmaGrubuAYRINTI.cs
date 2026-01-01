using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaGrubuAYRINTI : Bilesen
    {

        [NotMapped]
        public string _url
        {
            get
            {
                return Genel.urlDuzenle(this.firmaGrupAdi ?? "");
            }
        }

        public FirmaGrubuAYRINTI()
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
            return bossaDoldur(firmaGrupAdi);
        }



        public async static Task<FirmaGrubuAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaGrubuAYRINTI sonuc = new FirmaGrubuAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaGrubuAYRINTIs.FirstOrDefaultAsync(p => p.firmaGrubukimlik == kimlik && p.varmi == true);
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

        public static async Task<List<FirmaGrubuAYRINTI>> ara(params Expression<Func<FirmaGrubuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaGrubuAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaGrubuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaGrubuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaGrubuAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaGrubuAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "FirmaGrubu";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmaGrubukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmaGrubukimlik;
        }


        #endregion


    }
}

