using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaKampanyaTalebiAYRINTI : Bilesen
    {

        public FirmaKampanyaTalebiAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaKimlik, ".", dilKimlik);
            uyariVerInt32(i_firmaDurumuKimlik, ".", dilKimlik);
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
            return bossaDoldur(i_firmaKimlik);
        }



        public async static Task<FirmaKampanyaTalebiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaKampanyaTalebiAYRINTI sonuc = new FirmaKampanyaTalebiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaKampanyaTalebiAYRINTIs.FirstOrDefaultAsync(p => p.firmaKampanyaTalebikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<FirmaKampanyaTalebiAYRINTI>> ara(params Expression<Func<FirmaKampanyaTalebiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyaTalebiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaKampanyaTalebiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaKampanyaTalebiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyaTalebiAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<FirmaKampanyaTalebiAYRINTI?> bul(veri.Varlik vari, params Expression<Func<FirmaKampanyaTalebiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyaTalebiAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaKampanyaTalebiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "FirmaKampanyaTalebiAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmaKampanyaTalebikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmaKampanyaTalebikimlik;
        }


        #endregion


    }
}

