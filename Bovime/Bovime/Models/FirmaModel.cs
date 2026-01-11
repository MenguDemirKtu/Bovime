using Bovime.GenelIslemler;
using Bovime.veri;
using Bovime.veriTabani;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bovime.Models
{
    public class FirmaModel : ModelTabani
    {
        public Firma kartVerisi { get; set; }
        public List<FirmaAYRINTI> dokumVerisi { get; set; }
        public FirmaAYRINTIArama aramaParametresi { get; set; }


        public FirmaModel()
        {
            this.kartVerisi = new Firma();
            this.dokumVerisi = new List<FirmaAYRINTI>();
            this.aramaParametresi = new FirmaAYRINTIArama();
        }


        public async Task<AramaTalebi> ayrintiliAraKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                AramaTalebi talep = new AramaTalebi();
                talep.kodu = Guid.NewGuid().ToString();
                talep.tarih = DateTime.Now;
                talep.varmi = true;
                talep.talepAyrintisi = Newtonsoft.Json.JsonConvert.SerializeObject(aramaParametresi);
                await veriTabani.AramaTalebiCizelgesi.kaydetKos(talep, vari, false);
                return talep;
            }
        }
        public async Task silKos(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    Firma? silinecek = await Firma.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.FirmaModel modeli = new Models.FirmaModel();
            await modeli.veriCekKos(silen);
        }
        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }




        public async Task<Firma> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                long fk = 0;
                if (long.TryParse(fotoKonumu, out fk))
                    kartVerisi.i_fotoKimlik = fk;
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi.aciklama = Sayfa.dosyaKonumDuzelt(kartVerisi.aciklama, Genel.yazilimAyari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);
                await fotoBicimlendirKos(vari, kartVerisi, kartVerisi.i_fotoKimlik);


                KullaniciAYRINTIArama _arama = new KullaniciAYRINTIArama();
                _arama.i_kullaniciTuruKimlik = (int)enumref_KullaniciTuru.Firma;
                _arama.i_firmaKimlik = kartVerisi.firmakimlik;
                KullaniciAYRINTI? uyan = await _arama.bul(vari);


                if (uyan == null)
                {
                    Kullanici yeni = new Kullanici();
                    yeni.sifre = GuvenlikIslemi.sifrele("123");
                    yeni.kullaniciAdi = Genel.telNoBicimlendir(kartVerisi.telefon);
                    yeni.gercekAdi = kartVerisi.firmaAdi;
                    yeni.i_kullaniciTuruKimlik = (int)enumref_KullaniciTuru.Firma;
                    yeni.ePostaAdresi = kartVerisi.ePosta;
                    yeni.telefon = yeni.kullaniciAdi;
                    yeni.unvan = kartVerisi.firmaAdi + "YETKÝLÝSÝ";
                    yeni.i_dilKimlik = 1;
                    yeni.e_sifreDegisecekmi = true;
                    yeni.e_sozlesmeOnaylandimi = true;
                    yeni.i_firmaKimlik = kartVerisi.firmakimlik;
                    vari.Kullanicis.Add(yeni);
                    await vari.SaveChangesAsync();

                    uyan = await _arama.bul(vari);

                }


                if (uyan != null)
                {
                    List<RolAYRINTI> roller = await vari.RolAYRINTIs.Where(p => p.e_gecerlimi == true
                    && p.e_varsayilanmi == true
                    && p.i_varsayilanOlduguKullaniciTuruKimlik == (int)enumref_KullaniciTuru.Firma).ToListAsync();
                    if (roller.Count > 0)
                    {



                        KullaniciRoluAYRINTIArama _arama3 = new KullaniciRoluAYRINTIArama();
                        _arama3.i_kullaniciKimlik = uyan.kullaniciKimlik;
                        _arama3.i_rolKimlik = roller[0].rolKimlik;
                        _arama3.e_gecerlimi = true;

                        var karsilik3 = await _arama3.bul(vari);



                        if (karsilik3 == null)
                        {
                            KullaniciRolu yeniBag = new KullaniciRolu();
                            yeniBag.e_gecerlimi = true;
                            yeniBag.i_kullaniciKimlik = uyan.kullaniciKimlik;
                            yeniBag.i_rolKimlik = roller[0].rolKimlik;
                            await veriTabani.KullaniciRoluCizelgesi.kaydetKos(yeniBag, vari, false);
                        }

                    }
                }


                List<int> secilenKimlikler = this.firmaSektorleri.ToList();
                List<FirmaSektoru> kayitlilar = await FirmaSektoru.ara(vari, p => p.i_firmaKimlik == kartVerisi.firmakimlik, p => p.varmi == true);

                for (int i = 0; i < kayitlilar.Count; i++)
                {
                    int yer = secilenKimlikler.IndexOf(kayitlilar[i].i_sektorKimlik);
                    if (yer == -1)
                    {
                        await kayitlilar[i].silKos(vari, false);
                    }
                }

                for (int i = 0; i < secilenKimlikler.Count; i++)
                {
                    var karsilik = kayitlilar.FirstOrDefault(p => p.i_sektorKimlik == secilenKimlikler[i]);
                    if (karsilik == null)
                    {
                        // ekle 
                        FirmaSektoru yeni = new FirmaSektoru();
                        yeni.i_firmaKimlik = kartVerisi.firmakimlik;
                        yeni.i_sektorKimlik = secilenKimlikler[i];
                        await yeni.kaydetKos(vari, false);
                    }
                    else
                    {
                        // güncelle 
                    }

                }



                return kartVerisi;
            }
        }


        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await Firma.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<FirmaAYRINTI>();
                await baglilariCek(vari, kime);
                await fotoAyariBelirle(vari, kartVerisi._cizelgeAdi());
                List<FirmaSektoruAYRINTI> liste = await FirmaSektoruAYRINTI.ara(P => P.i_firmaKimlik == kimlik);
                firmaSektorleri = liste.Select(p => p.i_sektorKimlik).ToArray();
            }
        }

        public List<ref_FirmaDurumu> _ayref_FirmaDurumu { get; set; }
        public List<SektorAYRINTI> _aySektorAYRINTI { get; set; }

        public List<PaketAYRINTI> _ayPaketAYRINTI { get; set; }
        public int[] firmaSektorleri { get; set; }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            _ayref_FirmaDurumu = await ref_FirmaDurumu.ara(vari);
            _aySektorAYRINTI = await SektorAYRINTI.ara(vari);
            _ayPaketAYRINTI = await PaketAYRINTI.ara(vari);
            firmaSektorleri = new int[0];
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                FirmaAYRINTIArama kosul = new FirmaAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Firma();
                dokumVerisi = await kosul.cek(vari);
                await baglilariCek(vari, kime);
            }
        }
        public async Task kosulaGoreCek(Yonetici kime, string id)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                var talep = vari.AramaTalebis.FirstOrDefault(p => p.kodu == id);
                if (talep != null)
                {
                    FirmaAYRINTIArama kosul = JsonConvert.DeserializeObject<FirmaAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new FirmaAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new Firma();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
