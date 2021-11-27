using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace _NET_project2.Pages
{
    public class TableModel : PageModel
    {
         // Binding Email từ dữ liệu từ nguồn tới có tên Email, email, emaIL ...
        [BindProperty]
        public string Email { get; set; }

        // Binding cho UserId từ nguồn gửi đến, dữ liệu nguồn có tên username
        [BindProperty (Name = "username")]
        public string UserId { set; get; }

        // Binding ProductID - thiết lập BINDING ngay cả khi truy cập là HTTP GÉT
        [BindProperty(SupportsGet=true)]
        public int ProductID { set; get; }

        // Binding Color
        [BindProperty]
        public string Color { set; get; }
        private readonly ILogger<IndexModel> _logger;

        public TableModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
