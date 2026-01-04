using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class Firma : Bilesen
    {

        public Firma()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {
            if (string.IsNullOrEmpty(this.firmaUrl))
                this.firmaUrl = Genel.urlDuzenle(firmaAdi);
        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaDurumuKimlik, "Firma Durumu", dilKimlik);
        }




        [NotMapped]
        public enumref_FirmaDurumu _Firmadurumu
        {
            get
            {
                return (enumref_FirmaDurumu)this.i_firmaDurumuKimlik;
            }
            set
            {
                i_firmaDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(firmaAdi);
        }



        public async static Task<Firma?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Firma sonuc = new Firma();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Firmas.FirstOrDefaultAsync(p => p.firmakimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.FirmaCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.FirmaCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<Firma>> ara(params Expression<Func<Firma, bool>>[] kosullar)
        {
            return await veriTabani.FirmaCizelgesi.ara(kosullar);
        }
        public static async Task<List<Firma>> ara(veri.Varlik vari, params Expression<Func<Firma, bool>>[] kosullar)
        {
            return await veriTabani.FirmaCizelgesi.ara(vari, kosullar);
        }
        public static async Task<Firma?> bul(veri.Varlik vari, params Expression<Func<Firma, bool>>[] kosullar)
        {
            return await veriTabani.FirmaCizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Firma";
        }


        public override string _turkceAdi()
        {
            return "Firma";
        }
        public override string _birincilAnahtarAdi()
        {
            return "firmakimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.firmakimlik;
        }


        #endregion


    }
}

