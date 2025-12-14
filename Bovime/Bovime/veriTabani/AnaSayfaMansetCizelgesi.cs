using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class AnaSayfaMansetArama
    {
        public Int32? anaSayfaMansetkimlik { get; set; }
        public string? baslik { get; set; }
        public string? metin { get; set; }
        public Int32? sirasi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public DateTime? yayinBaslangic { get; set; }
        public DateTime? yayinBitis { get; set; }
        public bool? varmi { get; set; }
        public AnaSayfaMansetArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<AnaSayfaManset> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<AnaSayfaManset>(P => P.varmi == true);
            if (anaSayfaMansetkimlik != null)
                predicate = predicate.And(x => x.anaSayfaMansetkimlik == anaSayfaMansetkimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik != null && x.baslik.Contains(baslik));
            if (metin != null)
                predicate = predicate.And(x => x.metin != null && x.metin.Contains(metin));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (yayinBaslangic != null)
                predicate = predicate.And(x => x.yayinBaslangic == yayinBaslangic);
            if (yayinBitis != null)
                predicate = predicate.And(x => x.yayinBitis == yayinBitis);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<AnaSayfaManset>> cek(veri.Varlik vari)
        {
            List<AnaSayfaManset> sonuc = await vari.AnaSayfaMansets
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<AnaSayfaManset?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            AnaSayfaManset? sonuc = await vari.AnaSayfaMansets
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class AnaSayfaMansetCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<AnaSayfaManset>> ara(params Expression<Func<AnaSayfaManset, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<AnaSayfaManset>> ara(veri.Varlik vari, params Expression<Func<AnaSayfaManset, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.AnaSayfaMansets
                            .Where(kosul).OrderByDescending(p => p.anaSayfaMansetkimlik)
                   .ToListAsync();
        }



        public static async Task<AnaSayfaManset?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            AnaSayfaManset? kayit = await kime.AnaSayfaMansets.FirstOrDefaultAsync(p => p.anaSayfaMansetkimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(AnaSayfaManset yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.anaSayfaMansetkimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.AnaSayfaMansets.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                AnaSayfaManset? bulunan = await vari.AnaSayfaMansets.FirstOrDefaultAsync(p => p.anaSayfaMansetkimlik == yeni.anaSayfaMansetkimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(AnaSayfaManset kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.AnaSayfaMansets.FirstOrDefaultAsync(p => p.anaSayfaMansetkimlik == kimi.anaSayfaMansetkimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static AnaSayfaManset? tekliCek(Int32 kimlik, Varlik kime)
        {
            AnaSayfaManset? kayit = kime.AnaSayfaMansets.FirstOrDefault(p => p.anaSayfaMansetkimlik == kimlik);
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
        public static void kaydet(AnaSayfaManset yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.anaSayfaMansetkimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.AnaSayfaMansets.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.AnaSayfaMansets.FirstOrDefault(p => p.anaSayfaMansetkimlik == yeni.anaSayfaMansetkimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(AnaSayfaManset kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.AnaSayfaMansets.FirstOrDefault(p => p.anaSayfaMansetkimlik == kimi.anaSayfaMansetkimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


