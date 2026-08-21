using System.ComponentModel.DataAnnotations;

namespace Ghaem.Web.Models.Enums
{
    public enum TransactionType
    {
        [Display(Name = "فروش")]
        Sale = 1,
        [Display(Name = "رهن و اجاره")]
        Rent = 2
    }
}
