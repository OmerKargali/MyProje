public class Basvuru
{
    public int BasvuruId { get; set; }
    public int KullaniciId { get; set; }
    public int ProjeId { get; set; }
    public Kullanici? FK_Kullanici { get; set; }
    public Proje? FK_Proje { get; set; }
    public string? Durum { get; set; } = "Beklemede";

}