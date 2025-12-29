using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class ReklamKusagi3AYRINTIArama
    {
        public Int32? reklamKusagi3kimlik { get; set; }
        public string? baslik { get; set; }
        public string? metin { get; set; }
        public string? hedefUrl { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public ReklamKusagi3AYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<ReklamKusagi3AYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<ReklamKusagi3AYRINTI>(P => P.varmi == true);
            if (reklamKusagi3kimlik != null)
                predicate = predicate.And(x => x.reklamKusagi3kimlik == reklamKusagi3kimlik);
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
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<ReklamKusagi3AYRINTI>> cek(veri.Varlik vari)
        {
            List<ReklamKusagi3AYRINTI> sonuc = await vari.ReklamKusagi3AYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<ReklamKusagi3AYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            ReklamKusagi3AYRINTI? sonuc = await vari.ReklamKusagi3AYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class ReklamKusagi3AYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ReklamKusagi3AYRINTI>> ara(params Expression<Func<ReklamKusagi3AYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ReklamKusagi3AYRINTI>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi3AYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ReklamKusagi3AYRINTIs
                            .Where(kosul).OrderByDescending(p => p.reklamKusagi3kimlik)
                   .ToListAsync();
        }



        public static async Task<ReklamKusagi3AYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ReklamKusagi3AYRINTI? kayit = await kime.ReklamKusagi3AYRINTIs.FirstOrDefaultAsync(p => p.reklamKusagi3kimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static ReklamKusagi3AYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            ReklamKusagi3AYRINTI? kayit = kime.ReklamKusagi3AYRINTIs.FirstOrDefault(p => p.reklamKusagi3kimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

