using Microsoft.EntityFrameworkCore;

public class AppDBContext : DbContext
{
    public DbSet<Kullanici> Kullanici { get; set; }
    public DbSet<Proje> Proje { get; set; }
    public DbSet<Basvuru> Basvuru { get; set; }
    public DbSet<Kategori> Kategori { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }
}