using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class Paket : Bilesen
    {

        public Paket()
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
            return bossaDoldur(paketAdi);
        }



        public async static Task<Paket?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Paket sonuc = new Paket();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Pakets.FirstOrDefaultAsync(p => p.paketKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.PaketCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.PaketCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<Paket>> ara(params Expression<Func<Paket, bool>>[] kosullar)
        {
            return await veriTabani.PaketCizelgesi.ara(kosullar);
        }
        public static async Task<List<Paket>> ara(veri.Varlik vari, params Expression<Func<Paket, bool>>[] kosullar)
        {
            return await veriTabani.PaketCizelgesi.ara(vari, kosullar);
        }
        public static async Task<Paket?> bul(veri.Varlik vari, params Expression<Func<Paket, bool>>[] kosullar)
        {
            return await veriTabani.PaketCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Paket";
        }


        public override string _turkceAdi()
        {
            return "Paket";
        }
        public override string _birincilAnahtarAdi()
        {
            return "paketKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.paketKimlik;
        }


        #endregion


    }
}

