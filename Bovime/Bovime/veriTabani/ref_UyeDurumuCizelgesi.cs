using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class ref_UyeDurumuArama
    {
        public Int32? UyeDurumuKimlik { get; set; }
        public string? UyeDurumuAdi { get; set; }
        public ref_UyeDurumuArama()
        {
        }

        private ExpressionStarter<ref_UyeDurumu> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<ref_UyeDurumu>();
            if (UyeDurumuKimlik != null)
                predicate = predicate.And(x => x.UyeDurumuKimlik == UyeDurumuKimlik);
            if (UyeDurumuAdi != null)
                predicate = predicate.And(x => x.UyeDurumuAdi != null && x.UyeDurumuAdi.Contains(UyeDurumuAdi));
            return predicate;

        }
        public async Task<List<ref_UyeDurumu>> cek(veri.Varlik vari)
        {
            List<ref_UyeDurumu> sonuc = await vari.ref_UyeDurumus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<ref_UyeDurumu?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            ref_UyeDurumu? sonuc = await vari.ref_UyeDurumus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class ref_UyeDurumuCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ref_UyeDurumu>> ara(params Expression<Func<ref_UyeDurumu, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ref_UyeDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_UyeDurumu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_UyeDurumus
                            .Where(kosul).OrderByDescending(p => p.UyeDurumuKimlik)
                   .ToListAsync();
        }
        public static async Task<ref_UyeDurumu?> bul(veri.Varlik vari, params Expression<Func<ref_UyeDurumu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_UyeDurumus.FirstOrDefaultAsync(kosul);
        }



        public static async Task<ref_UyeDurumu?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_UyeDurumu? kayit = await kime.ref_UyeDurumus.FirstOrDefaultAsync(p => p.UyeDurumuKimlik == kimlik);
            return kayit;
        }




        public static ref_UyeDurumu? tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_UyeDurumu? kayit = kime.ref_UyeDurumus.FirstOrDefault(p => p.UyeDurumuKimlik == kimlik);
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(ref_UyeDurumu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.UyeDurumuKimlik <= 0)
            {
                kime.ref_UyeDurumus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_UyeDurumus.FirstOrDefault(p => p.UyeDurumuKimlik == yeni.UyeDurumuKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_UyeDurumu kimi, Varlik kime)
        {

        }
    }
}


