using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProje;
using MyProje.Models;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public AppDBContext _context;

    public AdminController(AppDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var Proje = _context.Proje.ToList();
        return View(Proje);
    }

    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;
        if (claimUser.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Admin");
        return View();
    }

    public IActionResult YeniProje()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> YeniProje(VMProje Proje)
    {
        Proje b = new Proje();
        if (Proje.ProjeAdi != null && Proje.ProjeLink != null)
        {
            b.ProjeAdi = Proje.ProjeAdi;
            b.Kategori = Proje.Kategori;
            b.ProjeDetay = Proje.ProjeDetay;
            b.ProjeKisaBilgi = Proje.ProjeKisaBilgi;
            b.ProjeLink = Proje.ProjeLink;
            b.ProjeFoto = Proje.ProjeFoto;
            if (ModelState.IsValid)
            {
                var existingBasvuru = await _context.Proje.FirstOrDefaultAsync(b => b.ProjeLink == Proje.ProjeLink);
                if (existingBasvuru != null)
                {
                    ModelState.AddModelError("ProjeLink", "Aynı Başvuru Linki Başka Bir Başvuruda Mevcut");
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            ModelState.AddModelError("", error.ErrorMessage);
                        }
                    }
                    return View(Proje);
                }
                _context.Proje.Add(b);
                _context.SaveChanges();
                return RedirectToAction("YeniProje", "Admin");
            }
        }
        return View();
    }

    public IActionResult BasvuruYonet()
    {
        IndexViewModel model = new IndexViewModel();

        var projeler = (from A in _context.Proje
                        join B in _context.Basvuru on A.ProjeId equals B.ProjeId
                        select new cstBasvuru
                        {
                            BasvuruId = B.BasvuruId,
                            ProjeAdi = A.ProjeAdi,
                            Durum = B.Durum,
                            KullaniciId = B.KullaniciId // Eğer kullanıcının da listede görünmesini isterseniz
                        }).ToList();

        model.list_cstBasvuru = projeler;
        model.Basvurular = new List<Basvuru>();

        return View(model);

    }


    [HttpPost]
    public IActionResult DurumGuncelle(int BasvuruId, string durum)
    {
        var basvuru = _context.Basvuru.Find(BasvuruId);
        if (basvuru != null)
        {
            basvuru.Durum = durum;
            _context.SaveChanges();
        }

        return RedirectToAction("BasvuruYonet");
    }

    public IActionResult ProjeSil(int ProjeId)
    {
        var ProjeSil = _context.Proje.Find(ProjeId);
        _context.Proje.Remove(ProjeSil);
        _context.SaveChanges();
        return RedirectToAction("Index", "Admin");
    }

    public IActionResult ProjeGetir(int ProjeId)
    {
        var proje = _context.Proje.Find(ProjeId);
        return View("ProjeGetir");
    }
    public IActionResult ProjeGuncelle(Proje b)
    {
        var proje = _context.Proje.Find(b.ProjeId);
        proje.ProjeAdi = b.ProjeAdi;
        proje.Kategori = b.Kategori;
        proje.ProjeDetay = b.ProjeDetay;
        proje.ProjeKisaBilgi = b.ProjeKisaBilgi;
        proje.ProjeLink = b.ProjeLink;
        proje.ProjeFoto = b.ProjeFoto;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Admin");
    }
}

