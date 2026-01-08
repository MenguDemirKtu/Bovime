using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class KampanyaSatisAYRINTIArama
    {
        public Int64? kimlik { get; set; }
        public string? firmaAdi { get; set; }
        public string? kampanyaAdi { get; set; }
        public Int64? y_firmakampanyasikimlik { get; set; }
        public Int32? sayisi { get; set; }
        public Int32? i_firmaKimlik { get; set; }
        public KampanyaSatisAYRINTIArama()
        {
        }

        private ExpressionStarter<KampanyaSatisAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<KampanyaSatisAYRINTI>();
            if (kimlik != null)
                predicate = predicate.And(x => x.kimlik == kimlik);
            if (firmaAdi != null)
                predicate = predicate.And(x => x.firmaAdi != null && x.firmaAdi.Contains(firmaAdi));
            if (kampanyaAdi != null)
                predicate = predicate.And(x => x.kampanyaAdi != null && x.kampanyaAdi.Contains(kampanyaAdi));
            if (y_firmakampanyasikimlik != null)
                predicate = predicate.And(x => x.y_firmakampanyasikimlik == y_firmakampanyasikimlik);
            if (sayisi != null)
                predicate = predicate.And(x => x.sayisi == sayisi);
            if (i_firmaKimlik != null)
                predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik);
            return predicate;

        }
        public async Task<List<KampanyaSatisAYRINTI>> cek(veri.Varlik vari)
        {
            List<KampanyaSatisAYRINTI> sonuc = await vari.KampanyaSatisAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<KampanyaSatisAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            KampanyaSatisAYRINTI? sonuc = await vari.KampanyaSatisAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class KampanyaSatisAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<KampanyaSatisAYRINTI>> ara(params Expression<Func<KampanyaSatisAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<KampanyaSatisAYRINTI>> ara(veri.Varlik vari, params Expression<Func<KampanyaSatisAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.KampanyaSatisAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.kimlik)
                   .ToListAsync();
        }
        public static async Task<KampanyaSatisAYRINTI?> bul(veri.Varlik vari, params Expression<Func<KampanyaSatisAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.KampanyaSatisAYRINTIs.FirstOrDefaultAsync(kosul);
        }



        public static async Task<KampanyaSatisAYRINTI?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            KampanyaSatisAYRINTI? kayit = await kime.KampanyaSatisAYRINTIs.FirstOrDefaultAsync(p => p.kimlik == kimlik);
            return kayit;
        }




        public static KampanyaSatisAYRINTI? tekliCek(Int64 kimlik, Varlik kime)
        {
            KampanyaSatisAYRINTI? kayit = kime.KampanyaSatisAYRINTIs.FirstOrDefault(p => p.kimlik == kimlik);
            return kayit;
        }
    }
}

