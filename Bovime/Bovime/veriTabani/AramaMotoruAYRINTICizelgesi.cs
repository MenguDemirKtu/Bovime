using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class AramaMotoruAYRINTIArama
    {
        public Int32? aramaMotorukimlik { get; set; }
        public string? sayfaAdresi { get; set; }
        public string? title { get; set; }
        public string? keywords { get; set; }
        public Int32? goruntulenmeSayisi { get; set; }
        public bool? varmi { get; set; }
        public AramaMotoruAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<AramaMotoruAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<AramaMotoruAYRINTI>(P => P.varmi == true);
            if (aramaMotorukimlik != null)
                predicate = predicate.And(x => x.aramaMotorukimlik == aramaMotorukimlik);
            if (sayfaAdresi != null)
                predicate = predicate.And(x => x.sayfaAdresi != null && x.sayfaAdresi.Contains(sayfaAdresi));
            if (title != null)
                predicate = predicate.And(x => x.title != null && x.title.Contains(title));
            if (keywords != null)
                predicate = predicate.And(x => x.keywords != null && x.keywords.Contains(keywords));
            if (goruntulenmeSayisi != null)
                predicate = predicate.And(x => x.goruntulenmeSayisi == goruntulenmeSayisi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<AramaMotoruAYRINTI>> cek(veri.Varlik vari)
        {
            List<AramaMotoruAYRINTI> sonuc = await vari.AramaMotoruAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<AramaMotoruAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            AramaMotoruAYRINTI? sonuc = await vari.AramaMotoruAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class AramaMotoruAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<AramaMotoruAYRINTI>> ara(params Expression<Func<AramaMotoruAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<AramaMotoruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<AramaMotoruAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.AramaMotoruAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.aramaMotorukimlik)
                   .ToListAsync();
        }



        public static async Task<AramaMotoruAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            AramaMotoruAYRINTI? kayit = await kime.AramaMotoruAYRINTIs.FirstOrDefaultAsync(p => p.aramaMotorukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static AramaMotoruAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            AramaMotoruAYRINTI? kayit = kime.AramaMotoruAYRINTIs.FirstOrDefault(p => p.aramaMotorukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

