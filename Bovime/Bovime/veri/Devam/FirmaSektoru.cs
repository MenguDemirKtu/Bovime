using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class FirmaSektoru : Bilesen
    {

        public FirmaSektoru()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaKimlik, "Firma", dilKimlik);
            uyariVerInt32(i_sektorKimlik, "Sektör", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_firmaKimlik);
        }



        public async static Task<FirmaSektoru?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FirmaSektoru sonuc = new FirmaSektoru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.FirmaSektorus.FirstOrDefaultAsync(p => p.firmaSektorukimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.FirmaSektoruCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.FirmaSektoruCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.varmi = true;
        }

        public static async Task<List<FirmaSektoru>> ara(params Expression<Func<FirmaSektoru, bool>>[] kosullar)
        {
            return await veriTabani.FirmaSektoruCizelgesi.ara(kosullar);
        }
        public static async Task<List<FirmaSektoru>> ara(veri.Varlik vari, params Expression<Func<FirmaSektoru, bool>>[] kosullar)
        {
            return await veriTabani.FirmaSektoruCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "FirmaSektoru";
        }


        public override string _turkceAdi()
        {
            return "Firma Sektörü";
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

