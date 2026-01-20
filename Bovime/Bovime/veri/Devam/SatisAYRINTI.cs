using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SatisAYRINTI : Bilesen
    {

        public SatisAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

            if (string.IsNullOrEmpty(this.kodu))
                this.kodu = Guid.NewGuid().ToString();
        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaKimlik, ".", dilKimlik);
            uyariVerString(SatisDurumuAdi, ".", dilKimlik);
        }




        [NotMapped]
        public enumref_SatisDurumu _Satisdurumu
        {
            get
            {
                return (enumref_SatisDurumu)this.i_satisDurumuKimlik;
            }
            set
            {
                i_satisDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_firmaKimlik);
        }



        public async static Task<SatisAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SatisAYRINTI sonuc = new SatisAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SatisAYRINTIs.FirstOrDefaultAsync(p => p.satisKimlik == kimlik && p.varmi == true);
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

        public static async Task<List<SatisAYRINTI>> ara(params Expression<Func<SatisAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SatisAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<SatisAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SatisAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SatisAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<SatisAYRINTI?> bul(veri.Varlik vari, params Expression<Func<SatisAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SatisAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SatisAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SatisA";
        }
        public override string _birincilAnahtarAdi()
        {
            return "satisKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.satisKimlik;
        }


        #endregion


    }
}

