using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class SiteDuyuruArama
    {
        public Int32? siteDuyurukimlik { get; set; }
        public string? siteDuyuruBasligi { get; set; }
        public string? siteDuyuruUrl { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public DateTime? yayinBaslangici { get; set; }
        public DateTime? yayinBitisi { get; set; }
        public string? tanitim { get; set; }
        public string? metin { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public SiteDuyuruArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<SiteDuyuru> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<SiteDuyuru>(P => P.varmi == true);
            if (siteDuyurukimlik != null)
                predicate = predicate.And(x => x.siteDuyurukimlik == siteDuyurukimlik);
            if (siteDuyuruBasligi != null)
                predicate = predicate.And(x => x.siteDuyuruBasligi != null && x.siteDuyuruBasligi.Contains(siteDuyuruBasligi));
            if (siteDuyuruUrl != null)
                predicate = predicate.And(x => x.siteDuyuruUrl != null && x.siteDuyuruUrl.Contains(siteDuyuruUrl));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
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
        public async Task<List<SiteDuyuru>> cek(veri.Varlik vari)
        {
            List<SiteDuyuru> sonuc = await vari.SiteDuyurus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<SiteDuyuru?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            SiteDuyuru? sonuc = await vari.SiteDuyurus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class SiteDuyuruCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<SiteDuyuru>> ara(params Expression<Func<SiteDuyuru, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<SiteDuyuru>> ara(veri.Varlik vari, params Expression<Func<SiteDuyuru, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SiteDuyurus
                            .Where(kosul).OrderByDescending(p => p.siteDuyurukimlik)
                   .ToListAsync();
        }



        public static async Task<SiteDuyuru?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SiteDuyuru? kayit = await kime.SiteDuyurus.FirstOrDefaultAsync(p => p.siteDuyurukimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(SiteDuyuru yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.siteDuyurukimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.SiteDuyurus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                SiteDuyuru? bulunan = await vari.SiteDuyurus.FirstOrDefaultAsync(p => p.siteDuyurukimlik == yeni.siteDuyurukimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(SiteDuyuru kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.SiteDuyurus.FirstOrDefaultAsync(p => p.siteDuyurukimlik == kimi.siteDuyurukimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static SiteDuyuru? tekliCek(Int32 kimlik, Varlik kime)
        {
            SiteDuyuru? kayit = kime.SiteDuyurus.FirstOrDefault(p => p.siteDuyurukimlik == kimlik);
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
        public static void kaydet(SiteDuyuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.siteDuyurukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SiteDuyurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.SiteDuyurus.FirstOrDefault(p => p.siteDuyurukimlik == yeni.siteDuyurukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(SiteDuyuru kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SiteDuyurus.FirstOrDefault(p => p.siteDuyurukimlik == kimi.siteDuyurukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


