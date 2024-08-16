using System.ComponentModel.DataAnnotations;

public class Proje
{
    [Key]
    public int ProjeId { get; set; }
    public int Kategori { get; set; }
    public string? ProjeAdi { get; set; }
    public string? ProjeDetay { get; set; }
    public string? ProjeKisaBilgi { get; set; }
    public string? ProjeLink { get; set; }
    public string? ProjeFoto { get; set; }
    public ICollection<Basvuru>? list_basvurular { get; set; }
}