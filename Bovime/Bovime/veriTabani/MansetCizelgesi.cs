using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class MansetArama
    {
        public Int32? mansetkimlik { get; set; }
        public string? baslikUst { get; set; }
        public string? baslikOrta { get; set; }
        public string? altYazi { get; set; }
        public Int32? sirasi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public bool? e_yayindami { get; set; }
        public bool? varmi { get; set; }
        public MansetArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<Manset> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<Manset>(P => P.varmi == true);
            if (mansetkimlik != null)
                predicate = predicate.And(x => x.mansetkimlik == mansetkimlik);
            if (baslikUst != null)
                predicate = predicate.And(x => x.baslikUst != null && x.baslikUst.Contains(baslikUst));
            if (baslikOrta != null)
                predicate = predicate.And(x => x.baslikOrta != null && x.baslikOrta.Contains(baslikOrta));
            if (altYazi != null)
                predicate = predicate.And(x => x.altYazi != null && x.altYazi.Contains(altYazi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (e_yayindami != null)
                predicate = predicate.And(x => x.e_yayindami == e_yayindami);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<Manset>> cek(veri.Varlik vari)
        {
            List<Manset> sonuc = await vari.Mansets
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<Manset?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            Manset? sonuc = await vari.Mansets
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class MansetCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Manset>> ara(params Expression<Func<Manset, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Manset>> ara(veri.Varlik vari, params Expression<Func<Manset, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Mansets
                            .Where(kosul).OrderByDescending(p => p.mansetkimlik)
                   .ToListAsync();
        }



        public static async Task<Manset?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Manset? kayit = await kime.Mansets.FirstOrDefaultAsync(p => p.mansetkimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Manset yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.mansetkimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Mansets.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Manset? bulunan = await vari.Mansets.FirstOrDefaultAsync(p => p.mansetkimlik == yeni.mansetkimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Manset kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Mansets.FirstOrDefaultAsync(p => p.mansetkimlik == kimi.mansetkimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Manset? tekliCek(Int32 kimlik, Varlik kime)
        {
            Manset? kayit = kime.Mansets.FirstOrDefault(p => p.mansetkimlik == kimlik);
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
        public static void kaydet(Manset yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.mansetkimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Mansets.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Mansets.FirstOrDefault(p => p.mansetkimlik == yeni.mansetkimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Manset kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Mansets.FirstOrDefault(p => p.mansetkimlik == kimi.mansetkimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


