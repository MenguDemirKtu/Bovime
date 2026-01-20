using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class FirmaGrubuReklamiAYRINTIArama
    {
        public Int32? firmaGrubuReklamikimlik { get; set; }
        public string? firmaGrubuBaslik { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public string? kisaTanitim { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public FirmaGrubuReklamiAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<FirmaGrubuReklamiAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<FirmaGrubuReklamiAYRINTI>(P => P.varmi == true);
            if (firmaGrubuReklamikimlik != null)
                predicate = predicate.And(x => x.firmaGrubuReklamikimlik == firmaGrubuReklamikimlik);
            if (firmaGrubuBaslik != null)
                predicate = predicate.And(x => x.firmaGrubuBaslik != null && x.firmaGrubuBaslik.Contains(firmaGrubuBaslik));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (kisaTanitim != null)
                predicate = predicate.And(x => x.kisaTanitim != null && x.kisaTanitim.Contains(kisaTanitim));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<FirmaGrubuReklamiAYRINTI>> cek(veri.Varlik vari)
        {
            List<FirmaGrubuReklamiAYRINTI> sonuc = await vari.FirmaGrubuReklamiAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<FirmaGrubuReklamiAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            FirmaGrubuReklamiAYRINTI? sonuc = await vari.FirmaGrubuReklamiAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class FirmaGrubuReklamiAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<FirmaGrubuReklamiAYRINTI>> ara(params Expression<Func<FirmaGrubuReklamiAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<FirmaGrubuReklamiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaGrubuReklamiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.FirmaGrubuReklamiAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.firmaGrubuReklamikimlik)
                   .ToListAsync();
        }



        public static async Task<FirmaGrubuReklamiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            FirmaGrubuReklamiAYRINTI? kayit = await kime.FirmaGrubuReklamiAYRINTIs.FirstOrDefaultAsync(p => p.firmaGrubuReklamikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static FirmaGrubuReklamiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            FirmaGrubuReklamiAYRINTI? kayit = kime.FirmaGrubuReklamiAYRINTIs.FirstOrDefault(p => p.firmaGrubuReklamikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

