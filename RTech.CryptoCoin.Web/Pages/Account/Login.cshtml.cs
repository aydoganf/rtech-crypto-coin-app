using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RTech.CryptoCoin.Web.Model;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using RTech.CryptoCoin.Users;
using RTech.CryptoCoin.Data;

namespace RTech.CryptoCoin.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IUserRepository _userRepository;

    public LoginModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public void OnGet()
    {
    }


    [BindProperty]
    public string UserName { get; set; }
    [BindProperty, DataType(DataType.Password)]
    public string Password { get; set; }
    public string Message { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userRepository.FindAsync(UserName);
        if (user == null)
        {
            Message = "Invalid attempt";
            return Page();
        }

        var encryptedPassword = StringEncryptor.Encrypt($"{Password}{user.Salt}");
        if (encryptedPassword != user.Password)
        {
            Message = "Invalid attempt";
            return Page();
        }

        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim("id", user.Id.ToString())
                };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        user.AddLogin(Guid.NewGuid(), HttpContext.Connection.RemoteIpAddress?.ToString());
        await _userRepository.UpdateAsync(user);
        
        return RedirectToPage("/Index");
    }
}
