using System.Threading.Tasks;
using VideoUrlFormat.Domain.Server;


#if NET5_0
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