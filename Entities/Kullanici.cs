using System.ComponentModel.DataAnnotations;
public class Kullanici
{
    public int KullaniciId { get; set; }

    [Required]
    public string? Adi { get; set; }
    public string? Sifre { get; set; }
    public string? Soyadi { get; set; }

    [Required]
    public string? KimlikNo { get; set; }
    public string? DogumGunu { get; set; }
    public string? TelefonNo { get; set; }
    public string? Sehir { get; set; }
    public bool? AdminMi { get; set; }
    public int Yas { get; set; }
    public string? Email { get; set; }
    public string? Okul { get; set; }
    public string? Bolum { get; set; }
    public string? Hakkinda { get; set; }
    public string? ProfilFoto { get; set; }
    public string? Ozgecmis { get; set; }
}