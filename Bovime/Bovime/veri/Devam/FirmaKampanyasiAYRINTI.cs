using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaKampanyasiAYRINTI : Bilesen
    {
        [NotMapped]
        public string _URL
        {
            get
            {
                return string.Format("/UyeFirma/Goster/{0}", this.firmaUrl ?? "");
            }
        }
        public FirmaKampanyasiAYRINTI()
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



        public async static Task<FirmaKampanyasiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaKampanyasiAYRINTI sonuc = new FirmaKampanyasiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaKampanyasiAYRINTIs.FirstOrDefaultAsync(p => p.firmaKampanyasikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<FirmaKampanyasiAYRINTI>> ara(params Expression<Func<FirmaKampanyasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyasiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaKampanyasiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaKampanyasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyasiAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<FirmaKampanyasiAYRINTI?> bul(veri.Varlik vari, params Expression<Func<FirmaKampanyasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaKampanyasiAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaKampanyasiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "FirmaKampanyasiAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmaKampanyasikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmaKampanyasikimlik;
        }


        #endregion


    }
}

