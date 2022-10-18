using System;

#if NETCOREAPP3_1
#elif NET5_0 
#elif NET6_0
namespace VideoUrlFormat.Model.Xg;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
/// <summary>
///     Xg 視訊回應
/// </summary>
public class XgVideoResponse
{
    /// <summary>
    ///     回傳的資料
    /// </summary>
    public Data Data { get; set; }
}

/// <summary>
///     詳細資料
/// </summary>
public class Data
{
    /// <summary>
    ///     Server ID
    /// </summary>
    public string? ServerId { get; set; }

    /// <summary>
    ///     輪局號
    /// </summary>
    public string? NoRun { get; set; }

    /// <summary>
    ///     Xg輪局號
    /// </summary>
    public string NoRunXg { get; set; } = null!;

    /// <summary>
    ///     輪局起始時間
    /// </summary>
    public DateTime Datetime { get; set; }
}