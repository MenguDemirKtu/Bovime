using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class SektorArama
    {
        public Int32? sektorkimlik { get; set; }
        public string? sektorAdi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? tanitim { get; set; }
        public bool? varmi { get; set; }
        public SektorArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<Sektor> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<Sektor>(P => P.varmi == true);
            if (sektorkimlik != null)
                predicate = predicate.And(x => x.sektorkimlik == sektorkimlik);
            if (sektorAdi != null)
                predicate = predicate.And(x => x.sektorAdi != null && x.sektorAdi.Contains(sektorAdi));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<Sektor>> cek(veri.Varlik vari)
        {
            List<Sektor> sonuc = await vari.Sektors
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<Sektor?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            Sektor? sonuc = await vari.Sektors
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class SektorCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Sektor>> ara(params Expression<Func<Sektor, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Sektor>> ara(veri.Varlik vari, params Expression<Func<Sektor, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Sektors
                            .Where(kosul).OrderByDescending(p => p.sektorkimlik)
                   .ToListAsync();
        }



        public static async Task<Sektor?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Sektor? kayit = await kime.Sektors.FirstOrDefaultAsync(p => p.sektorkimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Sektor yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.sektorkimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Sektors.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Sektor? bulunan = await vari.Sektors.FirstOrDefaultAsync(p => p.sektorkimlik == yeni.sektorkimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Sektor kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Sektors.FirstOrDefaultAsync(p => p.sektorkimlik == kimi.sektorkimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Sektor? tekliCek(Int32 kimlik, Varlik kime)
        {
            Sektor? kayit = kime.Sektors.FirstOrDefault(p => p.sektorkimlik == kimlik);
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
        public static void kaydet(Sektor yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sektorkimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Sektors.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Sektors.FirstOrDefault(p => p.sektorkimlik == yeni.sektorkimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Sektor kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Sektors.FirstOrDefault(p => p.sektorkimlik == kimi.sektorkimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


