using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class IletisimTalebiArama
    {
        public Int32? iletisimTalebikimlik { get; set; }
        public string? ad { get; set; }
        public string? ePosta { get; set; }
        public string? telefon { get; set; }
        public string? konu { get; set; }
        public string? ileti { get; set; }
        public DateTime? tarih { get; set; }
        public bool? e_gorulduMu { get; set; }
        public string? ipAdresi { get; set; }
        public bool? tarihDatetimeVarmi { get; set; }
        public bool? varmi { get; set; }
        public IletisimTalebiArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<IletisimTalebi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<IletisimTalebi>(P => P.varmi == true);
            if (iletisimTalebikimlik != null)
                predicate = predicate.And(x => x.iletisimTalebikimlik == iletisimTalebikimlik);
            if (ad != null)
                predicate = predicate.And(x => x.ad != null && x.ad.Contains(ad));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (konu != null)
                predicate = predicate.And(x => x.konu != null && x.konu.Contains(konu));
            if (ileti != null)
                predicate = predicate.And(x => x.ileti != null && x.ileti.Contains(ileti));
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (e_gorulduMu != null)
                predicate = predicate.And(x => x.e_gorulduMu == e_gorulduMu);
            if (ipAdresi != null)
                predicate = predicate.And(x => x.ipAdresi != null && x.ipAdresi.Contains(ipAdresi));
            if (tarihDatetimeVarmi != null)
                predicate = predicate.And(x => x.tarihDatetimeVarmi == tarihDatetimeVarmi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<IletisimTalebi>> cek(veri.Varlik vari)
        {
            List<IletisimTalebi> sonuc = await vari.IletisimTalebis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<IletisimTalebi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            IletisimTalebi? sonuc = await vari.IletisimTalebis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class IletisimTalebiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<IletisimTalebi>> ara(params Expression<Func<IletisimTalebi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<IletisimTalebi>> ara(veri.Varlik vari, params Expression<Func<IletisimTalebi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.IletisimTalebis
                            .Where(kosul).OrderByDescending(p => p.iletisimTalebikimlik)
                   .ToListAsync();
        }
        public static async Task<IletisimTalebi?> bul(veri.Varlik vari, params Expression<Func<IletisimTalebi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.IletisimTalebis.FirstOrDefaultAsync(kosul);
        }



        public static async Task<IletisimTalebi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            IletisimTalebi? kayit = await kime.IletisimTalebis.FirstOrDefaultAsync(p => p.iletisimTalebikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(IletisimTalebi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.iletisimTalebikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.IletisimTalebis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                IletisimTalebi? bulunan = await vari.IletisimTalebis.FirstOrDefaultAsync(p => p.iletisimTalebikimlik == yeni.iletisimTalebikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(IletisimTalebi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.IletisimTalebis.FirstOrDefaultAsync(p => p.iletisimTalebikimlik == kimi.iletisimTalebikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static IletisimTalebi? tekliCek(Int32 kimlik, Varlik kime)
        {
            IletisimTalebi? kayit = kime.IletisimTalebis.FirstOrDefault(p => p.iletisimTalebikimlik == kimlik);
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
        public static void kaydet(IletisimTalebi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.iletisimTalebikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.IletisimTalebis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.IletisimTalebis.FirstOrDefault(p => p.iletisimTalebikimlik == yeni.iletisimTalebikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(IletisimTalebi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.IletisimTalebis.FirstOrDefault(p => p.iletisimTalebikimlik == kimi.iletisimTalebikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


