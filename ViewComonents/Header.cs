using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyProje.Models;

public class Header : ViewComponent
{
    private readonly AppDBContext _context;

    public Header(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claim = HttpContext.User;
        string? strKullaniciId = claim?.FindFirstValue("kullaniciId");
        int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));
        Kullanici? entity = _context.Kullanici.Where(x => x.KullaniciId == intKullaniciId).FirstOrDefault();
        VMKullanici model = new VMKullanici
        {
            entity_Kullanici = entity
        };
        return View(model);
    }
}
