using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class BovimisayfasiAYRINTIArama
    {
        public Int32? bovimisayfasikimlik { get; set; }
        public string? bovimiSayfasiBaslik { get; set; }
        public string? title { get; set; }
        public string? url { get; set; }
        public string? tanitim { get; set; }
        public bool? e_yayindaMi { get; set; }
        public string? yayindaMi { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public BovimisayfasiAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<BovimisayfasiAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<BovimisayfasiAYRINTI>(P => P.varmi == true);
            if (bovimisayfasikimlik != null)
                predicate = predicate.And(x => x.bovimisayfasikimlik == bovimisayfasikimlik);
            if (bovimiSayfasiBaslik != null)
                predicate = predicate.And(x => x.bovimiSayfasiBaslik != null && x.bovimiSayfasiBaslik.Contains(bovimiSayfasiBaslik));
            if (title != null)
                predicate = predicate.And(x => x.title != null && x.title.Contains(title));
            if (url != null)
                predicate = predicate.And(x => x.url != null && x.url.Contains(url));
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (e_yayindaMi != null)
                predicate = predicate.And(x => x.e_yayindaMi == e_yayindaMi);
            if (yayindaMi != null)
                predicate = predicate.And(x => x.yayindaMi != null && x.yayindaMi.Contains(yayindaMi));
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<BovimisayfasiAYRINTI>> cek(veri.Varlik vari)
        {
            List<BovimisayfasiAYRINTI> sonuc = await vari.BovimisayfasiAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<BovimisayfasiAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            BovimisayfasiAYRINTI? sonuc = await vari.BovimisayfasiAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class BovimisayfasiAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<BovimisayfasiAYRINTI>> ara(params Expression<Func<BovimisayfasiAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<BovimisayfasiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<BovimisayfasiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.BovimisayfasiAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.bovimisayfasikimlik)
                   .ToListAsync();
        }
        public static async Task<BovimisayfasiAYRINTI?> bul(veri.Varlik vari, params Expression<Func<BovimisayfasiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.BovimisayfasiAYRINTIs.FirstOrDefaultAsync(kosul);
        }



        public static async Task<BovimisayfasiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            BovimisayfasiAYRINTI? kayit = await kime.BovimisayfasiAYRINTIs.FirstOrDefaultAsync(p => p.bovimisayfasikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static BovimisayfasiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            BovimisayfasiAYRINTI? kayit = kime.BovimisayfasiAYRINTIs.FirstOrDefault(p => p.bovimisayfasikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

