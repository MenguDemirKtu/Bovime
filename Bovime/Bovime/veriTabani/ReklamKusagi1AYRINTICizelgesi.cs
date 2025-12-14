using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class ReklamKusagi1AYRINTIArama
    {
        public Int32? reklamKusagi1kimlik { get; set; }
        public string? baslik { get; set; }
        public string? metin { get; set; }
        public string? hedefUrl { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public bool? varmi { get; set; }
        public ReklamKusagi1AYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<ReklamKusagi1AYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<ReklamKusagi1AYRINTI>(P => P.varmi == true);
            if (reklamKusagi1kimlik != null)
                predicate = predicate.And(x => x.reklamKusagi1kimlik == reklamKusagi1kimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik != null && x.baslik.Contains(baslik));
            if (metin != null)
                predicate = predicate.And(x => x.metin != null && x.metin.Contains(metin));
            if (hedefUrl != null)
                predicate = predicate.And(x => x.hedefUrl != null && x.hedefUrl.Contains(hedefUrl));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<ReklamKusagi1AYRINTI>> cek(veri.Varlik vari)
        {
            List<ReklamKusagi1AYRINTI> sonuc = await vari.ReklamKusagi1AYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<ReklamKusagi1AYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            ReklamKusagi1AYRINTI? sonuc = await vari.ReklamKusagi1AYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class ReklamKusagi1AYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ReklamKusagi1AYRINTI>> ara(params Expression<Func<ReklamKusagi1AYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ReklamKusagi1AYRINTI>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi1AYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ReklamKusagi1AYRINTIs
                            .Where(kosul).OrderByDescending(p => p.reklamKusagi1kimlik)
                   .ToListAsync();
        }



        public static async Task<ReklamKusagi1AYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ReklamKusagi1AYRINTI? kayit = await kime.ReklamKusagi1AYRINTIs.FirstOrDefaultAsync(p => p.reklamKusagi1kimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static ReklamKusagi1AYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            ReklamKusagi1AYRINTI? kayit = kime.ReklamKusagi1AYRINTIs.FirstOrDefault(p => p.reklamKusagi1kimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

