using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class UyeGirisiModel : SiteModeli
    {
        public async Task<bool> girisBasarilimi(string kullaniciAdi, string sifre)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                UyeAYRINTI? _kisi = await vari.UyeAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciAdi == kullaniciAdi);
                if (_kisi == null)
                    throw new Exception("Yanlış kullanıcı adı");

                if (_kisi.sifre != sifre)
                    throw new Exception("Yanlış şifre");

                return true;
            }
            return false;
        }
    }
}
