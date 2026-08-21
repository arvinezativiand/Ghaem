using Ghaem.Web.Models.Enums;

namespace Ghaem.Web.Models.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
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
        public string Address { get; set; } = string.Empty;
        
        public string Slug { get; set; } = string.Empty;
        public PropertyStatus Status { get; set; } = PropertyStatus.Available;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
    }
}
