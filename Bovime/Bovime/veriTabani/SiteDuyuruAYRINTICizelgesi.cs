using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class SiteDuyuruAYRINTIArama
    {
        public Int32? siteDuyurukimlik { get; set; }
        public string? siteDuyuruBasligi { get; set; }
        public string? siteDuyuruUrl { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public DateTime? yayinBaslangici { get; set; }
        public DateTime? yayinBitisi { get; set; }
        public string? tanitim { get; set; }
        public string? metin { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public SiteDuyuruAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<SiteDuyuruAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<SiteDuyuruAYRINTI>(P => P.varmi == true);
            if (siteDuyurukimlik != null)
                predicate = predicate.And(x => x.siteDuyurukimlik == siteDuyurukimlik);
            if (siteDuyuruBasligi != null)
                predicate = predicate.And(x => x.siteDuyuruBasligi != null && x.siteDuyuruBasligi.Contains(siteDuyuruBasligi));
            if (siteDuyuruUrl != null)
                predicate = predicate.And(x => x.siteDuyuruUrl != null && x.siteDuyuruUrl.Contains(siteDuyuruUrl));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (yayinBaslangici != null)
                predicate = predicate.And(x => x.yayinBaslangici == yayinBaslangici);
            if (yayinBitisi != null)
                predicate = predicate.And(x => x.yayinBitisi == yayinBitisi);
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (metin != null)
                predicate = predicate.And(x => x.metin != null && x.metin.Contains(metin));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<SiteDuyuruAYRINTI>> cek(veri.Varlik vari)
        {
            List<SiteDuyuruAYRINTI> sonuc = await vari.SiteDuyuruAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<SiteDuyuruAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            SiteDuyuruAYRINTI? sonuc = await vari.SiteDuyuruAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class SiteDuyuruAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<SiteDuyuruAYRINTI>> ara(params Expression<Func<SiteDuyuruAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<SiteDuyuruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SiteDuyuruAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SiteDuyuruAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.siteDuyurukimlik)
                   .ToListAsync();
        }



        public static async Task<SiteDuyuruAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SiteDuyuruAYRINTI? kayit = await kime.SiteDuyuruAYRINTIs.FirstOrDefaultAsync(p => p.siteDuyurukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SiteDuyuruAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            SiteDuyuruAYRINTI? kayit = kime.SiteDuyuruAYRINTIs.FirstOrDefault(p => p.siteDuyurukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

