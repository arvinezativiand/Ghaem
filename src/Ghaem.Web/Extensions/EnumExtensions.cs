using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ghaem.Web.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var displayAttribute = memberInfo.First().GetCustomAttribute<DisplayAttribute>();
                return displayAttribute?.Name ?? enumValue.ToString();
            }
            return enumValue.ToString();
        }
    }
}
