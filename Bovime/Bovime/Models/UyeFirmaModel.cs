using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class UyeFirmaModel : SiteModeli
    {
        public FirmaAYRINTI? firmasi { get; set; }
        public List<FirmaKampanyasiAYRINTI> kampanyalar { get; set; }

        public List<SektorAYRINTI> sektorleri { get; set; }

        public UyeFirmaModel()
        {
            firmasi = new FirmaAYRINTI();
            sektorleri = new List<SektorAYRINTI>();
            kampanyalar = new List<FirmaKampanyasiAYRINTI>();
        }
        public async Task veriCek(string url, UyeAYRINTI? uyesi)
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
