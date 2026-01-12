using Bovime.veri;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Bovime
{
    public class Genel
    {

        public static bool yenilensinmi = true;
        public static string menuHtml { get; set; }

        public static string mobilMenuHtml { get; set; }


        public static HakkimizdaAYRINTI hakkimizda { get; set; }


        public Genel()
        {
            menuHtml = "";
            mobilMenuHtml = "";
            hakkimizda = new HakkimizdaAYRINTI();
            ustler = new List<SiteMenuAYRINTI>();
        }


        public async static Task yenile(veri.Varlik vari)
        {
            if (yenilensinmi == true)
            {
                await menuOlustur(vari);
                hakkimizda = await vari.HakkimizdaAYRINTIs.FirstOrDefaultAsync() ?? new HakkimizdaAYRINTI();
                yenilensinmi = false;
            }
        }


        public static List<SiteMenuAYRINTI> ustler { get; set; }

        private static void alt(List<SiteMenuAYRINTI> menuler)
        {

            ustler = menuler.Where(p => p.e_altMenuMu == false).OrderBy(p => p.sirasi).ToList();
            mobilMenuHtml = "";
            for (int i = 0; i < ustler.Count; i++)
            {
                var siradaki = ustler[i];
                var altlari = menuler.Where(p => p.e_altMenuMu == true && p.i_ustSiteMenuKimlik == ustler[i].siteMenukimlik).OrderBy(p => p.sirasi).ToList();

                if (altlari.Count == 0)
                {
                    mobilMenuHtml += String.Format(" <li class=\"menu-item menu-item-type-post_type menu-item-object-page\"><a href=\"{1}\"> {0} </a></li>", siradaki.siteMenuAdi, siradaki.menuUrl);
                }
                else
                {

                    mobilMenuHtml += "\t\t<li class=\"menu-item menu-item-type-post_type menu-item-object-page menu-item-home current-menu-item page_item page-item-40 current_page_item current-menu-ancestor current-menu-parent current_page_parent current_page_ancestor menu-item-has-children menu-item-3847\">\r\n\t\t\t\t\t\t\t<a href=\"" + siradaki.menuUrl + "\"> " + siradaki.siteMenuAdi + " </a>\r\n\t\t\t\t\t\t\t<ul class=\"sub-menu\">";


                    for (int k = 0; k < altlari.Count; k++)
                    {
                        mobilMenuHtml += String.Format("\t<li class=\"menu-item menu-item-type-post_type menu-item-object-page\"><a href=\"" + altlari[k].menuUrl + "\">   " + altlari[k].siteMenuAdi + "  </a></li>\r\n\t\t\t\t\t\t\t");

                    }


                    mobilMenuHtml += "</ul>\r\n\t\t\t\t\t\t</li>";

                }
            }
        }
        public static async Task<string> menuOlustur(veri.Varlik vari)
        {
            List<SiteMenuAYRINTI> menuler = await vari.SiteMenuAYRINTIs.ToListAsync();
            ustler = menuler.Where(p => p.e_altMenuMu == false).OrderBy(p => p.sirasi).ToList();


            alt(menuler);

            string tamami = "";
            for (int i = 0; i < ustler.Count; i++)
            {

                var altlari = menuler.Where(p => p.e_altMenuMu == true && p.i_ustSiteMenuKimlik == ustler[i].siteMenukimlik).OrderBy(p => p.sirasi).ToList();

                if (altlari.Count == 0)
                {
                    string siradaki = "<li class=\"menu-item menu-item-type-post_type menu-item-object-page\"  ><a href=\"https://klbtheme.com/partdo/contact/\">" + ustler[i].siteMenuAdi + "</a></li>";

                    siradaki = siradaki.Replace("https://klbtheme.com/partdo/contact/", ustler[i].menuUrl);

                    tamami += siradaki;
                }
                else
                {

                    string ara = @"	<li class=""menu-item menu-item-type-post_type menu-item-object-page menu-item-home current-menu-item page_item page-item-40 current_page_item current-menu-ancestor current-menu-parent current_page_parent current_page_ancestor menu-item-has-children""  >
										<a href=""https://klbtheme.com/partdo/"">" + ustler[i].siteMenuAdi + "</a> ";

                    ara = ara.Replace("https://klbtheme.com/partdo/", "#");

                    ara += "\t<ul class=\"sub-menu\">";

                    tamami += ara;
                    for (int k = 0; k < altlari.Count; k++)
                    {

                        string yeni = " <li class=\"menu-item menu-item-type-post_type menu-item-object-page\"><a href=\"https://klbtheme.com/partdo/home-3/\">   " + altlari[k].siteMenuAdi + "  </a></li> ";
                        yeni = yeni.Replace("https://klbtheme.com/partdo/home-3/", altlari[k].menuUrl);
                        tamami += yeni;
                    }

                    tamami += "\t\t</ul>\r\n\t\t\t\t\t\t\t\t\t</li>";

                }

            }
            menuHtml = tamami;
            return tamami;
        }
        public static DateTime devamGunuOlusturmaTarihi { get; set; }
        public static string baglantiTumcesi = "......";
        public static void yenile()
        {
            using (veri.Varlik vari = new Varlik())
            {
                yazilimAyari = vari.YazilimAyariAYRINTIs.FirstOrDefault() ?? new YazilimAyariAYRINTI();
                sozcukler = vari.BYSSozcukAciklamaAYRINTIs.ToList();
            }
        }
        public static async Task<string> dokumKisaAciklama(Sayfa sayfasi, string cizelgeAdi)
        {

            string? kAdi = sayfasi.HttpContext.Session.GetString("mevcutKullanici");

            if (kAdi == null)
                return "";

            Yonetici? giren = JsonConvert.DeserializeObject<Yonetici>(kAdi);
            sayfasi.ViewBag.mevcut = giren;

            WebSayfasiAYRINTI ayrintisi = await sayfasi.adresBelirleKos(sayfasi, cizelgeAdi);
            sayfasi._sayfaTuru = enum_sayfaTuru.dokum;



            if (ayrintisi != null)
            {
                sayfasi.sayfaKimlik = ayrintisi.webSayfasiKimlik;
                return ayrintisi.dokumAciklamasi ?? "";
            }
            else
            {
                return "Kısa açıklama bulunamadı";
            }
        }


        public static async Task<string> dokumKisaAciklamaKos(Sayfa sayfasi, string cizelgeAdi)
        {
            string? kAdi = sayfasi.HttpContext.Session.GetString("mevcutKullanici");

            if (kAdi == null)
                return "";

            Yonetici? giren = JsonConvert.DeserializeObject<Yonetici>(kAdi);
            sayfasi.ViewBag.mevcut = giren;

            using (veri.Varlik vari = new Varlik())
            {
                WebSayfasiAYRINTI ayrintisi = await sayfasi.adresBelirleKos(vari, sayfasi, cizelgeAdi);
                sayfasi._sayfaTuru = enum_sayfaTuru.dokum;

                if (ayrintisi != null)
                {
                    sayfasi.sayfaKimlik = ayrintisi.webSayfasiKimlik;
                    if (ayrintisi.dokumAciklamasi != null)
                        return ayrintisi.dokumAciklamasi;
                    else
                        return "";
                }
                else
                {
                    return "Kısa açıklama bulunamadı";
                }
            }
        }
        public static int ayinKacindiHaftasi(DateTime tarih)
        {
            DateTime ayinIlkGunu = new DateTime(tarih.Year, tarih.Month, 1);
            DayOfWeek haftaninIlkGunu = DayOfWeek.Monday;
            int adim = (7 + (ayinIlkGunu.DayOfWeek - haftaninIlkGunu)) % 7;
            int gunNumarasi = tarih.Day + adim;
            int haftaNumarasi = (int)Math.Ceiling(gunNumarasi / 7.0);
            return haftaNumarasi;
        }

        public static bool lokalmi { get; set; }
        public static string kayitKonumu = "...";
        private static GunlukGuvenlikAnahtari? _anahtar;
        public static GunlukGuvenlikAnahtari? anahtar
        {
            get
            {
                bool cekilsinmi = false;

                if (_anahtar == null)
                    cekilsinmi = true;
                else
                {
                    if (_anahtar.tarih != DateTime.Today)
                        cekilsinmi = true;
                }

                if (cekilsinmi)
                {
                    using (veri.Varlik vari = new Varlik())
                    {
                        var karsilik = vari.GunlukGuvenlikAnahtaris.FirstOrDefault(p => p.tarih == DateTime.Today && p.varmi == true);
                        if (karsilik != null)
                            _anahtar = karsilik;
                        else
                        {
                            _anahtar = new GunlukGuvenlikAnahtari();
                            _anahtar.tarih = DateTime.Today;
                            _anahtar.varmi = true;
                            _anahtar.anahtar = Guid.NewGuid().ToString();
                            _anahtar.kaydet(vari, false);
                        }
                    }
                }
                return _anahtar;
            }
        }

        public static string tcGizle(string tc)
        {
            if (string.IsNullOrEmpty(tc)) return "";

            if (tc.Length < 3)
                return tc;
            else
            {
                string bas = tc.Substring(0, 3);
                return bas + "*********";
            }
        }

        public static List<BYSSozcukAciklamaAYRINTI>? sozcukler { get; set; }

        public static string adBicimlendir(string ad)
        {
            return ad.ToUpper().Trim();
        }
        public static string soyAdBicimlendir(string soyad)
        {
            return soyad.ToUpper().Trim();
        }

        static string tire = ((char)34).ToString();

        private static string tireArasi(string ifade)
        {
            return tire + ifade + tire;
        }

        public static string cokluSecimHazirla(string id, string name, List<SelectListItem> elemanlar)
        {
            string bas = "<select class=" + tireArasi("form-control special-select") + " id=" + tireArasi(id) + " multiple=" + tireArasi("multiple") + " name = " + tireArasi(name) + " > ";
            for (int i = 0; i < elemanlar.Count; i++)
            {
                string anahtar = elemanlar[i].Value;
                string metin = elemanlar[i].Text;

                if (elemanlar[i].Selected == false)
                    bas += "<option value=" + tireArasi(anahtar) + ">" + metin + "</option>";
                else
                    bas += "  <option value=" + tireArasi(anahtar) + "selected " + tireArasi("selected") + ">" + metin + "</option>";
            }
            bas += "</select>";
            return bas;
        }
        public static string cokluSecimHazirla2(string id, string name, List<SelectListItem> elemanlar)
        {
            string bas = "<select class=" + tireArasi("form-control special-select") + " id=" + tireArasi(id) + " multiple=" + tireArasi("multiple") + " name = " + tireArasi(name) + " > ";
            for (int i = 0; i < elemanlar.Count; i++)
            {
                string anahtar = elemanlar[i].Value;
                string metin = elemanlar[i].Text;

                bas += "<option value=" + tireArasi(anahtar) + ">" + metin + "</option>";
            }
            bas += "</select>";
            return bas;
        }

        public static string jsonaCevir(object deger)
        {
            return JsonConvert.SerializeObject(deger);
        }


        private static YazilimAyariAYRINTI? _yazilimAyari;

        public static YazilimAyariAYRINTI yazilimAyari
        {
            get
            {
                if (_yazilimAyari == null)
                {
                    using (veri.Varlik vari = new Varlik())
                    {
                        _yazilimAyari = vari.YazilimAyariAYRINTIs.FirstOrDefault(p => p.varmi == true);

                        if (_yazilimAyari != null)
                        {
                            if (string.IsNullOrEmpty(_yazilimAyari.surumNumarasi))
                            {
                                _yazilimAyari.surumNumarasi = "1.0";

                            }
                        }
                    }
                }
                return _yazilimAyari ?? new YazilimAyariAYRINTI();
            }
            set
            {
                _yazilimAyari = value;
            }
        }




        public static string epostaBicimlendir(string telno)
        {
            if (telno == null)
                return ".";

            telno = telno.Trim().ToLower();
            telno = telno.Replace("ı", "i");

            int yer = telno.IndexOf("@");
            if (yer == -1)
                return ".";


            yer = telno.IndexOf(".");
            if (yer == -1)
                return ".";

            if (telno.Length == 0)
                return ".";

            return telno;

        }

        public static string telNoBicimlendir(string telNo)
        {
            if (string.IsNullOrEmpty(telNo))
            {
                return ".";
            }

            telNo = telNo.Replace(" ", "");

            if (telNo.IndexOf("0") == 0)
            {
                telNo = telNo.Substring(1);
            }
            if (telNo.IndexOf("90") == 0)
            {
                telNo = telNo.Substring(2);
            }
            long deger = 0;

            if (long.TryParse(telNo, out deger))
                return deger.ToString();
            else
                return ".";

        }


        //public static void tcKimlikDogrula(string tc, string ad, string soyad, DateTime? dogumTarihi)
        //{

        //    long kimlik = Convert.ToInt64(tc);

        //    var client = new tcKimlikServisi.KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
        //    //    var response = await client.TCKimlikNoDogrulaAsync(Convert.ToInt64(11111111111), "ALİ YASİN", "DOĞAN", 1979);
        //    //     var result = response.Body.TCKimlikNoDogrulaResult;

        //    var yanit = client.TCKimlikNoDogrulaAsync(kimlik, ad, soyad, dogumTarihi.Value.Year);
        //    var sonuc = yanit.Result;
        //    var nedir = sonuc;
        //}

        public static string siteAdresi = "https:\\\\bys.taf.org.tr";
        //public static int mevcutSezon = 3;




        public static string tarihIfadesi(DateTime? tarih)
        {
            if (tarih == null)
                return "";
            else
                return String.Format("{0:dd.MM.yyyy}", (DateTime)tarih);
        }
        public static string dilIfadesi(int? dil)
        {
            if (dil == 0)
                return "";
            else
            {
                if (dil == 1)
                    return "Türkçe";
                else
                    return "İngilizce";

            }

        }
        public static string paraIfadesi(decimal? ucret)
        {
            if (ucret == null)
                return "";
            else
                return @Math.Round((decimal)ucret, 2).ToString();
        }

        public static string urlDuzenle(string? ifade)
        {
            if (ifade == null)
                ifade = "";
            ifade = ifade.Replace(' ', '-');
            ifade = ifade.Replace(' ', '-');
            ifade = ifade.ToLower();
            ifade = ifade.Replace('ç', 'c');
            ifade = ifade.Replace('ö', 'o');
            ifade = ifade.Replace('ü', 'u');
            ifade = ifade.Replace('ğ', 'g');
            ifade = ifade.Replace('ı', 'i');
            ifade = ifade.Replace('ş', 's');
            string sonuc = "";
            for (int i = 0; i < ifade.Length; i++)
            {
                char siradaki = ifade[i];
                if (char.IsLetterOrDigit(siradaki))
                    sonuc += siradaki;

                if (siradaki == '-')
                    sonuc += siradaki;

            }
            sonuc = sonuc.Trim();
            return sonuc.ToString();
        }
        public static string cinsiyetIfadesi(byte? cinsiyet)
        {
            if (cinsiyet == 1)
                return "Kadın";
            else
                return "Erkek";
        }
        public static string evetHayirIfadesi(bool? deger)
        {
            if (deger == true)
                return "Evet";
            else
                return "Hayır";
        }
        public static string evetHayirIfadesi(byte? deger)
        {
            if (deger == 1)
                return "Evet";
            else
                return "Hayır";
        }

        public static List<veri.CizelgeAdiAYRINTI> CizelgeAdlari = new List<veri.CizelgeAdiAYRINTI>();

        public static bool yedeklensinmi(params bool[] kayded)
        {
            if (kayded.Length == 0)
            {
                return true;
            }
            else
            {
                return kayded[0];
            }
        }

        public static Yonetici? mevcutKullanici(Sayfa sayfasi)
        {
            string? kAdi = sayfasi.HttpContext.Session.GetString("mevcutKullanici");
            if (kAdi == null)
                return null;
            Yonetici? giren = JsonConvert.DeserializeObject<Yonetici>(kAdi);
            return giren;
        }






    }
}
