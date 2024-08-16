namespace MyProje.Models
{
    public class VMProje
    {

        public int ProjeId { get; set; }
        public int Kategori { get; set; }
        public string? ProjeAdi { get; set; }
        public string? ProjeDetay { get; set; }
        public string? ProjeKisaBilgi { get; set; }
        public string? ProjeLink { get; set; }
        public string? ProjeFoto { get; set; }

        public Proje? entity_Proje { get; set; }
        public List<Proje>? list_Proje { get; set; }

        public List<Proje>? list_cstBasvuru { get; set; }
    }

}