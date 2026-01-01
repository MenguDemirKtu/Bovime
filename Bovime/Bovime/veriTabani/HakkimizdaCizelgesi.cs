using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class HakkimizdaArama
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
        public string? uzunTanitim { get; set; }
        public bool? varmi { get; set; }
        public HakkimizdaArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<Hakkimizda> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<Hakkimizda>(P => P.varmi == true);
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
            if (uzunTanitim != null)
                predicate = predicate.And(x => x.uzunTanitim != null && x.uzunTanitim.Contains(uzunTanitim));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<Hakkimizda>> cek(veri.Varlik vari)
        {
            List<Hakkimizda> sonuc = await vari.Hakkimizdas
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<Hakkimizda?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            Hakkimizda? sonuc = await vari.Hakkimizdas
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class HakkimizdaCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Hakkimizda>> ara(params Expression<Func<Hakkimizda, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Hakkimizda>> ara(veri.Varlik vari, params Expression<Func<Hakkimizda, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Hakkimizdas
                            .Where(kosul).OrderByDescending(p => p.hakkimizdakimlik)
                   .ToListAsync();
        }



        public static async Task<Hakkimizda?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Hakkimizda? kayit = await kime.Hakkimizdas.FirstOrDefaultAsync(p => p.hakkimizdakimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Hakkimizda yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.hakkimizdakimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Hakkimizdas.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Hakkimizda? bulunan = await vari.Hakkimizdas.FirstOrDefaultAsync(p => p.hakkimizdakimlik == yeni.hakkimizdakimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Hakkimizda kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Hakkimizdas.FirstOrDefaultAsync(p => p.hakkimizdakimlik == kimi.hakkimizdakimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Hakkimizda? tekliCek(Int32 kimlik, Varlik kime)
        {
            Hakkimizda? kayit = kime.Hakkimizdas.FirstOrDefault(p => p.hakkimizdakimlik == kimlik);
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
        public static void kaydet(Hakkimizda yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.hakkimizdakimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Hakkimizdas.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Hakkimizdas.FirstOrDefault(p => p.hakkimizdakimlik == yeni.hakkimizdakimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Hakkimizda kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Hakkimizdas.FirstOrDefault(p => p.hakkimizdakimlik == kimi.hakkimizdakimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


