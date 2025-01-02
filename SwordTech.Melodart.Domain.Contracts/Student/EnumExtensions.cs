using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SwordTech.Melodart.Domain.Contracts.Student
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var memberInfo = enumType.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return enumValue.ToString(); // Display name yoksa enum ismini döndür
        }
    }
}
