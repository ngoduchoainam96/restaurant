using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Album.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using XTLASPNET;

namespace _NET_project0.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        

        public RegisterConfirmationModel(UserManager<AppUser> userManager, IEmailSender sender)
        {
            _userManager = userManager;
           
        }

        public string Email { get; set; }

        public string UrlContinue {set; get;}

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }
            if (user.EmailConfirmed) {
                // Tài khoản đã xác thực email
              return ViewComponent(MessagePage.COMPONENTNAME,
                        new MessagePage.Message() {
                            title = "Thông báo",
                            htmlcontent = "Tài khoản đã xác thực, chờ chuyển hướng",
                            urlredirect = (returnUrl != null) ? returnUrl : Url.Page("/Index")
                        }

                );    
            }
            Email = email;
            if (returnUrl != null) {
                UrlContinue  =  Url.Page("RegisterConfirmation", new { email = Email, returnUrl = returnUrl });
            }
            else 
                UrlContinue  =  Url.Page("RegisterConfirmation", new { email = Email });

            // Once you add a real email sender, you should remove this code that lets you confirm the account
            return Page();
        }
    }
}
