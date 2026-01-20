using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class ref_SatisDurumu : Bilesen
    {

        public ref_SatisDurumu()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(SatisDurumuAdi, "", dilKimlik);
        }




        [NotMapped]
        public enumref_SatisDurumu _Satisdurumu
        {
            get
            {
                return (enumref_SatisDurumu)this.SatisDurumuKimlik;
            }
            set
            {
                SatisDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(SatisDurumuAdi);
        }



        public async static Task<ref_SatisDurumu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_SatisDurumu sonuc = new ref_SatisDurumu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_SatisDurumus.FirstOrDefaultAsync(p => p.SatisDurumuKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_SatisDurumu>> ara(params Expression<Func<ref_SatisDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_SatisDurumuCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_SatisDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_SatisDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_SatisDurumuCizelgesi.ara(vari, kosullar);
        }
        public static async Task<ref_SatisDurumu?> bul(veri.Varlik vari, params Expression<Func<ref_SatisDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_SatisDurumuCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_SatisDurumu";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "SatisDurumuKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.SatisDurumuKimlik;
        }


        #endregion


    }
}

