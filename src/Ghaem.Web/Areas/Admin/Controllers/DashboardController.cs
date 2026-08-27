using Ghaem.Web.Data;
using Ghaem.Web.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ghaem.Web.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalProperties = await _context.Properties.CountAsync();
            var saleProperties = await _context.Properties.CountAsync(p => p.TransactionType == TransactionType.Sale);
            var rentProperties = await _context.Properties.CountAsync(p => p.TransactionType == TransactionType.Rent);
            
            var recentProperties = await _context.Properties
                .Include(p => p.Images)
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToListAsync();

            ViewBag.TotalProperties = totalProperties;
            ViewBag.SaleProperties = saleProperties;
            ViewBag.RentProperties = rentProperties;
            ViewBag.RecentProperties = recentProperties;

            return View();
        }
    }
}
