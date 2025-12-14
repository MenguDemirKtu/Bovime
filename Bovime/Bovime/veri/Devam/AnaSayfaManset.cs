using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class AnaSayfaManset : Bilesen
    {

        public AnaSayfaManset()
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
            return bossaDoldur(baslik);
        }



        public async static Task<AnaSayfaManset?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                AnaSayfaManset sonuc = new AnaSayfaManset();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.AnaSayfaMansets.FirstOrDefaultAsync(p => p.anaSayfaMansetkimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.AnaSayfaMansetCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.AnaSayfaMansetCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.yayinBaslangic = DateTime.Now;
            this.sirasi = 100;
        }

        public static async Task<List<AnaSayfaManset>> ara(params Expression<Func<AnaSayfaManset, bool>>[] kosullar)
        {
            return await veriTabani.AnaSayfaMansetCizelgesi.ara(kosullar);
        }
        public static async Task<List<AnaSayfaManset>> ara(veri.Varlik vari, params Expression<Func<AnaSayfaManset, bool>>[] kosullar)
        {
            return await veriTabani.AnaSayfaMansetCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "AnaSayfaManset";
        }


        public override string _turkceAdi()
        {
            return "Ana Sayfa Manşet";
        }
        public override string _birincilAnahtarAdi()
        {
            return "anaSayfaMansetkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.anaSayfaMansetkimlik;
        }


        #endregion


    }
}

