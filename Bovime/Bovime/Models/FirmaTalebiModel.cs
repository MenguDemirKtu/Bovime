namespace Bovime.Models
{
    public class FirmaTalebiModel : SiteModeli
    {
        public string? firmaAdi { get; set; }
        public string? tel { get; set; }
        public string? ePosta { get; set; }
        public string? metin { get; set; }

        // Bot koruma alanları
        public string? website { get; set; }   // honeypot
        public long formTime { get; set; }      // zaman kontrolü
    }
}
