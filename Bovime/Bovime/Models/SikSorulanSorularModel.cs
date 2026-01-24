using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using Newtonsoft.Json;
using System; 
using System.Threading; 
using Microsoft.EntityFrameworkCore; 
using System.Threading; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using Bovime.veri; 
using Bovime.veriTabani; 
  using Microsoft.EntityFrameworkCore;
namespace Bovime.Models 
{ 
   public class SikSorulanSorularModel : ModelTabani 
   { 
       public SikSorulanSorular kartVerisi { get; set; } 
       public List<SikSorulanSorularAYRINTI> dokumVerisi { get; set; } 
       public SikSorulanSorularAYRINTIArama aramaParametresi { get; set; }


public SikSorulanSorularModel()
{
 this.kartVerisi = new SikSorulanSorular(); 
 this.dokumVerisi = new  List<SikSorulanSorularAYRINTI>(); 
 this.aramaParametresi = new SikSorulanSorularAYRINTIArama();
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
 private async Task ekkosulEkle(veri.Varlik vari, Yonetici kime, SikSorulanSorularAYRINTIArama kosul)
  {
   }
  public async Task silKos(Sayfa sayfasi, string id, Yonetici silen) 
  {
using (veri.Varlik vari = new veri.Varlik())
 {
    List<string> kayitlar = id.Split(',').ToList();
 for (int i = 0; i < kayitlar.Count; i++)
 {
   SikSorulanSorular? silinecek = await SikSorulanSorular.olusturKos(vari, kayitlar[i]); 
 if (silinecek == null)
    continue;
silinecek._sayfaAta(sayfasi); 
   await silinecek.silKos(vari); 
 }
 }
  Models.SikSorulanSorularModel modeli = new Models.SikSorulanSorularModel();
   await modeli.veriCekKos(silen); 
 }
 public async Task  yetkiKontrolu(Sayfa sayfasi)
  {
   enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
  if (kartVerisi._birincilAnahtar() > 0)
      yetkiTuru = enumref_YetkiTuru.Guncelleme;
   if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
       throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
  }




      public async Task<SikSorulanSorular> kaydetKos(Sayfa sayfasi)
  {
    using(veri.Varlik vari = new veri.Varlik())
  {
     kullanan = sayfasi.mevcutKullanici();
     kartVerisi._kontrolEt(sayfasi.dilKimlik, vari );
     kartVerisi._sayfaAta(sayfasi);
    await kartVerisi.kaydetKos(vari,true);
     return kartVerisi;
  }
  }


public async Task veriCekKos(Yonetici kime, long kimlik) 
   { 
   this.kullanan = kime; 
   yenimiBelirle(kimlik); 
   using (veri.Varlik vari = new Varlik()) 
 { 
    var kart = await SikSorulanSorular.olusturKos(vari, kimlik); 
   if (kart != null) 
       kartVerisi = kart; 
    dokumVerisi = new List<SikSorulanSorularAYRINTI>(); 
 await baglilariCek(vari, kime);
 } 
 } 

 public async Task baglilariCek(veri.Varlik vari, Yonetici kim) {}

  public async Task veriCekKos(Yonetici kime) 
  { 
    this.kullanan = kime; 
 using (veri.Varlik vari = new Varlik()) 
 { 
     SikSorulanSorularAYRINTIArama kosul = new SikSorulanSorularAYRINTIArama(); 
     kosul.varmi = true; 
     kartVerisi = new SikSorulanSorular();  
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
        SikSorulanSorularAYRINTIArama kosul = JsonConvert.DeserializeObject<SikSorulanSorularAYRINTIArama>(talep.talepAyrintisi?? "" )?? new SikSorulanSorularAYRINTIArama  ();       
              await ekkosulEkle(vari, kime, kosul);
       dokumVerisi = await kosul.cek(vari);      
      kartVerisi = new SikSorulanSorular();           
 await baglilariCek(vari, kime); 
      aramaParametresi = kosul;           
     }          
   }          
    }          

     } 
}
