namespace Keysme.Services.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    using Keysme.Data.Models;

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null)
            {
                return "";
            }

            var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>();
            if (member != null)
            {
                return member.GetName();
            }

            return enumValue.ToString();
        }

        public static string GetLineIcon(this Enum enumValue)
        {
            if (enumValue == null)
            {
                return "";
            }

            var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault()?.GetCustomAttribute<LineIconAttribute>();
            if (member != null)
            {
                return member.Icon;
            }

            return enumValue.ToString();
        }
    }
}
