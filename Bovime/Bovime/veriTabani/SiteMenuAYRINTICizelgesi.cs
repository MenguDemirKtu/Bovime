using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class SiteMenuAYRINTIArama
    {
        public Int32? siteMenukimlik { get; set; }
        public string? siteMenuAdi { get; set; }
        public string? menuUrl { get; set; }
        public bool? e_altMenuMu { get; set; }
        public string? altMenuMu { get; set; }
        public Int32? i_ustSiteMenuKimlik { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public SiteMenuAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<SiteMenuAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<SiteMenuAYRINTI>(P => P.varmi == true);
            if (siteMenukimlik != null)
                predicate = predicate.And(x => x.siteMenukimlik == siteMenukimlik);
            if (siteMenuAdi != null)
                predicate = predicate.And(x => x.siteMenuAdi != null && x.siteMenuAdi.Contains(siteMenuAdi));
            if (menuUrl != null)
                predicate = predicate.And(x => x.menuUrl != null && x.menuUrl.Contains(menuUrl));
            if (e_altMenuMu != null)
                predicate = predicate.And(x => x.e_altMenuMu == e_altMenuMu);
            if (altMenuMu != null)
                predicate = predicate.And(x => x.altMenuMu != null && x.altMenuMu.Contains(altMenuMu));
            if (i_ustSiteMenuKimlik != null)
                predicate = predicate.And(x => x.i_ustSiteMenuKimlik == i_ustSiteMenuKimlik);
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<SiteMenuAYRINTI>> cek(veri.Varlik vari)
        {
            List<SiteMenuAYRINTI> sonuc = await vari.SiteMenuAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<SiteMenuAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            SiteMenuAYRINTI? sonuc = await vari.SiteMenuAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class SiteMenuAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<SiteMenuAYRINTI>> ara(params Expression<Func<SiteMenuAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<SiteMenuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SiteMenuAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SiteMenuAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.siteMenukimlik)
                   .ToListAsync();
        }



        public static async Task<SiteMenuAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SiteMenuAYRINTI? kayit = await kime.SiteMenuAYRINTIs.FirstOrDefaultAsync(p => p.siteMenukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SiteMenuAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            SiteMenuAYRINTI? kayit = kime.SiteMenuAYRINTIs.FirstOrDefault(p => p.siteMenukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

