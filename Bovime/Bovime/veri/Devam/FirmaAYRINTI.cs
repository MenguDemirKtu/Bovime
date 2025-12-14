using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaAYRINTI : Bilesen
    {

        public FirmaAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaDurumuKimlik, ".", dilKimlik);
            uyariVerString(FirmaDurumuAdi, ".", dilKimlik);
        }




        [NotMapped]
        public enumref_FirmaDurumu _Firmadurumu
        {
            get
            {
                return (enumref_FirmaDurumu)this.i_firmaDurumuKimlik;
            }
            set
            {
                i_firmaDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(firmaAdi);
        }



        public async static Task<FirmaAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaAYRINTI sonuc = new FirmaAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaAYRINTIs.FirstOrDefaultAsync(p => p.firmakimlik == kimlik && p.varmi == true);
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

        public static async Task<List<FirmaAYRINTI>> ara(params Expression<Func<FirmaAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Firma";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmakimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmakimlik;
        }


        #endregion


    }
}

