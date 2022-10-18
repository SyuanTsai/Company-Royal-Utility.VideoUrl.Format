using System.Threading.Tasks;
using VideoUrlFormat.Domain.Server;


#if NETCOREAPP3_1
#elif NET5_0
#elif NET6_0
using VideoUrlFormat.Interface;
using VideoUrlFormat.Model;
namespace VideoUrlFormat.Abstract.GameClubVideo;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public abstract class BaseVideo : IGenerateVideo
{
    protected BaseVideo()
    {
    }

    /// <summary>
    ///     產生單一輪局的視訊網址
    /// </summary>
    /// <param name="info"></param>
    /// <param name="server"></param>
    /// <returns></returns>
    public virtual async Task<Video> GenerateVideo(GameInfo info
                                                 , Server   server)
    {
        var result = new Video()
        {
            VideoOne   = CommonModeVideo(info.NoRun, info.NoActive, server.HistoryVideoUrl1)
          , VideoTwo   = CommonModeVideo(info.NoRun, info.NoActive, server.HistoryVideoUrl2)
          , VideoThree = CommonModeVideo(info.NoRun, info.NoActive, server.HistoryVideoUrl3),
        };

        return result;
    }

    /// <summary>
    ///     資料驗證
    /// </summary>
    /// <param name="noRun">
    ///     輪號-應該為中文日期 yymmdd0001
    /// </param>
    /// <param name="noActive">
    ///     局號-應該為4位數中文
    /// </param>
    /// <param name="urlData"></param>
    /// <returns></returns>
    public virtual bool Validation(string noRun, string noActive, string urlData)
    {
        if (string.IsNullOrWhiteSpace(urlData))
        {
            return false;
        }

        if (noRun.Length < 6)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(noActive))
        {
            return false;
        }

        return true;
    }


    /// <summary>
    ///     基本模板Url生產 
    /// </summary>
    /// <param name="noRun"></param>
    /// <param name="noActive"></param>
    /// <param name="urlData"></param>
    /// <returns></returns>
    private static string CommonModeVideo(string  noRun
                                        , string  noActive
                                        , string? urlData)
    {
        // 產生結果=> URL/日期/局號 
        return !string.IsNullOrWhiteSpace(urlData) ? $"{urlData}{noRun.Substring(2, 4)}/{noActive}.mp4" : string.Empty;
    }
}