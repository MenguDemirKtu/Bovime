using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class AltMenuArama
    {
        public Int32? altMenukimlik { get; set; }
        public string? altMenuBaslik { get; set; }
        public string? menuAdi { get; set; }
        public string? menuUrl { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public AltMenuArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<AltMenu> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<AltMenu>(P => P.varmi == true);
            if (altMenukimlik != null)
                predicate = predicate.And(x => x.altMenukimlik == altMenukimlik);
            if (altMenuBaslik != null)
                predicate = predicate.And(x => x.altMenuBaslik != null && x.altMenuBaslik.Contains(altMenuBaslik));
            if (menuAdi != null)
                predicate = predicate.And(x => x.menuAdi != null && x.menuAdi.Contains(menuAdi));
            if (menuUrl != null)
                predicate = predicate.And(x => x.menuUrl != null && x.menuUrl.Contains(menuUrl));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<AltMenu>> cek(veri.Varlik vari)
        {
            List<AltMenu> sonuc = await vari.AltMenus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<AltMenu?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            AltMenu? sonuc = await vari.AltMenus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class AltMenuCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<AltMenu>> ara(params Expression<Func<AltMenu, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<AltMenu>> ara(veri.Varlik vari, params Expression<Func<AltMenu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.AltMenus
                            .Where(kosul).OrderByDescending(p => p.altMenukimlik)
                   .ToListAsync();
        }
        public static async Task<AltMenu?> bul(veri.Varlik vari, params Expression<Func<AltMenu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.AltMenus.FirstOrDefaultAsync(kosul);
        }



        public static async Task<AltMenu?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            AltMenu? kayit = await kime.AltMenus.FirstOrDefaultAsync(p => p.altMenukimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(AltMenu yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.altMenukimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.AltMenus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                AltMenu? bulunan = await vari.AltMenus.FirstOrDefaultAsync(p => p.altMenukimlik == yeni.altMenukimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(AltMenu kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.AltMenus.FirstOrDefaultAsync(p => p.altMenukimlik == kimi.altMenukimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static AltMenu? tekliCek(Int32 kimlik, Varlik kime)
        {
            AltMenu? kayit = kime.AltMenus.FirstOrDefault(p => p.altMenukimlik == kimlik);
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
        public static void kaydet(AltMenu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.altMenukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.AltMenus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.AltMenus.FirstOrDefault(p => p.altMenukimlik == yeni.altMenukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(AltMenu kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.AltMenus.FirstOrDefault(p => p.altMenukimlik == kimi.altMenukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


