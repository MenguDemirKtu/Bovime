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
   public class FirmaModel : ModelTabani 
   { 
       public Firma kartVerisi { get; set; } 
       public List<FirmaAYRINTI> dokumVerisi { get; set; } 
       public FirmaAYRINTIArama aramaParametresi { get; set; }


public FirmaModel()
{
 this.kartVerisi = new Firma(); 
 this.dokumVerisi = new  List<FirmaAYRINTI>(); 
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
 public async Task  yetkiKontrolu(Sayfa sayfasi)
  {
   enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
  if (kartVerisi._birincilAnahtar() > 0)
      yetkiTuru = enumref_YetkiTuru.Guncelleme;
   if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
       throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
  }




      public async Task<Firma> kaydetKos(Sayfa sayfasi)
  {
    using(veri.Varlik vari = new veri.Varlik())
  {
    long fk = 0; 
    if (long.TryParse(fotoKonumu, out fk)) 
      kartVerisi.i_fotoKimlik = fk;
     kullanan = sayfasi.mevcutKullanici();
     kartVerisi._kontrolEt(sayfasi.dilKimlik, vari );
     kartVerisi.aciklama = Sayfa.dosyaKonumDuzelt(kartVerisi.aciklama, Genel.yazilimAyari);
     kartVerisi._sayfaAta(sayfasi);
    await kartVerisi.kaydetKos(vari,true);
      await           fotoBicimlendirKos(vari, kartVerisi, kartVerisi.i_fotoKimlik);
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
 } 
 } 

 public List<ref_FirmaDurumu> _ayref_FirmaDurumu { get; set; } public async Task baglilariCek(veri.Varlik vari, Yonetici kim) {   _ayref_FirmaDurumu = await ref_FirmaDurumu.ara(vari);}

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
        FirmaAYRINTIArama kosul = JsonConvert.DeserializeObject<FirmaAYRINTIArama>(talep.talepAyrintisi?? "" )?? new FirmaAYRINTIArama  ();       
       dokumVerisi = await kosul.cek(vari);      
      kartVerisi = new Firma();           
 await baglilariCek(vari, kime); 
      aramaParametresi = kosul;           
     }          
   }          
    }          

     } 
}
