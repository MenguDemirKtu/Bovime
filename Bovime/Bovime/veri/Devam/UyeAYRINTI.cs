using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class UyeAYRINTI : Bilesen
    {

        public UyeAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(UyeDurumuAdi, ".", dilKimlik);
        }




        [NotMapped]
        public enumref_UyeDurumu _Uyedurumu
        {
            get
            {
                return (enumref_UyeDurumu)this.i_uyeDurumuKimlik;
            }
            set
            {
                i_uyeDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(adi);
        }



        public async static Task<UyeAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                UyeAYRINTI sonuc = new UyeAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.UyeAYRINTIs.FirstOrDefaultAsync(p => p.uyekimlik == kimlik && p.varmi == true);
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

        public static async Task<List<UyeAYRINTI>> ara(params Expression<Func<UyeAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.UyeAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<UyeAYRINTI>> ara(veri.Varlik vari, params Expression<Func<UyeAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.UyeAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<UyeAYRINTI?> bul(veri.Varlik vari, params Expression<Func<UyeAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.UyeAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "UyeAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "UyeAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "uyekimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.uyekimlik;
        }


        #endregion


    }
}

