using System.ComponentModel.DataAnnotations;

namespace Ghaem.Web.Models.Enums
{
    public enum PropertyType
    {
        [Display(Name = "آپارتمان")]
        Apartment = 1,
        [Display(Name = "خانه")]
        House = 2,
        [Display(Name = "ویلا")]
        Villa = 3,
        [Display(Name = "مغازه")]
        Shop = 4,
        [Display(Name = "دفتر")]
        Office = 5,
        [Display(Name = "زمین")]
        Land = 6,
        [Display(Name = "سایر")]
        Other = 7
    }
}
