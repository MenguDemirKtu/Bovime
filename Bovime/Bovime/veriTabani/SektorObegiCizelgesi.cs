using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class SektorObegiArama
    {
        public Int32? sektorObegikimlik { get; set; }
        public string? sektorObegiAdi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? e_anaSayfadaGorunsunMu { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? tanitim { get; set; }
        public bool? varmi { get; set; }
        public SektorObegiArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<SektorObegi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<SektorObegi>(P => P.varmi == true);
            if (sektorObegikimlik != null)
                predicate = predicate.And(x => x.sektorObegikimlik == sektorObegikimlik);
            if (sektorObegiAdi != null)
                predicate = predicate.And(x => x.sektorObegiAdi != null && x.sektorObegiAdi.Contains(sektorObegiAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (e_anaSayfadaGorunsunMu != null)
                predicate = predicate.And(x => x.e_anaSayfadaGorunsunMu == e_anaSayfadaGorunsunMu);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<SektorObegi>> cek(veri.Varlik vari)
        {
            List<SektorObegi> sonuc = await vari.SektorObegis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<SektorObegi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            SektorObegi? sonuc = await vari.SektorObegis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class SektorObegiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<SektorObegi>> ara(params Expression<Func<SektorObegi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<SektorObegi>> ara(veri.Varlik vari, params Expression<Func<SektorObegi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SektorObegis
                            .Where(kosul).OrderByDescending(p => p.sektorObegikimlik)
                   .ToListAsync();
        }



        public static async Task<SektorObegi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SektorObegi? kayit = await kime.SektorObegis.FirstOrDefaultAsync(p => p.sektorObegikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(SektorObegi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.sektorObegikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.SektorObegis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                SektorObegi? bulunan = await vari.SektorObegis.FirstOrDefaultAsync(p => p.sektorObegikimlik == yeni.sektorObegikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(SektorObegi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.SektorObegis.FirstOrDefaultAsync(p => p.sektorObegikimlik == kimi.sektorObegikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static SektorObegi? tekliCek(Int32 kimlik, Varlik kime)
        {
            SektorObegi? kayit = kime.SektorObegis.FirstOrDefault(p => p.sektorObegikimlik == kimlik);
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
        public static void kaydet(SektorObegi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sektorObegikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SektorObegis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.SektorObegis.FirstOrDefault(p => p.sektorObegikimlik == yeni.sektorObegikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(SektorObegi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SektorObegis.FirstOrDefault(p => p.sektorObegikimlik == kimi.sektorObegikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


