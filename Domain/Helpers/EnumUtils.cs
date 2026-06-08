using System.ComponentModel;
using System.Reflection;

namespace Domain.Helpers;

public static class EnumUtils
{
    public static string GetDescription(this Enum value)
    {
        if (value == null) return string.Empty;

        // 列挙値のFieldInfを取得
        FieldInfo? field = value.GetType().GetField(value.ToString());
        if (field == null) return value.ToString();

        // DescriptionAttributeを取得
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        // 属性があればその説明文、なければ列挙値名を返す
        return attribute?.Description ?? value.ToString();
    }
}
