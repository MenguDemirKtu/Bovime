using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class FirmaArama
    {
        public Int32? firmakimlik { get; set; }
        public string? firmaAdi { get; set; }
        public Int32? i_firmaDurumuKimlik { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? telefon { get; set; }
        public string? telefon2 { get; set; }
        public string? ePosta { get; set; }
        public string? adres { get; set; }
        public string? tanitim { get; set; }
        public string? konum { get; set; }
        public Int32? sirasi { get; set; }
        public Int32? puani { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public string? firmaUrl { get; set; }
        public FirmaArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<Firma> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<Firma>(P => P.varmi == true);
            if (firmakimlik != null)
                predicate = predicate.And(x => x.firmakimlik == firmakimlik);
            if (firmaAdi != null)
                predicate = predicate.And(x => x.firmaAdi != null && x.firmaAdi.Contains(firmaAdi));
            if (i_firmaDurumuKimlik != null)
                predicate = predicate.And(x => x.i_firmaDurumuKimlik == i_firmaDurumuKimlik);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (telefon2 != null)
                predicate = predicate.And(x => x.telefon2 != null && x.telefon2.Contains(telefon2));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (adres != null)
                predicate = predicate.And(x => x.adres != null && x.adres.Contains(adres));
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (konum != null)
                predicate = predicate.And(x => x.konum != null && x.konum.Contains(konum));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (puani != null)
                predicate = predicate.And(x => x.puani == puani);
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (firmaUrl != null)
                predicate = predicate.And(x => x.firmaUrl != null && x.firmaUrl.Contains(firmaUrl));
            return predicate;

        }
        public async Task<List<Firma>> cek(veri.Varlik vari)
        {
            List<Firma> sonuc = await vari.Firmas
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<Firma?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            Firma? sonuc = await vari.Firmas
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class FirmaCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Firma>> ara(params Expression<Func<Firma, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Firma>> ara(veri.Varlik vari, params Expression<Func<Firma, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Firmas
                            .Where(kosul).OrderByDescending(p => p.firmakimlik)
                   .ToListAsync();
        }
        public static async Task<Firma?> bul(veri.Varlik vari, params Expression<Func<Firma, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Firmas.FirstOrDefaultAsync(kosul);
        }



        public static async Task<Firma?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Firma? kayit = await kime.Firmas.FirstOrDefaultAsync(p => p.firmakimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Firma yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.firmakimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Firmas.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Firma? bulunan = await vari.Firmas.FirstOrDefaultAsync(p => p.firmakimlik == yeni.firmakimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Firma kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Firmas.FirstOrDefaultAsync(p => p.firmakimlik == kimi.firmakimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Firma? tekliCek(Int32 kimlik, Varlik kime)
        {
            Firma? kayit = kime.Firmas.FirstOrDefault(p => p.firmakimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(Firma yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.firmakimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Firmas.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Firmas.FirstOrDefault(p => p.firmakimlik == yeni.firmakimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Firma kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Firmas.FirstOrDefault(p => p.firmakimlik == kimi.firmakimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


