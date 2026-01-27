using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class IletisimTalebiAYRINTIArama
    {
        public Int32? iletisimTalebikimlik { get; set; }
        public string? ad { get; set; }
        public string? ePosta { get; set; }
        public string? telefon { get; set; }
        public string? konu { get; set; }
        public string? ileti { get; set; }
        public DateTime? tarih { get; set; }
        public bool? e_gorulduMu { get; set; }
        public string? gorulduMu { get; set; }
        public string? ipAdresi { get; set; }
        public bool? tarihDatetimeVarmi { get; set; }
        public bool? varmi { get; set; }
        public IletisimTalebiAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<IletisimTalebiAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<IletisimTalebiAYRINTI>(P => P.varmi == true);
            if (iletisimTalebikimlik != null)
                predicate = predicate.And(x => x.iletisimTalebikimlik == iletisimTalebikimlik);
            if (ad != null)
                predicate = predicate.And(x => x.ad != null && x.ad.Contains(ad));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (konu != null)
                predicate = predicate.And(x => x.konu != null && x.konu.Contains(konu));
            if (ileti != null)
                predicate = predicate.And(x => x.ileti != null && x.ileti.Contains(ileti));
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (e_gorulduMu != null)
                predicate = predicate.And(x => x.e_gorulduMu == e_gorulduMu);
            if (gorulduMu != null)
                predicate = predicate.And(x => x.gorulduMu != null && x.gorulduMu.Contains(gorulduMu));
            if (ipAdresi != null)
                predicate = predicate.And(x => x.ipAdresi != null && x.ipAdresi.Contains(ipAdresi));
            if (tarihDatetimeVarmi != null)
                predicate = predicate.And(x => x.tarihDatetimeVarmi == tarihDatetimeVarmi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<IletisimTalebiAYRINTI>> cek(veri.Varlik vari)
        {
            List<IletisimTalebiAYRINTI> sonuc = await vari.IletisimTalebiAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<IletisimTalebiAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            IletisimTalebiAYRINTI? sonuc = await vari.IletisimTalebiAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class IletisimTalebiAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<IletisimTalebiAYRINTI>> ara(params Expression<Func<IletisimTalebiAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<IletisimTalebiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<IletisimTalebiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.IletisimTalebiAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.iletisimTalebikimlik)
                   .ToListAsync();
        }
        public static async Task<IletisimTalebiAYRINTI?> bul(veri.Varlik vari, params Expression<Func<IletisimTalebiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.IletisimTalebiAYRINTIs.FirstOrDefaultAsync(kosul);
        }



        public static async Task<IletisimTalebiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            IletisimTalebiAYRINTI? kayit = await kime.IletisimTalebiAYRINTIs.FirstOrDefaultAsync(p => p.iletisimTalebikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static IletisimTalebiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            IletisimTalebiAYRINTI? kayit = kime.IletisimTalebiAYRINTIs.FirstOrDefault(p => p.iletisimTalebikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

