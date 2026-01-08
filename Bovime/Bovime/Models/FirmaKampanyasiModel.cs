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
   public class FirmaKampanyasiModel : ModelTabani 
   { 
       public FirmaKampanyasi kartVerisi { get; set; } 
       public List<FirmaKampanyasiAYRINTI> dokumVerisi { get; set; } 
       public FirmaKampanyasiAYRINTIArama aramaParametresi { get; set; }


public FirmaKampanyasiModel()
{
 this.kartVerisi = new FirmaKampanyasi(); 
 this.dokumVerisi = new  List<FirmaKampanyasiAYRINTI>(); 
 this.aramaParametresi = new FirmaKampanyasiAYRINTIArama();
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
   FirmaKampanyasi? silinecek = await FirmaKampanyasi.olusturKos(vari, kayitlar[i]); 
 if (silinecek == null)
    continue;
silinecek._sayfaAta(sayfasi); 
   await silinecek.silKos(vari); 
 }
 }
  Models.FirmaKampanyasiModel modeli = new Models.FirmaKampanyasiModel();
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




      public async Task<FirmaKampanyasi> kaydetKos(Sayfa sayfasi)
  {
    using(veri.Varlik vari = new veri.Varlik())
  {
    long fk = 0; 
    if (long.TryParse(fotoKonumu, out fk)) 
      kartVerisi.i_fotoKimlik = fk;
     kullanan = sayfasi.mevcutKullanici();
     kartVerisi._kontrolEt(sayfasi.dilKimlik, vari );
     kartVerisi.metin = Sayfa.dosyaKonumDuzelt(kartVerisi.metin, Genel.yazilimAyari);
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
    var kart = await FirmaKampanyasi.olusturKos(vari, kimlik); 
   if (kart != null) 
       kartVerisi = kart; 
    dokumVerisi = new List<FirmaKampanyasiAYRINTI>(); 
 await baglilariCek(vari, kime);
    await fotoAyariBelirle(vari, kartVerisi._cizelgeAdi());
 } 
 } 

 public List<FirmaAYRINTI> _ayFirmaAYRINTI { get; set; } public async Task baglilariCek(veri.Varlik vari, Yonetici kim) {   _ayFirmaAYRINTI = await FirmaAYRINTI.ara(vari);}

  public async Task veriCekKos(Yonetici kime) 
  { 
    this.kullanan = kime; 
 using (veri.Varlik vari = new Varlik()) 
 { 
     FirmaKampanyasiAYRINTIArama kosul = new FirmaKampanyasiAYRINTIArama(); 
     kosul.varmi = true; 
     kartVerisi = new FirmaKampanyasi();  
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
        FirmaKampanyasiAYRINTIArama kosul = JsonConvert.DeserializeObject<FirmaKampanyasiAYRINTIArama>(talep.talepAyrintisi?? "" )?? new FirmaKampanyasiAYRINTIArama  ();       
       dokumVerisi = await kosul.cek(vari);      
      kartVerisi = new FirmaKampanyasi();           
 await baglilariCek(vari, kime); 
      aramaParametresi = kosul;           
     }          
   }          
    }          

     } 
}
