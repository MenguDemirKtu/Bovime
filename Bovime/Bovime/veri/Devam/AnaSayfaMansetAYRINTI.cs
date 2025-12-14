using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veri
{
    public partial class AnaSayfaMansetAYRINTI : Bilesen
    {

        public AnaSayfaMansetAYRINTI()
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



        public async static Task<AnaSayfaMansetAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                AnaSayfaMansetAYRINTI sonuc = new AnaSayfaMansetAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.AnaSayfaMansetAYRINTIs.FirstOrDefaultAsync(p => p.anaSayfaMansetkimlik == kimlik && p.varmi == true);
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

        public static async Task<List<AnaSayfaMansetAYRINTI>> ara(params Expression<Func<AnaSayfaMansetAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.AnaSayfaMansetAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<AnaSayfaMansetAYRINTI>> ara(veri.Varlik vari, params Expression<Func<AnaSayfaMansetAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.AnaSayfaMansetAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "AnaSayfaMansetAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "AnaSayfaManset";
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

