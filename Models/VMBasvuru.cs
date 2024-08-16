namespace MyProje.Models
{
    public class Basvuru
    {
        public int BasvuruId { get; set; }
        public int KullaniciId { get; set; }
        public int ProjeId { get; set; }
        public Basvuru? entity_Basvuru { get; set; }
        public List<Basvuru>? list_Basvuru { get; set; }
        public string? Durum { get; set; } = "Beklemede";
    }
}