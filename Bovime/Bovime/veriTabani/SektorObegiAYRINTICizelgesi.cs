using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class SektorObegiAYRINTIArama
    {
        public Int32? sektorObegikimlik { get; set; }
        public string? sektorObegiAdi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? e_anaSayfadaGorunsunMu { get; set; }
        public string? anaSayfadaGorunsunMu { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public string? tanitim { get; set; }
        public bool? varmi { get; set; }
        public SektorObegiAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<SektorObegiAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<SektorObegiAYRINTI>(P => P.varmi == true);
            if (sektorObegikimlik != null)
                predicate = predicate.And(x => x.sektorObegikimlik == sektorObegikimlik);
            if (sektorObegiAdi != null)
                predicate = predicate.And(x => x.sektorObegiAdi != null && x.sektorObegiAdi.Contains(sektorObegiAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (e_anaSayfadaGorunsunMu != null)
                predicate = predicate.And(x => x.e_anaSayfadaGorunsunMu == e_anaSayfadaGorunsunMu);
            if (anaSayfadaGorunsunMu != null)
                predicate = predicate.And(x => x.anaSayfadaGorunsunMu != null && x.anaSayfadaGorunsunMu.Contains(anaSayfadaGorunsunMu));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<SektorObegiAYRINTI>> cek(veri.Varlik vari)
        {
            List<SektorObegiAYRINTI> sonuc = await vari.SektorObegiAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<SektorObegiAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            SektorObegiAYRINTI? sonuc = await vari.SektorObegiAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class SektorObegiAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<SektorObegiAYRINTI>> ara(params Expression<Func<SektorObegiAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<SektorObegiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SektorObegiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SektorObegiAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.sektorObegikimlik)
                   .ToListAsync();
        }



        public static async Task<SektorObegiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SektorObegiAYRINTI? kayit = await kime.SektorObegiAYRINTIs.FirstOrDefaultAsync(p => p.sektorObegikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SektorObegiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            SektorObegiAYRINTI? kayit = kime.SektorObegiAYRINTIs.FirstOrDefault(p => p.sektorObegikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

