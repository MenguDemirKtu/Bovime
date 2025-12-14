using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class AnaSayfaMansetAYRINTIArama
    {
        public Int32? anaSayfaMansetkimlik { get; set; }
        public string? baslik { get; set; }
        public string? metin { get; set; }
        public Int32? sirasi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public DateTime? yayinBaslangic { get; set; }
        public DateTime? yayinBitis { get; set; }
        public bool? varmi { get; set; }
        public AnaSayfaMansetAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<AnaSayfaMansetAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<AnaSayfaMansetAYRINTI>(P => P.varmi == true);
            if (anaSayfaMansetkimlik != null)
                predicate = predicate.And(x => x.anaSayfaMansetkimlik == anaSayfaMansetkimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik != null && x.baslik.Contains(baslik));
            if (metin != null)
                predicate = predicate.And(x => x.metin != null && x.metin.Contains(metin));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (yayinBaslangic != null)
                predicate = predicate.And(x => x.yayinBaslangic == yayinBaslangic);
            if (yayinBitis != null)
                predicate = predicate.And(x => x.yayinBitis == yayinBitis);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<AnaSayfaMansetAYRINTI>> cek(veri.Varlik vari)
        {
            List<AnaSayfaMansetAYRINTI> sonuc = await vari.AnaSayfaMansetAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<AnaSayfaMansetAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            AnaSayfaMansetAYRINTI? sonuc = await vari.AnaSayfaMansetAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class AnaSayfaMansetAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<AnaSayfaMansetAYRINTI>> ara(params Expression<Func<AnaSayfaMansetAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<AnaSayfaMansetAYRINTI>> ara(veri.Varlik vari, params Expression<Func<AnaSayfaMansetAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.AnaSayfaMansetAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.anaSayfaMansetkimlik)
                   .ToListAsync();
        }



        public static async Task<AnaSayfaMansetAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            AnaSayfaMansetAYRINTI? kayit = await kime.AnaSayfaMansetAYRINTIs.FirstOrDefaultAsync(p => p.anaSayfaMansetkimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static AnaSayfaMansetAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            AnaSayfaMansetAYRINTI? kayit = kime.AnaSayfaMansetAYRINTIs.FirstOrDefault(p => p.anaSayfaMansetkimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

