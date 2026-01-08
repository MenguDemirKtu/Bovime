namespace Bovime.veri
{
    public partial class KullaniciAYRINTI : Bilesen
    {

        public KullaniciAYRINTI()
        {
            _varSayilan();
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(kullaniciAdi, ".", dilKimlik);
            uyariVerString(kullaniciTuru, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return kullaniciAdi.ToString();
        }


        public static KullaniciAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static KullaniciAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                KullaniciAYRINTI sonuc = new KullaniciAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.KullaniciAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }



        /// <summary>
        /// Veri tabanından kayıtlı olan verisini çeker. Dolayısıyla yapılan bir değişikliği aktaran işlemi burada yeniden seçemeyiz.  
        /// </summary>
        /// <returns></returns>
        public Kullanici _verisi()
        {
            Kullanici sonuc = Kullanici.olustur(kullaniciKimlik);
            return sonuc;
        }

        protected KullaniciAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.KullaniciAYRINTICizelgesi.tekliCek(kullaniciKimlik, vari);
            }
        }



        #region ozluk


        public override string _cizelgeAdi()
        {
            return "KullaniciAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Kullanıcı AYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciKimlik";
        }


        public override long _birincilAnahtar()
        {
            return kullaniciKimlik;
        }


        #endregion


    }
}

