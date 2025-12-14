using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class FirmaSektoruArama
    {
        public Int32? firmaSektorukimlik { get; set; }
        public Int32? i_firmaKimlik { get; set; }
        public Int32? i_sektorKimlik { get; set; }
        public bool? varmi { get; set; }
        public FirmaSektoruArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<FirmaSektoru> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<FirmaSektoru>(P => P.varmi == true);
            if (firmaSektorukimlik != null)
                predicate = predicate.And(x => x.firmaSektorukimlik == firmaSektorukimlik);
            if (i_firmaKimlik != null)
                predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik);
            if (i_sektorKimlik != null)
                predicate = predicate.And(x => x.i_sektorKimlik == i_sektorKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<FirmaSektoru>> cek(veri.Varlik vari)
        {
            List<FirmaSektoru> sonuc = await vari.FirmaSektorus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<FirmaSektoru?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            FirmaSektoru? sonuc = await vari.FirmaSektorus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class FirmaSektoruCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<FirmaSektoru>> ara(params Expression<Func<FirmaSektoru, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<FirmaSektoru>> ara(veri.Varlik vari, params Expression<Func<FirmaSektoru, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.FirmaSektorus
                            .Where(kosul).OrderByDescending(p => p.firmaSektorukimlik)
                   .ToListAsync();
        }



        public static async Task<FirmaSektoru?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            FirmaSektoru? kayit = await kime.FirmaSektorus.FirstOrDefaultAsync(p => p.firmaSektorukimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(FirmaSektoru yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.firmaSektorukimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.FirmaSektorus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                FirmaSektoru? bulunan = await vari.FirmaSektorus.FirstOrDefaultAsync(p => p.firmaSektorukimlik == yeni.firmaSektorukimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(FirmaSektoru kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.FirmaSektorus.FirstOrDefaultAsync(p => p.firmaSektorukimlik == kimi.firmaSektorukimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static FirmaSektoru? tekliCek(Int32 kimlik, Varlik kime)
        {
            FirmaSektoru? kayit = kime.FirmaSektorus.FirstOrDefault(p => p.firmaSektorukimlik == kimlik);
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
        public static void kaydet(FirmaSektoru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.firmaSektorukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.FirmaSektorus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.FirmaSektorus.FirstOrDefault(p => p.firmaSektorukimlik == yeni.firmaSektorukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(FirmaSektoru kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.FirmaSektorus.FirstOrDefault(p => p.firmaSektorukimlik == kimi.firmaSektorukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


