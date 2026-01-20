using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class FirmaKampanyaTalebiArama
    {
        public Int32? firmaKampanyaTalebikimlik { get; set; }
        public Int32? i_firmaKimlik { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? metin { get; set; }
        public Int32? sirasi { get; set; }
        public bool? e_gorulduMu { get; set; }
        public bool? varmi { get; set; }
        public DateTime? girisTarihi { get; set; }
        public FirmaKampanyaTalebiArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<FirmaKampanyaTalebi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<FirmaKampanyaTalebi>(P => P.varmi == true);
            if (firmaKampanyaTalebikimlik != null)
                predicate = predicate.And(x => x.firmaKampanyaTalebikimlik == firmaKampanyaTalebikimlik);
            if (i_firmaKimlik != null)
                predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (metin != null)
                predicate = predicate.And(x => x.metin != null && x.metin.Contains(metin));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (e_gorulduMu != null)
                predicate = predicate.And(x => x.e_gorulduMu == e_gorulduMu);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (girisTarihi != null)
                predicate = predicate.And(x => x.girisTarihi == girisTarihi);
            return predicate;

        }
        public async Task<List<FirmaKampanyaTalebi>> cek(veri.Varlik vari)
        {
            List<FirmaKampanyaTalebi> sonuc = await vari.FirmaKampanyaTalebis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<FirmaKampanyaTalebi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            FirmaKampanyaTalebi? sonuc = await vari.FirmaKampanyaTalebis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class FirmaKampanyaTalebiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<FirmaKampanyaTalebi>> ara(params Expression<Func<FirmaKampanyaTalebi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<FirmaKampanyaTalebi>> ara(veri.Varlik vari, params Expression<Func<FirmaKampanyaTalebi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.FirmaKampanyaTalebis
                            .Where(kosul).OrderByDescending(p => p.firmaKampanyaTalebikimlik)
                   .ToListAsync();
        }
        public static async Task<FirmaKampanyaTalebi?> bul(veri.Varlik vari, params Expression<Func<FirmaKampanyaTalebi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.FirmaKampanyaTalebis.FirstOrDefaultAsync(kosul);
        }



        public static async Task<FirmaKampanyaTalebi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            FirmaKampanyaTalebi? kayit = await kime.FirmaKampanyaTalebis.FirstOrDefaultAsync(p => p.firmaKampanyaTalebikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(FirmaKampanyaTalebi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.firmaKampanyaTalebikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.FirmaKampanyaTalebis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                FirmaKampanyaTalebi? bulunan = await vari.FirmaKampanyaTalebis.FirstOrDefaultAsync(p => p.firmaKampanyaTalebikimlik == yeni.firmaKampanyaTalebikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(FirmaKampanyaTalebi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.FirmaKampanyaTalebis.FirstOrDefaultAsync(p => p.firmaKampanyaTalebikimlik == kimi.firmaKampanyaTalebikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static FirmaKampanyaTalebi? tekliCek(Int32 kimlik, Varlik kime)
        {
            FirmaKampanyaTalebi? kayit = kime.FirmaKampanyaTalebis.FirstOrDefault(p => p.firmaKampanyaTalebikimlik == kimlik);
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
        public static void kaydet(FirmaKampanyaTalebi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.firmaKampanyaTalebikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.FirmaKampanyaTalebis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.FirmaKampanyaTalebis.FirstOrDefault(p => p.firmaKampanyaTalebikimlik == yeni.firmaKampanyaTalebikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(FirmaKampanyaTalebi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.FirmaKampanyaTalebis.FirstOrDefault(p => p.firmaKampanyaTalebikimlik == kimi.firmaKampanyaTalebikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


