using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MyProje.Models;




public class AccessController : Controller
{
    public AppDBContext _context;

    public AccessController(AppDBContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity?.IsAuthenticated == true)
        {
            var adminMi = bool.Parse(claimUser.FindFirstValue("AdminMi"));
            if (adminMi)
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(VMLogin modellogin)
    {
        string struyari = "";
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        string? strBaglanti = configuration.GetConnectionString("WebApiDatabase");
        var opt = new DbContextOptionsBuilder<AppDBContext>();
        opt.UseNpgsql(strBaglanti);
        _context = new AppDBContext(opt.Options);

        if (!string.IsNullOrWhiteSpace(modellogin.Adi) && !string.IsNullOrWhiteSpace(modellogin.Sifre))
        {
            Kullanici? sorgu = _context.Kullanici.Where(x => x.Adi == modellogin.Adi && x.Sifre == modellogin.Sifre).FirstOrDefault();
            if (sorgu == null)
            {
                struyari = "Kullanıcı Bulunamadı";
            }
            else
            {
                string? strRole = "Kullanici";
                bool? blnAdminMi = sorgu.AdminMi;
                string strAdminMi = "false";
                string strUrl = "/Home/Index";
                if (blnAdminMi == true)
                {
                    strAdminMi = "true";
                    strUrl = "/Admin/Index";
                    strRole = "Admin";
                }

                List<Claim> claims = new List<Claim>(){
                    new Claim(ClaimTypes.NameIdentifier, modellogin.Adi),
                    new Claim(ClaimTypes.Name, modellogin.Adi),
                    new Claim("AdminMi", strAdminMi),
                    new Claim("KullaniciId",sorgu.KullaniciId.ToString()),
                    new Claim(ClaimTypes.Role,strRole)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);
                return Redirect(strUrl);
            }
        }
        else
        {
            struyari = "Adınızı ya da Şifrenizi Giriniz";
        }
        ViewData["ValidateMessage"] = struyari;
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(VMCreate modelcreate)
    {
        Kullanici dbdeneme = new Kullanici();
        if (modelcreate.Adi != null && modelcreate.Soyadi != null)
        {
            dbdeneme.Adi = modelcreate.Adi;
            dbdeneme.Soyadi = modelcreate.Soyadi;
            dbdeneme.KimlikNo = modelcreate.KimlikNo;
            dbdeneme.DogumGunu = modelcreate.DogumGunu;
            dbdeneme.TelefonNo = modelcreate.TelefonNo;
            dbdeneme.Sehir = modelcreate.Sehir;
            dbdeneme.Sifre = modelcreate.Sifre;
            dbdeneme.AdminMi = modelcreate.AdminMi;
            dbdeneme.Email = modelcreate.Email;
            dbdeneme.Yas = modelcreate.Yas;
            dbdeneme.Okul = modelcreate.Okul;
            dbdeneme.Bolum = modelcreate.Bolum;
            dbdeneme.Hakkinda = modelcreate.Hakkinda;
            dbdeneme.ProfilFoto = modelcreate.ProfilFoto;
            dbdeneme.Ozgecmis = modelcreate.Ozgecmis;

            if (ModelState.IsValid)
            {
                var existingUser = await _context.Kullanici.FirstOrDefaultAsync(u => u.KimlikNo == modelcreate.KimlikNo);
                if (existingUser != null)
                {
                    ModelState.AddModelError("KimlikNo", "Kimlik no zaten kayıtlı");
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            ModelState.AddModelError("", error.ErrorMessage);
                        }
                    }
                    return View(modelcreate);
                }
                _context.Kullanici.Add(dbdeneme);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
        return View(modelcreate);
    }
}


