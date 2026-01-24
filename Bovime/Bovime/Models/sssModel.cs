
using Bovime.veri;

namespace Bovime.Models
{
    public class sssModel
    {

        public List<SikSorulanSorularAYRINTI> sorular { get; set; }


        public sssModel()
        {
            sorular = new List<SikSorulanSorularAYRINTI>();
        }

        public async Task veriCek()
        {
            using (veri.Varlik vari = new Varlik())
            {
                sorular = await SikSorulanSorularAYRINTI.ara(vari);
                sorular = sorular.OrderBy(p => p.sirasi).ToList();
            }
        }

    }
}
