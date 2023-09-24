using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RTech.CryptoCoin.Users;

namespace RTech.CryptoCoin.Web.Pages.Account;

public class IndexModel : PageModel
{
    private readonly IUserRepository _userRepository;
    public User RTechUser;

    public IndexModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value ?? Guid.Empty.ToString());

        RTechUser = await _userRepository.FindAsync(userId);

        if (RTechUser == null)
        {
            return RedirectToPage("/Account/Logout");
        }

        return Page();
    }
}
