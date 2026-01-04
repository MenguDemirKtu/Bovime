using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class SektorAYRINTIArama
    {
        public Int32? sektorkimlik { get; set; }
        public string? sektorAdi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public string? tanitim { get; set; }
        public bool? varmi { get; set; }
        public string? sektorUrl { get; set; }
        public bool? e_anaSayfaGorunsunmu { get; set; }
        public SektorAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<SektorAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<SektorAYRINTI>(P => P.varmi == true);
            if (sektorkimlik != null)
                predicate = predicate.And(x => x.sektorkimlik == sektorkimlik);
            if (sektorAdi != null)
                predicate = predicate.And(x => x.sektorAdi != null && x.sektorAdi.Contains(sektorAdi));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (sektorUrl != null)
                predicate = predicate.And(x => x.sektorUrl != null && x.sektorUrl.Contains(sektorUrl));
            if (e_anaSayfaGorunsunmu != null)
                predicate = predicate.And(x => x.e_anaSayfaGorunsunmu == e_anaSayfaGorunsunmu);
            return predicate;

        }
        public async Task<List<SektorAYRINTI>> cek(veri.Varlik vari)
        {
            List<SektorAYRINTI> sonuc = await vari.SektorAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<SektorAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            SektorAYRINTI? sonuc = await vari.SektorAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class SektorAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<SektorAYRINTI>> ara(params Expression<Func<SektorAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<SektorAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SektorAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SektorAYRINTIs
                            .Where(kosul).OrderBy(p => p.sektorAdi)
                   .ToListAsync();
        }
        public static async Task<SektorAYRINTI?> bul(veri.Varlik vari, params Expression<Func<SektorAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SektorAYRINTIs.FirstOrDefaultAsync(kosul);
        }



        public static async Task<SektorAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SektorAYRINTI? kayit = await kime.SektorAYRINTIs.FirstOrDefaultAsync(p => p.sektorkimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SektorAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            SektorAYRINTI? kayit = kime.SektorAYRINTIs.FirstOrDefault(p => p.sektorkimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

