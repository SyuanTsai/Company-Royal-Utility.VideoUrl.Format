
#if NETCOREAPP3_1
#elif NET5_0 
#elif NET6_0
namespace VideoUrlFormat.Model;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
/// <summary>
///     視訊網址
/// </summary>
public class Video
{
    /// <summary>
    ///     視訊桌面
    /// </summary>
    public string VideoOne { get; set; } = "";

    /// <summary>
    ///     視訊牌面
    /// </summary>
    public string VideoTwo { get; set; } = "";

    /// <summary>
    ///     備用
    /// </summary>
    public string VideoThree { get; set; } = "";
}