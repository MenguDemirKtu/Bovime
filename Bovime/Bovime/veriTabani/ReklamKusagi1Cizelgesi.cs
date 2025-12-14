using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class ReklamKusagi1Arama
    {
        public Int32? reklamKusagi1kimlik { get; set; }
        public string? baslik { get; set; }
        public string? metin { get; set; }
        public string? hedefUrl { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public bool? varmi { get; set; }
        public ReklamKusagi1Arama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<ReklamKusagi1> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<ReklamKusagi1>(P => P.varmi == true);
            if (reklamKusagi1kimlik != null)
                predicate = predicate.And(x => x.reklamKusagi1kimlik == reklamKusagi1kimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik != null && x.baslik.Contains(baslik));
            if (metin != null)
                predicate = predicate.And(x => x.metin != null && x.metin.Contains(metin));
            if (hedefUrl != null)
                predicate = predicate.And(x => x.hedefUrl != null && x.hedefUrl.Contains(hedefUrl));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<ReklamKusagi1>> cek(veri.Varlik vari)
        {
            List<ReklamKusagi1> sonuc = await vari.ReklamKusagi1s
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<ReklamKusagi1?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            ReklamKusagi1? sonuc = await vari.ReklamKusagi1s
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class ReklamKusagi1Cizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ReklamKusagi1>> ara(params Expression<Func<ReklamKusagi1, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ReklamKusagi1>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi1, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ReklamKusagi1s
                            .Where(kosul).OrderByDescending(p => p.reklamKusagi1kimlik)
                   .ToListAsync();
        }



        public static async Task<ReklamKusagi1?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ReklamKusagi1? kayit = await kime.ReklamKusagi1s.FirstOrDefaultAsync(p => p.reklamKusagi1kimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(ReklamKusagi1 yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.reklamKusagi1kimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.ReklamKusagi1s.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                ReklamKusagi1? bulunan = await vari.ReklamKusagi1s.FirstOrDefaultAsync(p => p.reklamKusagi1kimlik == yeni.reklamKusagi1kimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(ReklamKusagi1 kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.ReklamKusagi1s.FirstOrDefaultAsync(p => p.reklamKusagi1kimlik == kimi.reklamKusagi1kimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static ReklamKusagi1? tekliCek(Int32 kimlik, Varlik kime)
        {
            ReklamKusagi1? kayit = kime.ReklamKusagi1s.FirstOrDefault(p => p.reklamKusagi1kimlik == kimlik);
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
        public static void kaydet(ReklamKusagi1 yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.reklamKusagi1kimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.ReklamKusagi1s.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ReklamKusagi1s.FirstOrDefault(p => p.reklamKusagi1kimlik == yeni.reklamKusagi1kimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ReklamKusagi1 kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.ReklamKusagi1s.FirstOrDefault(p => p.reklamKusagi1kimlik == kimi.reklamKusagi1kimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


