using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class FirmaGrubuAYRINTIArama
    {
        public Int32? firmaGrubukimlik { get; set; }
        public string? firmaGrupAdi { get; set; }
        public Int32? sirasi { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public FirmaGrubuAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<FirmaGrubuAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<FirmaGrubuAYRINTI>(P => P.varmi == true);
            if (firmaGrubukimlik != null)
                predicate = predicate.And(x => x.firmaGrubukimlik == firmaGrubukimlik);
            if (firmaGrupAdi != null)
                predicate = predicate.And(x => x.firmaGrupAdi != null && x.firmaGrupAdi.Contains(firmaGrupAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<FirmaGrubuAYRINTI>> cek(veri.Varlik vari)
        {
            List<FirmaGrubuAYRINTI> sonuc = await vari.FirmaGrubuAYRINTIs
           .Where(kosulOlustur()).OrderBy(p => p.sirasi)
           .ToListAsync();
            return sonuc;
        }
        public async Task<FirmaGrubuAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            FirmaGrubuAYRINTI? sonuc = await vari.FirmaGrubuAYRINTIs
           .Where(predicate).OrderBy(p => p.sirasi)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class FirmaGrubuAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<FirmaGrubuAYRINTI>> ara(params Expression<Func<FirmaGrubuAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<FirmaGrubuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaGrubuAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.FirmaGrubuAYRINTIs
                            .Where(kosul).OrderBy(p => p.sirasi)
                   .ToListAsync();
        }



        public static async Task<FirmaGrubuAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            FirmaGrubuAYRINTI? kayit = await kime.FirmaGrubuAYRINTIs.FirstOrDefaultAsync(p => p.firmaGrubukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static FirmaGrubuAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            FirmaGrubuAYRINTI? kayit = kime.FirmaGrubuAYRINTIs.FirstOrDefault(p => p.firmaGrubukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

