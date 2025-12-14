using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class ref_FirmaDurumu : Bilesen
    {

        public ref_FirmaDurumu()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(FirmaDurumuAdi, "", dilKimlik);
        }




        [NotMapped]
        public enumref_FirmaDurumu _Firmadurumu
        {
            get
            {
                return (enumref_FirmaDurumu)this.FirmaDurumuKimlik;
            }
            set
            {
                FirmaDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(FirmaDurumuAdi);
        }



        public async static Task<ref_FirmaDurumu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_FirmaDurumu sonuc = new ref_FirmaDurumu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_FirmaDurumus.FirstOrDefaultAsync(p => p.FirmaDurumuKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_FirmaDurumu>> ara(params Expression<Func<ref_FirmaDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_FirmaDurumuCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_FirmaDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_FirmaDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_FirmaDurumuCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_FirmaDurumu";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "FirmaDurumuKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.FirmaDurumuKimlik;
        }


        #endregion


    }
}

