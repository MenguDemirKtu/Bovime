using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class ref_KampanyaDurumu : Bilesen
    {

        public ref_KampanyaDurumu()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(KampanyaDurumuAdi, "", dilKimlik);
        }




        [NotMapped]
        public enumref_KampanyaDurumu _Kampanyadurumu
        {
            get
            {
                return (enumref_KampanyaDurumu)this.KampanyaDurumuKimlik;
            }
            set
            {
                KampanyaDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(KampanyaDurumuAdi);
        }



        public async static Task<ref_KampanyaDurumu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_KampanyaDurumu sonuc = new ref_KampanyaDurumu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_KampanyaDurumus.FirstOrDefaultAsync(p => p.KampanyaDurumuKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_KampanyaDurumu>> ara(params Expression<Func<ref_KampanyaDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_KampanyaDurumuCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_KampanyaDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_KampanyaDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_KampanyaDurumuCizelgesi.ara(vari, kosullar);
        }
        public static async Task<ref_KampanyaDurumu?> bul(veri.Varlik vari, params Expression<Func<ref_KampanyaDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_KampanyaDurumuCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_KampanyaDurumu";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "KampanyaDurumuKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.KampanyaDurumuKimlik;
        }


        #endregion


    }
}

