using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class HakkimizdaAYRINTIArama
    {
        public Int32? hakkimizdakimlik { get; set; }
        public string? firmaAdi { get; set; }
        public string? kisaTanitim { get; set; }
        public string? telefon { get; set; }
        public string? ePosta { get; set; }
        public string? adres { get; set; }
        public string? konum { get; set; }
        public string? facebook { get; set; }
        public string? twitter { get; set; }
        public string? instagram { get; set; }
        public string? linkedin { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public string? uzunTanitim { get; set; }
        public bool? varmi { get; set; }
        public HakkimizdaAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<HakkimizdaAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<HakkimizdaAYRINTI>(P => P.varmi == true);
            if (hakkimizdakimlik != null)
                predicate = predicate.And(x => x.hakkimizdakimlik == hakkimizdakimlik);
            if (firmaAdi != null)
                predicate = predicate.And(x => x.firmaAdi != null && x.firmaAdi.Contains(firmaAdi));
            if (kisaTanitim != null)
                predicate = predicate.And(x => x.kisaTanitim != null && x.kisaTanitim.Contains(kisaTanitim));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (adres != null)
                predicate = predicate.And(x => x.adres != null && x.adres.Contains(adres));
            if (konum != null)
                predicate = predicate.And(x => x.konum != null && x.konum.Contains(konum));
            if (facebook != null)
                predicate = predicate.And(x => x.facebook != null && x.facebook.Contains(facebook));
            if (twitter != null)
                predicate = predicate.And(x => x.twitter != null && x.twitter.Contains(twitter));
            if (instagram != null)
                predicate = predicate.And(x => x.instagram != null && x.instagram.Contains(instagram));
            if (linkedin != null)
                predicate = predicate.And(x => x.linkedin != null && x.linkedin.Contains(linkedin));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (uzunTanitim != null)
                predicate = predicate.And(x => x.uzunTanitim != null && x.uzunTanitim.Contains(uzunTanitim));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<HakkimizdaAYRINTI>> cek(veri.Varlik vari)
        {
            List<HakkimizdaAYRINTI> sonuc = await vari.HakkimizdaAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<HakkimizdaAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            HakkimizdaAYRINTI? sonuc = await vari.HakkimizdaAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class HakkimizdaAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<HakkimizdaAYRINTI>> ara(params Expression<Func<HakkimizdaAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<HakkimizdaAYRINTI>> ara(veri.Varlik vari, params Expression<Func<HakkimizdaAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.HakkimizdaAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.hakkimizdakimlik)
                   .ToListAsync();
        }



        public static async Task<HakkimizdaAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            HakkimizdaAYRINTI? kayit = await kime.HakkimizdaAYRINTIs.FirstOrDefaultAsync(p => p.hakkimizdakimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static HakkimizdaAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            HakkimizdaAYRINTI? kayit = kime.HakkimizdaAYRINTIs.FirstOrDefault(p => p.hakkimizdakimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

