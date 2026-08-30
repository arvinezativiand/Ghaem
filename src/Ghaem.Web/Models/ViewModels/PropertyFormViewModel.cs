using Ghaem.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Ghaem.Web.Models.ViewModels
{
    public class PropertyFormViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان الزامی است")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات الزامی است")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "متراژ الزامی است")]
        public decimal Area { get; set; }

        public int BedroomCount { get; set; }
        public bool HasParking { get; set; }
        public bool HasStorage { get; set; }
        public int Floor { get; set; }
        public int? Unit { get; set; }
        public int TotalFloors { get; set; }

        public decimal? SalePrice { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? MonthlyRent { get; set; }

        public bool HasElevator { get; set; }
        public int ConstructionYear { get; set; }

        public PropertyType PropertyType { get; set; }
        public TransactionType TransactionType { get; set; }
        
        [Required(ErrorMessage = "آدرس الزامی است")]
        public string Address { get; set; } = string.Empty;

        public string? Slug { get; set; }

        public PropertyStatus Status { get; set; } = PropertyStatus.Available;
        
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TransactionType == TransactionType.Sale && !SalePrice.HasValue)
            {
                yield return new ValidationResult("برای فروش، قیمت فروش الزامی است.", new[] { nameof(SalePrice) });
            }

            if (TransactionType == TransactionType.Rent)
            {
                if (!Deposit.HasValue)
                {
                    yield return new ValidationResult("برای اجاره، مبلغ رهن (ودیعه) الزامی است.", new[] { nameof(Deposit) });
                }
                // MonthlyRent is optional — empty means رهن کامل
            }
        }
    }
}
