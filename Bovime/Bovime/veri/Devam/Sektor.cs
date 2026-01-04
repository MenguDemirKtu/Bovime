using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class Sektor : Bilesen
    {

        public Sektor()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {
            if (string.IsNullOrEmpty(this.sektorUrl))
                this.sektorUrl = Genel.urlDuzenle(sektorAdi ?? "");

            if (vari == null)
                varmi = true;

            if (e_anaSayfaGorunsunmu == null)
                e_anaSayfaGorunsunmu = true;

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return bossaDoldur(sektorAdi);
        }



        public async static Task<Sektor?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Sektor sonuc = new Sektor();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Sektors.FirstOrDefaultAsync(p => p.sektorkimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.SektorCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SektorCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_anaSayfaGorunsunmu = true;
        }

        public static async Task<List<Sektor>> ara(params Expression<Func<Sektor, bool>>[] kosullar)
        {
            return await veriTabani.SektorCizelgesi.ara(kosullar);
        }
        public static async Task<List<Sektor>> ara(veri.Varlik vari, params Expression<Func<Sektor, bool>>[] kosullar)
        {
            return await veriTabani.SektorCizelgesi.ara(vari, kosullar);
        }
        public static async Task<Sektor?> bul(veri.Varlik vari, params Expression<Func<Sektor, bool>>[] kosullar)
        {
            return await veriTabani.SektorCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Sektor";
        }


        public override string _turkceAdi()
        {
            return "Sektör";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sektorkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sektorkimlik;
        }


        #endregion


    }
}

