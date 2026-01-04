using Bovime.veri;

namespace Bovime.Models
{
    public class UyeFirmaModel : SiteModeli
    {
        public FirmaAYRINTI? firmasi { get; set; }
        public List<FirmaKampanyasiAYRINTI> kampanyalar { get; set; }
        public async Task veriCek(string url)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                this.firmasi = await FirmaAYRINTI.bul(vari, p => p.firmaUrl == url) ?? throw new Exception("Firma bilgisi bulunamadı");
                if (firmasi._Firmadurumu != enumref_FirmaDurumu.Faal_Uye)
                {
                    throw new Exception("Faal firma bilgisine erişilemedi");
                }
                kampanyalar = await FirmaKampanyasiAYRINTI.ara(p => p.i_firmaKimlik == firmasi.firmakimlik);
                var bedşr = kampanyalar;
            }
        }
    }
}
