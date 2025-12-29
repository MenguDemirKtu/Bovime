using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class MansetAYRINTIArama
    {
        public Int32? mansetkimlik { get; set; }
        public string? baslikUst { get; set; }
        public string? baslikOrta { get; set; }
        public string? altYazi { get; set; }
        public Int32? sirasi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public bool? e_yayindami { get; set; }
        public string? yayindami { get; set; }
        public bool? varmi { get; set; }
        public string? hedefUrl { get; set; }
        public MansetAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<MansetAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<MansetAYRINTI>(P => P.varmi == true);
            if (mansetkimlik != null)
                predicate = predicate.And(x => x.mansetkimlik == mansetkimlik);
            if (baslikUst != null)
                predicate = predicate.And(x => x.baslikUst != null && x.baslikUst.Contains(baslikUst));
            if (baslikOrta != null)
                predicate = predicate.And(x => x.baslikOrta != null && x.baslikOrta.Contains(baslikOrta));
            if (altYazi != null)
                predicate = predicate.And(x => x.altYazi != null && x.altYazi.Contains(altYazi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (e_yayindami != null)
                predicate = predicate.And(x => x.e_yayindami == e_yayindami);
            if (yayindami != null)
                predicate = predicate.And(x => x.yayindami != null && x.yayindami.Contains(yayindami));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (hedefUrl != null)
                predicate = predicate.And(x => x.hedefUrl != null && x.hedefUrl.Contains(hedefUrl));
            return predicate;

        }
        public async Task<List<MansetAYRINTI>> cek(veri.Varlik vari)
        {
            List<MansetAYRINTI> sonuc = await vari.MansetAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<MansetAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            MansetAYRINTI? sonuc = await vari.MansetAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class MansetAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<MansetAYRINTI>> ara(params Expression<Func<MansetAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<MansetAYRINTI>> ara(veri.Varlik vari, params Expression<Func<MansetAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.MansetAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.mansetkimlik)
                   .ToListAsync();
        }



        public static async Task<MansetAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            MansetAYRINTI? kayit = await kime.MansetAYRINTIs.FirstOrDefaultAsync(p => p.mansetkimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static MansetAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            MansetAYRINTI? kayit = kime.MansetAYRINTIs.FirstOrDefault(p => p.mansetkimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

