using System;

#if NET5_0
#elif NET6_0
namespace VideoUrlFormat.Utility;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public static class EnumHelper
{
    public static TEnum GetEnum<TEnum>(int value)
    {
        var data = Enum.GetName(typeof(TEnum), value: value);

        if (data != null)
        {
            var result = (TEnum) Enum.Parse(typeof(TEnum), data);

            return result;
        }

        throw new NullReferenceException($"{value} 查詢無對應結果。");
    }

    public static string GetName<TEnum>(int value)
    {
        var result = Enum.GetName(typeof(TEnum), value: value);

        if (result != null)
        {
            return result;
        }

        throw new NullReferenceException($"{value} 查詢無對應結果。");
    }
}