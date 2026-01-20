using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class FirmaGrubuArama
    {
        public Int32? firmaGrubukimlik { get; set; }
        public string? firmaGrupAdi { get; set; }
        public Int32? sirasi { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public FirmaGrubuArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<FirmaGrubu> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<FirmaGrubu>(P => P.varmi == true);
            if (firmaGrubukimlik != null)
                predicate = predicate.And(x => x.firmaGrubukimlik == firmaGrubukimlik);
            if (firmaGrupAdi != null)
                predicate = predicate.And(x => x.firmaGrupAdi != null && x.firmaGrupAdi.Contains(firmaGrupAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<FirmaGrubu>> cek(veri.Varlik vari)
        {
            List<FirmaGrubu> sonuc = await vari.FirmaGrubus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<FirmaGrubu?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            FirmaGrubu? sonuc = await vari.FirmaGrubus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class FirmaGrubuCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<FirmaGrubu>> ara(params Expression<Func<FirmaGrubu, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<FirmaGrubu>> ara(veri.Varlik vari, params Expression<Func<FirmaGrubu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.FirmaGrubus
                            .Where(kosul).OrderByDescending(p => p.firmaGrubukimlik)
                   .ToListAsync();
        }



        public static async Task<FirmaGrubu?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            FirmaGrubu? kayit = await kime.FirmaGrubus.FirstOrDefaultAsync(p => p.firmaGrubukimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(FirmaGrubu yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.firmaGrubukimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.FirmaGrubus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                FirmaGrubu? bulunan = await vari.FirmaGrubus.FirstOrDefaultAsync(p => p.firmaGrubukimlik == yeni.firmaGrubukimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(FirmaGrubu kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.FirmaGrubus.FirstOrDefaultAsync(p => p.firmaGrubukimlik == kimi.firmaGrubukimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static FirmaGrubu? tekliCek(Int32 kimlik, Varlik kime)
        {
            FirmaGrubu? kayit = kime.FirmaGrubus.FirstOrDefault(p => p.firmaGrubukimlik == kimlik);
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
        public static void kaydet(FirmaGrubu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.firmaGrubukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.FirmaGrubus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.FirmaGrubus.FirstOrDefault(p => p.firmaGrubukimlik == yeni.firmaGrubukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(FirmaGrubu kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.FirmaGrubus.FirstOrDefault(p => p.firmaGrubukimlik == kimi.firmaGrubukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


