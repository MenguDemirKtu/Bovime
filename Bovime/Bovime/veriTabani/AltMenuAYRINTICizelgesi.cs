using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class AltMenuAYRINTIArama
    {
        public Int32? altMenukimlik { get; set; }
        public string? altMenuBaslik { get; set; }
        public string? menuAdi { get; set; }
        public string? menuUrl { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public AltMenuAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<AltMenuAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<AltMenuAYRINTI>(P => P.varmi == true);
            if (altMenukimlik != null)
                predicate = predicate.And(x => x.altMenukimlik == altMenukimlik);
            if (altMenuBaslik != null)
                predicate = predicate.And(x => x.altMenuBaslik != null && x.altMenuBaslik.Contains(altMenuBaslik));
            if (menuAdi != null)
                predicate = predicate.And(x => x.menuAdi != null && x.menuAdi.Contains(menuAdi));
            if (menuUrl != null)
                predicate = predicate.And(x => x.menuUrl != null && x.menuUrl.Contains(menuUrl));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<AltMenuAYRINTI>> cek(veri.Varlik vari)
        {
            List<AltMenuAYRINTI> sonuc = await vari.AltMenuAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<AltMenuAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            AltMenuAYRINTI? sonuc = await vari.AltMenuAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class AltMenuAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<AltMenuAYRINTI>> ara(params Expression<Func<AltMenuAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<AltMenuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<AltMenuAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.AltMenuAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.altMenukimlik)
                   .ToListAsync();
        }
        public static async Task<AltMenuAYRINTI?> bul(veri.Varlik vari, params Expression<Func<AltMenuAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.AltMenuAYRINTIs.FirstOrDefaultAsync(kosul);
        }



        public static async Task<AltMenuAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            AltMenuAYRINTI? kayit = await kime.AltMenuAYRINTIs.FirstOrDefaultAsync(p => p.altMenukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static AltMenuAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            AltMenuAYRINTI? kayit = kime.AltMenuAYRINTIs.FirstOrDefault(p => p.altMenukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

