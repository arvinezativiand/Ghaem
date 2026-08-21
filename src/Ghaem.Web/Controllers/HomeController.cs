using Ghaem.Web.Data;
using Ghaem.Web.Models.Entities;
using Ghaem.Web.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ghaem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var latestProperties = await _context.Properties
                .Include(p => p.Images)
                .Where(p => p.Status != PropertyStatus.Hidden)
                .OrderByDescending(p => p.CreatedAt)
                .Take(6)
                .ToListAsync();

            return View(latestProperties);
        }

        public async Task<IActionResult> Properties(TransactionType? type, PropertyType? propType, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Properties
                .Include(p => p.Images)
                .Where(p => p.Status != PropertyStatus.Hidden)
                .AsQueryable();

            if (type.HasValue)
                query = query.Where(p => p.TransactionType == type.Value);
            
            if (propType.HasValue)
                query = query.Where(p => p.PropertyType == propType.Value);

            if (minPrice.HasValue)
                query = query.Where(p => (p.TransactionType == TransactionType.Sale && p.SalePrice >= minPrice) || (p.TransactionType == TransactionType.Rent && p.Deposit >= minPrice));

            if (maxPrice.HasValue)
                query = query.Where(p => (p.TransactionType == TransactionType.Sale && p.SalePrice <= maxPrice) || (p.TransactionType == TransactionType.Rent && p.Deposit <= maxPrice));

            var properties = await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
            
            ViewBag.CurrentType = type;
            ViewBag.CurrentPropType = propType;
            return View(properties);
        }

        [Route("properties/{slug}")]
        public async Task<IActionResult> PropertyDetails(string slug)
        {
            var property = await _context.Properties
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Slug == slug && p.Status != PropertyStatus.Hidden);

            if (property == null) return NotFound();

            return View(property);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
