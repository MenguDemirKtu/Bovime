using Bovime.veri;
using Bovime.veriTabani;
using Newtonsoft.Json;
namespace Bovime.Models
{
    public class FirmaKampanyaTalebiModel : ModelTabani
    {
        public FirmaKampanyaTalebi kartVerisi { get; set; }
        public List<FirmaKampanyaTalebiAYRINTI> dokumVerisi { get; set; }
        public FirmaKampanyaTalebiAYRINTIArama aramaParametresi { get; set; }


        public FirmaKampanyaTalebiModel()
        {
            this.kartVerisi = new FirmaKampanyaTalebi();
            this.dokumVerisi = new List<FirmaKampanyaTalebiAYRINTI>();
            this.aramaParametresi = new FirmaKampanyaTalebiAYRINTIArama();
        }


        public async Task<long> kampanyayaCevir(veri.Varlik vari, FirmaKampanyaTalebi talep)
        {
            FirmaKampanyasi yeni = new FirmaKampanyasi();
            yeni.baslik = talep.baslik;
            yeni.metin = talep.metin;
            yeni.aciklama = talep.metin;
            yeni.i_firmaKimlik = talep.i_firmaKimlik;
            yeni.i_fotoKimlik = talep.i_fotoKimlik;
            yeni.i_kampanyaDurumuKimlik = (int)enumref_KampanyaDurumu.Firma_Girdi_Bekliyor;
            yeni.y_firmaKampanyaTalebiKimlik = talep.firmaKampanyaTalebikimlik;
            await yeni.kaydetKos(vari, false);
            return yeni.firmaKampanyasikimlik;
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
        private async Task ekkosulEkle(veri.Varlik vari, Yonetici kime, FirmaKampanyaTalebiAYRINTIArama kosul)
        {
            if (kime._KullaniciTuru == enumref_KullaniciTuru.Firma)
            {
                kosul.i_firmaKimlik = kime.i_firmaKimlik;
            }
        }
        public async Task silKos(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    FirmaKampanyaTalebi? silinecek = await FirmaKampanyaTalebi.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.FirmaKampanyaTalebiModel modeli = new Models.FirmaKampanyaTalebiModel();
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




        public async Task<FirmaKampanyaTalebi> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                long fk = 0;
                if (long.TryParse(fotoKonumu, out fk))
                    kartVerisi.i_fotoKimlik = fk;
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);
                await fotoBicimlendirKos(vari, kartVerisi, kartVerisi.i_fotoKimlik);
                return kartVerisi;
            }
        }

        public async Task<long> kabulEtKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                long fk = 0;
                if (long.TryParse(fotoKonumu, out fk))
                    kartVerisi.i_fotoKimlik = fk;
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.i_kampanyaDurumuKimlik = (int)enumref_KampanyaDurumu.Yayinda;
                await kartVerisi.kaydetKos(vari, true);
                await fotoBicimlendirKos(vari, kartVerisi, kartVerisi.i_fotoKimlik);
                long sonuc = await kampanyayaCevir(vari, kartVerisi);
                return sonuc;
            }
        }

        public async Task<FirmaKampanyaTalebi> reddetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                long fk = 0;
                if (long.TryParse(fotoKonumu, out fk))
                    kartVerisi.i_fotoKimlik = fk;
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.i_kampanyaDurumuKimlik = (int)enumref_KampanyaDurumu.Reddedildi;
                await kartVerisi.kaydetKos(vari, true);
                await fotoBicimlendirKos(vari, kartVerisi, kartVerisi.i_fotoKimlik);
                return kartVerisi;
            }
        }



        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await FirmaKampanyaTalebi.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;

                if (kartVerisi.firmaKampanyaTalebikimlik > 0)
                {
                    if (kime._KullaniciTuru == enumref_KullaniciTuru.Yazilimci || kime._KullaniciTuru == enumref_KullaniciTuru.Sistem_Yoneticisi)
                    {
                        kart.e_gorulduMu = true;
                        await kartVerisi.kaydetKos(vari, false);
                    }
                }


                dokumVerisi = new List<FirmaKampanyaTalebiAYRINTI>();
                await baglilariCek(vari, kime);
                await fotoAyariBelirle(vari, kartVerisi._cizelgeAdi());


            }
        }

        public List<FirmaAYRINTI> _ayFirmaAYRINTI { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            if (kim._KullaniciTuru == enumref_KullaniciTuru.Firma)
            {
                _ayFirmaAYRINTI = await FirmaAYRINTI.ara(vari, p => p.firmakimlik == kim.i_firmaKimlik);
                return;
            }
            _ayFirmaAYRINTI = await FirmaAYRINTI.ara(vari);
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                FirmaKampanyaTalebiAYRINTIArama kosul = new FirmaKampanyaTalebiAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new FirmaKampanyaTalebi();
                await ekkosulEkle(vari, kime, kosul);
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
                    FirmaKampanyaTalebiAYRINTIArama kosul = JsonConvert.DeserializeObject<FirmaKampanyaTalebiAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new FirmaKampanyaTalebiAYRINTIArama();
                    await ekkosulEkle(vari, kime, kosul);
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new FirmaKampanyaTalebi();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
