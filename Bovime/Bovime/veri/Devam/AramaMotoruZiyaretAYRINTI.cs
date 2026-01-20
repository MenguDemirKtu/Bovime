using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class AramaMotoruZiyaretAYRINTI : Bilesen
    {

        public AramaMotoruZiyaretAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_aramaMotoruKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_aramaMotoruKimlik);
        }



        public async static Task<AramaMotoruZiyaretAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                AramaMotoruZiyaretAYRINTI sonuc = new AramaMotoruZiyaretAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.AramaMotoruZiyaretAYRINTIs.FirstOrDefaultAsync(p => p.aramaMotoruZiyaretkimlik == kimlik && p.varmi == true);
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

        public static async Task<List<AramaMotoruZiyaretAYRINTI>> ara(params Expression<Func<AramaMotoruZiyaretAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.AramaMotoruZiyaretAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<AramaMotoruZiyaretAYRINTI>> ara(veri.Varlik vari, params Expression<Func<AramaMotoruZiyaretAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.AramaMotoruZiyaretAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<AramaMotoruZiyaretAYRINTI?> bul(veri.Varlik vari, params Expression<Func<AramaMotoruZiyaretAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.AramaMotoruZiyaretAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "AramaMotoruZiyaretAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "AramaMotoruZiyaret";
        }
        public override string _birincilAnahtarAdi()
        {
            return "aramaMotoruZiyaretkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.aramaMotoruZiyaretkimlik;
        }


        #endregion


    }
}

