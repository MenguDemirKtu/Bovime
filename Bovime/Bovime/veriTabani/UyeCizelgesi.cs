using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class UyeArama
    {
        public Int64? uyekimlik { get; set; }
        public string? adi { get; set; }
        public string? soyadi { get; set; }
        public string? telefon { get; set; }
        public string? ePosta { get; set; }
        public DateTime? uyelikTarihi { get; set; }
        public bool? varmi { get; set; }
        public string? kullaniciAdi { get; set; }
        public string? sifre { get; set; }
        public string? adres { get; set; }
        public Int32? i_uyeDurumuKimlik { get; set; }
        public UyeArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<Uye> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<Uye>(P => P.varmi == true);
            if (uyekimlik != null)
                predicate = predicate.And(x => x.uyekimlik == uyekimlik);
            if (adi != null)
                predicate = predicate.And(x => x.adi != null && x.adi.Contains(adi));
            if (soyadi != null)
                predicate = predicate.And(x => x.soyadi != null && x.soyadi.Contains(soyadi));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (uyelikTarihi != null)
                predicate = predicate.And(x => x.uyelikTarihi == uyelikTarihi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (kullaniciAdi != null)
                predicate = predicate.And(x => x.kullaniciAdi != null && x.kullaniciAdi.Contains(kullaniciAdi));
            if (sifre != null)
                predicate = predicate.And(x => x.sifre != null && x.sifre.Contains(sifre));
            if (adres != null)
                predicate = predicate.And(x => x.adres != null && x.adres.Contains(adres));
            if (i_uyeDurumuKimlik != null)
                predicate = predicate.And(x => x.i_uyeDurumuKimlik == i_uyeDurumuKimlik);
            return predicate;

        }
        public async Task<List<Uye>> cek(veri.Varlik vari)
        {
            List<Uye> sonuc = await vari.Uyes
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<Uye?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            Uye? sonuc = await vari.Uyes
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class UyeCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Uye>> ara(params Expression<Func<Uye, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Uye>> ara(veri.Varlik vari, params Expression<Func<Uye, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Uyes
                            .Where(kosul).OrderByDescending(p => p.uyekimlik)
                   .ToListAsync();
        }
        public static async Task<Uye?> bul(veri.Varlik vari, params Expression<Func<Uye, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Uyes.FirstOrDefaultAsync(kosul);
        }



        public static async Task<Uye?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            Uye? kayit = await kime.Uyes.FirstOrDefaultAsync(p => p.uyekimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Uye yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.uyekimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Uyes.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Uye? bulunan = await vari.Uyes.FirstOrDefaultAsync(p => p.uyekimlik == yeni.uyekimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Uye kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Uyes.FirstOrDefaultAsync(p => p.uyekimlik == kimi.uyekimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Uye? tekliCek(Int64 kimlik, Varlik kime)
        {
            Uye? kayit = kime.Uyes.FirstOrDefault(p => p.uyekimlik == kimlik);
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
        public static void kaydet(Uye yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.uyekimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Uyes.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Uyes.FirstOrDefault(p => p.uyekimlik == yeni.uyekimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Uye kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Uyes.FirstOrDefault(p => p.uyekimlik == kimi.uyekimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


