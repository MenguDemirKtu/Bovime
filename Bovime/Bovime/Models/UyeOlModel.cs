using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class UyeOlModel
    {
        public SozlesmeAYRINTI uyeOlSozlesmesi { get; set; }
        public UyeOlModel()
        {
            this.uyeOlSozlesmesi = new SozlesmeAYRINTI();
        }
        public async Task hazirla()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                SozlesmeAYRINTI? sozlesme = await vari.SozlesmeAYRINTIs.FirstOrDefaultAsync(p => p.i_sozlesmeTuruKimlik == (int)enumref_SozlesmeTuru.Uyelik_Sozlesmesi);
                if (sozlesme != null)
                    this.uyeOlSozlesmesi = sozlesme;
            }
        }
    }
}
