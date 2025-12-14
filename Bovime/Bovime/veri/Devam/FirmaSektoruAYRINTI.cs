using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaSektoruAYRINTI : Bilesen
    {

        public FirmaSektoruAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaKimlik, ".", dilKimlik);
            uyariVerInt32(i_sektorKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_firmaKimlik);
        }



        public async static Task<FirmaSektoruAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaSektoruAYRINTI sonuc = new FirmaSektoruAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaSektoruAYRINTIs.FirstOrDefaultAsync(p => p.firmaSektorukimlik == kimlik && p.varmi == true);
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

        public static async Task<List<FirmaSektoruAYRINTI>> ara(params Expression<Func<FirmaSektoruAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaSektoruAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaSektoruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaSektoruAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.FirmaSektoruAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaSektoruAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "FirmaSektoru";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmaSektorukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmaSektorukimlik;
        }


        #endregion


    }
}

