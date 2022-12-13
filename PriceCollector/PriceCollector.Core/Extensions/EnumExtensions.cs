using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace PriceCollector.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T e) where T : Enum, IConvertible
        {
            var type = e.GetType();

            foreach (int val in Enum.GetValues(type))
            {
                if (val != e.ToInt32(CultureInfo.InvariantCulture)) continue;

                var memInfo = type.GetMember(type.GetEnumName(val) ?? string.Empty);

                if (memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is
                    DescriptionAttribute descriptionAttribute)
                {
                    return descriptionAttribute.Description;
                }
            }

            return null;
        }
    }
}