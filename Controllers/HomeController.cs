using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyProje.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;



namespace MyProje.Controllers;


public class HomeController : Controller
{
    public AppDBContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(AppDBContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [Authorize(Roles = "Kullanici")]
    public IActionResult Index()
    {
        System.Security.Claims.ClaimsPrincipal claim = this.User;
        string? strKullaniciId = claim.FindFirstValue("KullaniciId");
        int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));
        Kullanici? entity = _context.Kullanici.Where(x => x.KullaniciId == intKullaniciId).FirstOrDefault();
        VMKullanici model = new VMKullanici();
        model.entity_Kullanici = entity;
        return View(model);
    }
    [Authorize(Roles = "Kullanici")]
    public IActionResult Contact()
    {
        return View();
    }
    [Authorize(Roles = "Kullanici")]
    public IActionResult About()
    {
        System.Security.Claims.ClaimsPrincipal claim = this.User;
        string? strKullaniciId = claim.FindFirstValue("KullaniciId");
        int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));
        Kullanici? entity = _context.Kullanici.Where(x => x.KullaniciId == intKullaniciId).FirstOrDefault();
        VMKullanici model = new VMKullanici();
        model.entity_Kullanici = entity;
        return View(model);
    }
    [Authorize(Roles = "Kullanici")]
    public IActionResult Portfolio()
    {
        var liste = _context.Proje.ToList();
        VMProje model = new VMProje();
        model.list_Proje = liste;
        return View(model);
    }
    [Authorize(Roles = "Kullanici")]
    public IActionResult PortfolioDetay(int id)
    {
        var entity = _context.Proje.Where(x => x.ProjeId == id).FirstOrDefault();
        VMProje model = new VMProje();
        model.entity_Proje = entity;
        return View(model);
    }

    public async Task<IActionResult> BasvuruKaydet(int ProjeId)
    {
        int kullaniciId = int.Parse(User.FindFirstValue("KullaniciId"));

        // var mevcutBasvuru = await _context.Basvuru.FirstOrDefaultAsync(x => x.KullaniciId == input.KullaniciId && x.ProjeId == input.ProjeId);

        // if (mevcutBasvuru != null)
        // {
        //     ModelState.AddModelError("KullaniciId", "Zaten bu projeye başvurdunuz.");
        //     return RedirectToAction("PortfolioDetay");
        // }

        var yeniProje = new Basvuru
        {
            ProjeId = ProjeId,
            KullaniciId = kullaniciId
        };

        await _context.Basvuru.AddAsync(yeniProje);
        await _context.SaveChangesAsync();

        return RedirectToAction("Services");
    }
    [Authorize(Roles = "Kullanici")]
    public IActionResult Services()
    {
        IndexViewModel model = new IndexViewModel();
        System.Security.Claims.ClaimsPrincipal claim = this.User;
        string? strKullaniciId = claim.FindFirstValue("KullaniciId");
        int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));
        Kullanici? kullanici = _context.Kullanici.Where(x => x.KullaniciId == intKullaniciId).FirstOrDefault();
        if (kullanici != null)
        {
            int kullaniciId = kullanici.KullaniciId;
            var projeler = (from A in _context.Proje
                            join B in _context.Basvuru on A.ProjeId equals B.ProjeId
                            where B.KullaniciId == intKullaniciId
                            select new cstBasvuru
                            {
                                BasvuruId = B.BasvuruId,
                                ProjeAdi = A.ProjeAdi,
                                Durum = B.Durum


                            }).ToList();

            model.list_cstBasvuru = projeler;
            model.Basvurular = new List<Basvuru>();
        }
        return View(model);
    }
    [Authorize(Roles = "Kullanici")]
    public IActionResult Resume()
    {
        System.Security.Claims.ClaimsPrincipal claim = this.User;
        string? strKullaniciId = claim.FindFirstValue("KullaniciId");
        int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));
        Kullanici? entity = _context.Kullanici.Where(x => x.KullaniciId == intKullaniciId).FirstOrDefault();
        VMKullanici model = new VMKullanici();
        model.entity_Kullanici = entity;
        return View(model);
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Access");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
