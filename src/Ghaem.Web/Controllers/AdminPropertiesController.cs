using Ghaem.Web.Data;
using Ghaem.Web.Models.Entities;
using Ghaem.Web.Models.Enums;
using Ghaem.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Ghaem.Web.Controllers
{
    [Authorize]
    public class AdminPropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminPropertiesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var query = _context.Properties
                .Include(p => p.Images)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(p => 
                    p.Title.Contains(search) || 
                    (p.Address != null && p.Address.Contains(search)) || 
                    (p.Description != null && p.Description.Contains(search)));
                ViewBag.SearchTerm = search;
            }

            var properties = await query
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return View(properties);
        }

        public IActionResult Create()
        {
            return View(new PropertyFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyFormViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Slug))
            {
                model.Slug = GenerateSlug(model.Title);
            }
            // Always set to Available on Create
            model.Status = PropertyStatus.Available;

            if (ModelState.IsValid)
            {
                var existingSlug = await _context.Properties.AnyAsync(p => p.Slug == model.Slug);
                if (existingSlug)
                {
                    ModelState.AddModelError("Slug", "این اسلاگ قبلاً استفاده شده است.");
                    return View(model);
                }

                var property = new Property
                {
                    Title = model.Title,
                    Description = model.Description,
                    Area = model.Area,
                    BedroomCount = model.BedroomCount,
                    HasParking = model.HasParking,
                    HasStorage = model.HasStorage,
                    Floor = model.Floor,
                    Unit = model.Unit,
                    TotalFloors = model.TotalFloors,
                    SalePrice = model.SalePrice,
                    Deposit = model.Deposit,
                    MonthlyRent = model.MonthlyRent,
                    HasElevator = model.HasElevator,
                    ConstructionYear = model.ConstructionYear,
                    PropertyType = model.PropertyType,
                    TransactionType = model.TransactionType,
                    Address = model.Address,
                    Slug = model.Slug,
                    Status = model.Status, // using the forced Available status
                    CreatedAt = DateTime.UtcNow
                };

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();

                await HandleImageUploads(model.Images, property.Id);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var property = await _context.Properties
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null) return NotFound();

            var model = new PropertyFormViewModel
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Area = property.Area,
                BedroomCount = property.BedroomCount,
                HasParking = property.HasParking,
                HasStorage = property.HasStorage,
                Floor = property.Floor,
                Unit = property.Unit,
                TotalFloors = property.TotalFloors,
                SalePrice = property.SalePrice,
                Deposit = property.Deposit,
                MonthlyRent = property.MonthlyRent,
                HasElevator = property.HasElevator,
                ConstructionYear = property.ConstructionYear,
                PropertyType = property.PropertyType,
                TransactionType = property.TransactionType,
                Address = property.Address,
                Slug = property.Slug,
                Status = property.Status
            };

            ViewBag.ExistingImages = property.Images.OrderBy(i => i.DisplayOrder).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropertyFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingSlug = await _context.Properties.AnyAsync(p => p.Slug == model.Slug && p.Id != model.Id);
                if (existingSlug)
                {
                    ModelState.AddModelError("Slug", "این اسلاگ قبلاً استفاده شده است.");
                    ViewBag.ExistingImages = await _context.PropertyImages.Where(i => i.PropertyId == model.Id).OrderBy(i => i.DisplayOrder).ToListAsync();
                    return View(model);
                }

                var property = await _context.Properties.FindAsync(id);
                if (property == null) return NotFound();

                property.Title = model.Title;
                property.Description = model.Description;
                property.Area = model.Area;
                property.BedroomCount = model.BedroomCount;
                property.HasParking = model.HasParking;
                property.HasStorage = model.HasStorage;
                property.Floor = model.Floor;
                property.Unit = model.Unit;
                property.TotalFloors = model.TotalFloors;
                property.SalePrice = model.SalePrice;
                property.Deposit = model.Deposit;
                property.MonthlyRent = model.MonthlyRent;
                property.HasElevator = model.HasElevator;
                property.ConstructionYear = model.ConstructionYear;
                property.PropertyType = model.PropertyType;
                property.TransactionType = model.TransactionType;
                property.Address = model.Address;
                property.Slug = model.Slug;
                property.Status = model.Status;
                property.UpdatedAt = DateTime.UtcNow;

                _context.Update(property);
                await _context.SaveChangesAsync();

                if (model.Images != null && model.Images.Any())
                {
                    await HandleImageUploads(model.Images, property.Id);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ExistingImages = await _context.PropertyImages.Where(i => i.PropertyId == model.Id).OrderBy(i => i.DisplayOrder).ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.PropertyImages.FindAsync(id);
            if (image != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "properties");
                string filePath = Path.Combine(uploadFolder, image.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.PropertyImages.Remove(image);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var property = await _context.Properties.FirstOrDefaultAsync(m => m.Id == id);
            if (property == null) return NotFound();
            return View(property);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var property = await _context.Properties.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            if (property != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "properties");
                foreach (var img in property.Images)
                {
                    string filePath = Path.Combine(uploadFolder, img.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task HandleImageUploads(List<IFormFile> images, int propertyId)
        {
            if (images == null || !images.Any()) return;

            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "properties");
            Directory.CreateDirectory(uploadFolder);

            var existingImagesCount = await _context.PropertyImages.CountAsync(i => i.PropertyId == propertyId);
            int order = existingImagesCount + 1;

            foreach (var file in images)
            {
                if (file.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(ext)) continue;

                    string uniqueFileName = Guid.NewGuid().ToString() + ext;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var propertyImage = new PropertyImage
                    {
                        PropertyId = propertyId,
                        FileName = uniqueFileName,
                        IsCover = existingImagesCount == 0 && order == 1,
                        DisplayOrder = order++
                    };
                    _context.PropertyImages.Add(propertyImage);
                }
            }
            await _context.SaveChangesAsync();
        }
        private string GenerateSlug(string phrase)
        {
            if (string.IsNullOrEmpty(phrase)) return "";
            string str = phrase.ToLower().Trim();
            // Replace spaces with dashes
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", "-");
            // Remove invalid chars (allow english, persian, numbers, dashes)
            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\u0600-\u06FF-]", "");
            return str;
        }
    }
}
