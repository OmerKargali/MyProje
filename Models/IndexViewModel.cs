namespace MyProje
{
    public class IndexViewModel
    {
        public Kullanici? Kullanici { get; set; }
        public IEnumerable<Kullanici>? Kullanicilar { get; set; }

        public Proje? Proje { get; set; }
        public IEnumerable<Proje>? Projeler { get; set; }

        public Kategori? Kategori { get; set; }
        public IEnumerable<Kategori>? Kategoriler { get; set; }

        public Basvuru? Basvuru { get; set; }
        public IEnumerable<Basvuru>? Basvurular { get; set; }

        public List<cstBasvuru>? list_cstBasvuru { get; set; }

    }

}