using Bovime.veri;
using Microsoft.EntityFrameworkCore;
using QRCoder;

namespace Bovime.Models
{
    public class UyeFirmaModel : SiteModeli
    {
        public FirmaAYRINTI? firmasi { get; set; }
        public List<FirmaKampanyasiAYRINTI> kampanyalar { get; set; }

        public List<SektorAYRINTI> sektorleri { get; set; }

        public FirmaKampanyasiAYRINTI kampanyasi { get; set; }

        public SatisAYRINTI? satisi { get; set; }

        private bool _hataVarmi { get; set; }
        private string _hataAciklamasi { get; set; }
        public bool hataVarmi
        {
            get
            {
                return _hataVarmi;
            }
        }
        public string hataAciklamasi
        {
            get
            {
                return _hataAciklamasi;
            }
        }
        public byte[] qrBytes { get; set; }
        private void kodOlustur(string veri)
        {
            var generator = new QRCodeGenerator();
            var qrData = generator.CreateQrCode(veri, QRCodeGenerator.ECCLevel.Q);
            var pngQr = new PngByteQRCode(qrData);
            qrBytes = pngQr.GetGraphic(20);
        }
        public async Task bilgiCek(Uye? kim, string kod)
        {
            try
            {

                _hataVarmi = false;
                using (veri.Varlik vari = new Varlik())
                {
                    kampanyasi = await FirmaKampanyasiAYRINTI.bul(vari, p => p.kampanyaKodu == kod) ?? throw new Exception("Kampanya bulunamadı");
                    enumref_KampanyaDurumu durum = (enumref_KampanyaDurumu)kampanyasi.i_kampanyaDurumuKimlik;

                    if (durum == enumref_KampanyaDurumu.Suresi_Doldu)
                    {
                        _hataVarmi = true;
                        _hataAciklamasi = "Kampanya süresi dolmuş";
                        return;
                    }
                    if (durum != enumref_KampanyaDurumu.Yayinda)
                    {
                        _hataVarmi = true;
                        _hataAciklamasi = "Kampanya yayında değil";
                        return;
                    }
                    if (kim == null)
                    {
                        _hataVarmi = true;
                        _hataAciklamasi = @"Üye girişi yapılmamış. Lütfen oturum açınız. Üye giriçi için <a href=""/UyeGirisi"" > TIKLAYINIZ </a>. <br> Üye olmak için <a href=""/UyeOl"">TIKLAYINIZ</a>  ";
                        satisi = null;
                        return;
                    }
                    else
                    {
                        await SqlIslemi.satisKoduBelirle(vari);
                        satisi = await SatisAYRINTI.bul(vari, p => p.i_uyeKimlik == kim.uyekimlik, p => p.y_firmaKampanyasiKimlik == kampanyasi.firmaKampanyasikimlik);
                        if (satisi == null)
                        {
                            _hataVarmi = true;
                            _hataAciklamasi = "Satış bilgisi bulunamadı";
                            return;
                        }
                        kodOlustur(satisi.kodu);

                    }
                }
            }
            catch (Exception ex)
            {
                _hataVarmi = true;
                _hataAciklamasi = ex.Message;
            }
        }
        public UyeFirmaModel()
        {
            firmasi = new FirmaAYRINTI();
            sektorleri = new List<SektorAYRINTI>();
            kampanyalar = new List<FirmaKampanyasiAYRINTI>();
        }
        public async Task veriCek(string url, Uye? uyesi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                this.firmasi = await FirmaAYRINTI.bul(vari, p => p.firmaUrl == url) ?? throw new Exception("Firma bilgisi bulunamadı");
                if (firmasi._Firmadurumu != enumref_FirmaDurumu.Faal_Uye)
                {
                    throw new Exception("Faal firma bilgisine erişilemedi");
                }
                kampanyalar = await FirmaKampanyasiAYRINTI.ara(p => p.i_firmaKimlik == firmasi.firmakimlik);
                List<FirmaSektoruAYRINTI> baglari = await FirmaSektoruAYRINTI.ara(vari, p => p.i_firmaKimlik == firmasi.firmakimlik);
                List<int> sektorKimlikler = baglari.Select(p => p.i_sektorKimlik).ToList();
                sektorleri = await vari.SektorAYRINTIs.Where(o => sektorKimlikler.Contains(o.sektorkimlik)).OrderBy(p => p.sektorAdi).ToListAsync();

                if (uyesi != null)
                {
                    List<SatisAYRINTI> satislar = await SatisAYRINTI.ara(vari, p => p.i_firmaKimlik == firmasi.firmakimlik
                    , p => p.i_uyeKimlik == uyesi.uyekimlik);

                    bool eklendimi = false;
                    for (int i = 0; i < kampanyalar.Count; i++)
                    {
                        var karsilik = satislar.FirstOrDefault(p => p.y_firmaKampanyasiKimlik == kampanyalar[i].firmaKampanyasikimlik);
                        if (karsilik == null)
                        {
                            Satis yeni = new Satis();
                            yeni.i_firmaKimlik = firmasi.firmakimlik;
                            yeni.kodu = Guid.NewGuid().ToString();
                            yeni.varmi = true;
                            yeni.i_uyeKimlik = uyesi.uyekimlik;
                            yeni.y_firmaKampanyasiKimlik = kampanyalar[i].firmaKampanyasikimlik;
                            yeni.tarih = DateTime.Now;
                            yeni._Satisdurumu = enumref_SatisDurumu.Olustu;
                            eklendimi = true;
                            await vari.Satiss.AddAsync(yeni);
                        }
                    }

                    if (eklendimi)
                    {
                        await vari.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
