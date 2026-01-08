using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class KampanyaSatisAYRINTI : Bilesen
    {

        public KampanyaSatisAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_firmaKimlik, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(firmaAdi);
        }



        public async static Task<KampanyaSatisAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KampanyaSatisAYRINTI sonuc = new KampanyaSatisAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.KampanyaSatisAYRINTIs.FirstOrDefaultAsync(p => p.kimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        public static async Task<List<KampanyaSatisAYRINTI>> ara(params Expression<Func<KampanyaSatisAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.KampanyaSatisAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<KampanyaSatisAYRINTI>> ara(veri.Varlik vari, params Expression<Func<KampanyaSatisAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.KampanyaSatisAYRINTICizelgesi.ara(vari, kosullar);
        }
        public static async Task<KampanyaSatisAYRINTI?> bul(veri.Varlik vari, params Expression<Func<KampanyaSatisAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.KampanyaSatisAYRINTICizelgesi.bul(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "KampanyaSatisAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.kimlik;
        }


        #endregion


    }
}

