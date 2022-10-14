using System.Text.Json.Serialization;


#if NET5_0
#elif NET6_0
using VideoUrlFormat.Enums;
namespace VideoUrlFormat.Model.Xg;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
/// <summary>
///     Xg視訊請求
/// </summary>
public class XgVideoRequest 
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
    ///     局號 - 內部使用
    /// </summary>
    [JsonIgnore]
    public string NoActive { get; set; } = null!;

    /// <summary>
    ///     遊戲編號
    /// </summary>
    [JsonIgnore]
    public string GameType { get; set; } = null!;
}
