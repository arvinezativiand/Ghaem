using System.ComponentModel.DataAnnotations;

namespace Ghaem.Web.Models.Enums
{
    public enum PropertyStatus
    {
        [Display(Name = "موجود")]
        Available = 1,
        [Display(Name = "فروخته شده")]
        Sold = 2,
        [Display(Name = "اجاره داده شده")]
        Rented = 3,
        [Display(Name = "مخفی")]
        Hidden = 4
    }
}
