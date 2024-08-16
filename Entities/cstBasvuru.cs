public class cstBasvuru
{
    public int BasvuruId { get; set; }
    public string? ProjeAdi { get; set; }
    public string? ProjeDetay { get; set; }
    public string? ProjeKisaBilgi { get; set; }
    public int KullaniciId { get; set; }
    public int ProjeId { get; set; }
    public string? Durum { get; set; } = "Beklemede";

}