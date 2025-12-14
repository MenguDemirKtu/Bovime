using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class SiteDuyuru : Bilesen
    {

        public SiteDuyuru()
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
            return bossaDoldur(siteDuyuruBasligi);
        }



        public async static Task<SiteDuyuru?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SiteDuyuru sonuc = new SiteDuyuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SiteDuyurus.FirstOrDefaultAsync(p => p.siteDuyurukimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.SiteDuyuruCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SiteDuyuruCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.sirasi = 100;
            this.varmi = true;
        }

        public static async Task<List<SiteDuyuru>> ara(params Expression<Func<SiteDuyuru, bool>>[] kosullar)
        {
            return await veriTabani.SiteDuyuruCizelgesi.ara(kosullar);
        }
        public static async Task<List<SiteDuyuru>> ara(veri.Varlik vari, params Expression<Func<SiteDuyuru, bool>>[] kosullar)
        {
            return await veriTabani.SiteDuyuruCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SiteDuyuru";
        }


        public override string _turkceAdi()
        {
            return "Site Duyuru";
        }
        public override string _birincilAnahtarAdi()
        {
            return "siteDuyurukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.siteDuyurukimlik;
        }


        #endregion


    }
}

