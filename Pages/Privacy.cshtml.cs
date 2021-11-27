using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace _NET_project2.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

         public IActionResult OnGet()
        {
            return ViewComponent("MessagePage", new XTLASPNET.MessagePage.Message {
                title = "Bạn đã đặt thành công, trang web sẽ tự động chuyển về Trang chủ trong chốc lát, hoặc bấm vào link bên dưới",
                htmlcontent = "XIN MỜI XEM LẠI PHẦN ĐẶT: <strong></strong>",
                secondwait = 5,
                urlredirect = "/"
            });
        }
    }
}
