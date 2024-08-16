namespace MyProje.Models
{
    public class VMLogin()
    {
        public string? Adi { get; set; }
        public string? Sifre { get; set; }
    }
    public class VMCreate()
    {
        public int KullaniciId { get; set; }
        public string? Adi { get; set; }
        public string? Soyadi { get; set; }
        public string? KimlikNo { get; set; }
        public string? DogumGunu { get; set; }
        public string? TelefonNo { get; set; }
        public string? Sehir { get; set; }
        public string? Sifre { get; set; }
        public bool? AdminMi { get; set; }
        public int Yas { get; set; }
        public string? Email { get; set; }
        public string? Okul { get; set; }
        public string? Bolum { get; set; }
        public string? Hakkinda { get; set; }
        public string? ProfilFoto { get; set; }
        public string? Ozgecmis { get; set; }
    }

    // public class VMProjeCreate
    // {

    //     public int ProjeId { get; set; }
    //     public int Kategori { get; set; }
    //     public string? ProjeAdi { get; set; }
    //     public string? ProjeDetay { get; set; }
    //     public string? ProjeKisaBilgi { get; set; }
    //     public string? ProjeLink { get; set; }
    //     public string? ProjeFoto { get; set; }
    // }

}