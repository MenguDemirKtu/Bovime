using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SektorObegi : Bilesen
    {

        public SektorObegi()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return bossaDoldur(sektorObegiAdi);
        }



        public async static Task<SektorObegi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SektorObegi sonuc = new SektorObegi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SektorObegis.FirstOrDefaultAsync(p => p.sektorObegikimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.SektorObegiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SektorObegiCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_anaSayfadaGorunsunMu = false;
            this.varmi = true;
        }

        public static async Task<List<SektorObegi>> ara(params Expression<Func<SektorObegi, bool>>[] kosullar)
        {
            return await veriTabani.SektorObegiCizelgesi.ara(kosullar);
        }
        public static async Task<List<SektorObegi>> ara(veri.Varlik vari, params Expression<Func<SektorObegi, bool>>[] kosullar)
        {
            return await veriTabani.SektorObegiCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SektorObegi";
        }


        public override string _turkceAdi()
        {
            return "Sektör Öbeği";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sektorObegikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sektorObegikimlik;
        }


        #endregion


    }
}

