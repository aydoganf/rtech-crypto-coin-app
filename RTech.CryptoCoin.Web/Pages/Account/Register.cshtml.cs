using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RTech.CryptoCoin.Users;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RTech.CryptoCoin.Web.Pages.Account;

public class RegisterModel : PageModel
{
    [BindProperty]
    public string UserName { get; set; }
    [BindProperty, DataType(DataType.Password)]
    public string Password { get; set; }

    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserManager _userManager;
    private readonly IUserRepository _userRepository;

    public RegisterModel(
        IHttpContextAccessor contextAccessor,
        IUserManager userManager,
        IUserRepository userRepository)
    {
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public void OnGet()
    {
        if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            _contextAccessor.HttpContext.Response.Redirect("/Index");
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var user = _userManager.Create(UserName, Password);


        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, UserName),
            new Claim("id", user.Id.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        user.AddLogin(Guid.NewGuid(), HttpContext.Connection.RemoteIpAddress?.ToString());

        await _userRepository.InsertAsync(user);
        return RedirectToPage("/Index");
    }
}
