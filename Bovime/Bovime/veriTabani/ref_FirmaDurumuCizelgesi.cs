using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class ref_FirmaDurumuArama
    {
        public Int32? FirmaDurumuKimlik { get; set; }
        public string? FirmaDurumuAdi { get; set; }
        public ref_FirmaDurumuArama()
        {
        }

        private ExpressionStarter<ref_FirmaDurumu> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<ref_FirmaDurumu>();
            if (FirmaDurumuKimlik != null)
                predicate = predicate.And(x => x.FirmaDurumuKimlik == FirmaDurumuKimlik);
            if (FirmaDurumuAdi != null)
                predicate = predicate.And(x => x.FirmaDurumuAdi != null && x.FirmaDurumuAdi.Contains(FirmaDurumuAdi));
            return predicate;

        }
        public async Task<List<ref_FirmaDurumu>> cek(veri.Varlik vari)
        {
            List<ref_FirmaDurumu> sonuc = await vari.ref_FirmaDurumus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<ref_FirmaDurumu?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            ref_FirmaDurumu? sonuc = await vari.ref_FirmaDurumus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class ref_FirmaDurumuCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ref_FirmaDurumu>> ara(params Expression<Func<ref_FirmaDurumu, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ref_FirmaDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_FirmaDurumu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_FirmaDurumus
                            .Where(kosul).OrderByDescending(p => p.FirmaDurumuKimlik)
                   .ToListAsync();
        }



        public static async Task<ref_FirmaDurumu?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_FirmaDurumu? kayit = await kime.ref_FirmaDurumus.FirstOrDefaultAsync(p => p.FirmaDurumuKimlik == kimlik);
            return kayit;
        }




        public static ref_FirmaDurumu? tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_FirmaDurumu? kayit = kime.ref_FirmaDurumus.FirstOrDefault(p => p.FirmaDurumuKimlik == kimlik);
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(ref_FirmaDurumu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.FirmaDurumuKimlik <= 0)
            {
                kime.ref_FirmaDurumus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_FirmaDurumus.FirstOrDefault(p => p.FirmaDurumuKimlik == yeni.FirmaDurumuKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_FirmaDurumu kimi, Varlik kime)
        {

        }
    }
}


