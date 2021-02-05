using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using portfolio.data.Concrete.EfCore;
using portfolio.webui.Models;

namespace portfolio.webui.Controllers
{
    public class AccountController:Controller
    {
       PortfolioContext c = new PortfolioContext();
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminModel model)
        {           
            var login = c.Admins.FirstOrDefault(x=>x.UserName==model.UserName && x.Password == model.Password);
            if(login!=null){
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,model.UserName)
                };
                var userIdentity = new ClaimsIdentity(claims,"Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("aboutlist","admin");
            }
            return View(model);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }
}