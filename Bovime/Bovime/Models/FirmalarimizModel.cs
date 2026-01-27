using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class FirmalarimizModel : SiteModeli
    {
        public List<FirmaAYRINTI> firmalar { get; set; }

        public FirmalarimizModel()
        {
            firmalar = new List<FirmaAYRINTI>();
        }

        public async Task veriCek()
        {
            using (veri.Varlik vari = new Varlik())
            {
                firmalar = await vari.FirmaAYRINTIs.Where(p => p.i_firmaDurumuKimlik == (int)enumref_FirmaDurumu.Faal_Uye).OrderBy(p => p.sirasi).ToListAsync();
            }
        }
    }
}
