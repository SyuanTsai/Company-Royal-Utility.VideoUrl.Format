
using System;

#if NETCOREAPP3_1
#elif NET5_0 
#elif NET6_0
namespace VideoUrlFormat.Model.Base;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public class VideoSetting
{
    /// <summary>
    ///     ServerConnection
    /// </summary>
    public string ServerConnection { get; set; } = string.Empty;

    /// <summary>
    ///     KsApi網址
    /// </summary>
    public Uri KsApi { get; set; }


}