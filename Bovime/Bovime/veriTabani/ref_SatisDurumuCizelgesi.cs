using Bovime.veri;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bovime.veriTabani
{

    public class ref_SatisDurumuArama
    {
        public Int32? SatisDurumuKimlik { get; set; }
        public string? SatisDurumuAdi { get; set; }
        public ref_SatisDurumuArama()
        {
        }

        private ExpressionStarter<ref_SatisDurumu> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<ref_SatisDurumu>();
            if (SatisDurumuKimlik != null)
                predicate = predicate.And(x => x.SatisDurumuKimlik == SatisDurumuKimlik);
            if (SatisDurumuAdi != null)
                predicate = predicate.And(x => x.SatisDurumuAdi != null && x.SatisDurumuAdi.Contains(SatisDurumuAdi));
            return predicate;

        }
        public async Task<List<ref_SatisDurumu>> cek(veri.Varlik vari)
        {
            List<ref_SatisDurumu> sonuc = await vari.ref_SatisDurumus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<ref_SatisDurumu?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            ref_SatisDurumu? sonuc = await vari.ref_SatisDurumus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class ref_SatisDurumuCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ref_SatisDurumu>> ara(params Expression<Func<ref_SatisDurumu, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ref_SatisDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_SatisDurumu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_SatisDurumus
                            .Where(kosul).OrderByDescending(p => p.SatisDurumuKimlik)
                   .ToListAsync();
        }
        public static async Task<ref_SatisDurumu?> bul(veri.Varlik vari, params Expression<Func<ref_SatisDurumu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_SatisDurumus.FirstOrDefaultAsync(kosul);
        }



        public static async Task<ref_SatisDurumu?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_SatisDurumu? kayit = await kime.ref_SatisDurumus.FirstOrDefaultAsync(p => p.SatisDurumuKimlik == kimlik);
            return kayit;
        }




        public static ref_SatisDurumu? tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_SatisDurumu? kayit = kime.ref_SatisDurumus.FirstOrDefault(p => p.SatisDurumuKimlik == kimlik);
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(ref_SatisDurumu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.SatisDurumuKimlik <= 0)
            {
                kime.ref_SatisDurumus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_SatisDurumus.FirstOrDefault(p => p.SatisDurumuKimlik == yeni.SatisDurumuKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_SatisDurumu kimi, Varlik kime)
        {

        }
    }
}


