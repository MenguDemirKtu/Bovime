using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class BovimisayfasiArama
    {
        public Int32? bovimisayfasikimlik { get; set; }
        public string? bovimiSayfasiBaslik { get; set; }
        public string? title { get; set; }
        public string? url { get; set; }
        public string? tanitim { get; set; }
        public bool? e_yayindaMi { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public BovimisayfasiArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<Bovimisayfasi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<Bovimisayfasi>(P => P.varmi == true);
            if (bovimisayfasikimlik != null)
                predicate = predicate.And(x => x.bovimisayfasikimlik == bovimisayfasikimlik);
            if (bovimiSayfasiBaslik != null)
                predicate = predicate.And(x => x.bovimiSayfasiBaslik != null && x.bovimiSayfasiBaslik.Contains(bovimiSayfasiBaslik));
            if (title != null)
                predicate = predicate.And(x => x.title != null && x.title.Contains(title));
            if (url != null)
                predicate = predicate.And(x => x.url != null && x.url.Contains(url));
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (e_yayindaMi != null)
                predicate = predicate.And(x => x.e_yayindaMi == e_yayindaMi);
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<Bovimisayfasi>> cek(veri.Varlik vari)
        {
            List<Bovimisayfasi> sonuc = await vari.Bovimisayfasis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<Bovimisayfasi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            Bovimisayfasi? sonuc = await vari.Bovimisayfasis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class BovimisayfasiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Bovimisayfasi>> ara(params Expression<Func<Bovimisayfasi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Bovimisayfasi>> ara(veri.Varlik vari, params Expression<Func<Bovimisayfasi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Bovimisayfasis
                            .Where(kosul).OrderByDescending(p => p.bovimisayfasikimlik)
                   .ToListAsync();
        }
        public static async Task<Bovimisayfasi?> bul(veri.Varlik vari, params Expression<Func<Bovimisayfasi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Bovimisayfasis.FirstOrDefaultAsync(kosul);
        }



        public static async Task<Bovimisayfasi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Bovimisayfasi? kayit = await kime.Bovimisayfasis.FirstOrDefaultAsync(p => p.bovimisayfasikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Bovimisayfasi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.bovimisayfasikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Bovimisayfasis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Bovimisayfasi? bulunan = await vari.Bovimisayfasis.FirstOrDefaultAsync(p => p.bovimisayfasikimlik == yeni.bovimisayfasikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Bovimisayfasi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Bovimisayfasis.FirstOrDefaultAsync(p => p.bovimisayfasikimlik == kimi.bovimisayfasikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Bovimisayfasi? tekliCek(Int32 kimlik, Varlik kime)
        {
            Bovimisayfasi? kayit = kime.Bovimisayfasis.FirstOrDefault(p => p.bovimisayfasikimlik == kimlik);
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
        public static void kaydet(Bovimisayfasi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.bovimisayfasikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Bovimisayfasis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Bovimisayfasis.FirstOrDefault(p => p.bovimisayfasikimlik == yeni.bovimisayfasikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Bovimisayfasi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Bovimisayfasis.FirstOrDefault(p => p.bovimisayfasikimlik == kimi.bovimisayfasikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


