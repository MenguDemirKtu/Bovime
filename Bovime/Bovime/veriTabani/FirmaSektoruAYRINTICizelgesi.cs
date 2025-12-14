using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class FirmaSektoruAYRINTIArama
    {
        public Int32? firmaSektorukimlik { get; set; }
        public Int32? i_firmaKimlik { get; set; }
        public string? firmaAdi { get; set; }
        public Int32? i_sektorKimlik { get; set; }
        public string? sektorAdi { get; set; }
        public bool? varmi { get; set; }
        public FirmaSektoruAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<FirmaSektoruAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<FirmaSektoruAYRINTI>(P => P.varmi == true);
            if (firmaSektorukimlik != null)
                predicate = predicate.And(x => x.firmaSektorukimlik == firmaSektorukimlik);
            if (i_firmaKimlik != null)
                predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik);
            if (firmaAdi != null)
                predicate = predicate.And(x => x.firmaAdi != null && x.firmaAdi.Contains(firmaAdi));
            if (i_sektorKimlik != null)
                predicate = predicate.And(x => x.i_sektorKimlik == i_sektorKimlik);
            if (sektorAdi != null)
                predicate = predicate.And(x => x.sektorAdi != null && x.sektorAdi.Contains(sektorAdi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<FirmaSektoruAYRINTI>> cek(veri.Varlik vari)
        {
            List<FirmaSektoruAYRINTI> sonuc = await vari.FirmaSektoruAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<FirmaSektoruAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            FirmaSektoruAYRINTI? sonuc = await vari.FirmaSektoruAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class FirmaSektoruAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<FirmaSektoruAYRINTI>> ara(params Expression<Func<FirmaSektoruAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<FirmaSektoruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaSektoruAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.FirmaSektoruAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.firmaSektorukimlik)
                   .ToListAsync();
        }



        public static async Task<FirmaSektoruAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            FirmaSektoruAYRINTI? kayit = await kime.FirmaSektoruAYRINTIs.FirstOrDefaultAsync(p => p.firmaSektorukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static FirmaSektoruAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            FirmaSektoruAYRINTI? kayit = kime.FirmaSektoruAYRINTIs.FirstOrDefault(p => p.firmaSektorukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

