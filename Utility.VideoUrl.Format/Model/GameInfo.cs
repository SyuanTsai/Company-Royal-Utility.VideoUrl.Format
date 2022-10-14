
using System;

#if NET5_0
#elif NET6_0
namespace VideoUrlFormat.Model;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public class GameInfo
{
    /// <summary>
    ///     伺服器ID
    /// </summary>
    public string ServerId { get; set; } = null!;

    /// <summary>
    ///     輪號
    /// </summary>
    public string NoRun { get; set; } = null!;

    /// <summary>
    ///     局號
    /// </summary>
    public string NoActive { get; set; } = null!;

    /// <summary>
    ///     輪局時間
    /// </summary>
    public DateTime Time  { get; set; }
}