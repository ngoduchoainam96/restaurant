using Microsoft.EntityFrameworkCore;
using razor09.efcore.Models;

namespace razor09.efcore.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options) : base(options)
        {
            // Phương thức khởi tạo này chứa options để kết nối đến MS SQL Server
            // Thực hiện điều này khi Inject trong dịch vụ hệ thống
        }
        public DbSet<Menu> Menu {set; get;}
    }
}