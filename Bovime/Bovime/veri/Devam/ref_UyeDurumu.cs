using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class ref_UyeDurumu : Bilesen
    {

        public ref_UyeDurumu()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(UyeDurumuAdi, "", dilKimlik);
        }




        [NotMapped]
        public enumref_UyeDurumu _Uyedurumu
        {
            get
            {
                return (enumref_UyeDurumu)this.UyeDurumuKimlik;
            }
            set
            {
                UyeDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(UyeDurumuAdi);
        }



        public async static Task<ref_UyeDurumu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_UyeDurumu sonuc = new ref_UyeDurumu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_UyeDurumus.FirstOrDefaultAsync(p => p.UyeDurumuKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_UyeDurumu>> ara(params Expression<Func<ref_UyeDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_UyeDurumuCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_UyeDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_UyeDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_UyeDurumuCizelgesi.ara(vari, kosullar);
        }
        public static async Task<ref_UyeDurumu?> bul(veri.Varlik vari, params Expression<Func<ref_UyeDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_UyeDurumuCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_UyeDurumu";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "UyeDurumuKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.UyeDurumuKimlik;
        }


        #endregion


    }
}

