using System;
using System.Threading.Tasks;
using VideoUrlFormat.Domain.Server;

#if NETCOREAPP3_1
#elif NET5_0 
#elif NET6_0
using VideoUrlFormat.Model;
namespace VideoUrlFormat.Abstract.GameClubVideo;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public class GcRgRacingVideo : BaseVideo
{
    /// <summary>
    ///     產生單一輪局的視訊網址
    /// </summary>
    /// <param name="info"></param>
    /// <param name="server"></param>
    /// <returns></returns>
    public override async Task<Video> GenerateVideo(GameInfo info
                                                  , Server   server)
    {
        var result = new Video()
        {
            VideoOne   = CustomVideoUrl(info.NoRun, info.NoActive, server.HistoryVideoUrl1, info.Time)
          , VideoTwo   = CustomVideoUrl(info.NoRun, info.NoActive, server.HistoryVideoUrl2, info.Time)
          , VideoThree = CustomVideoUrl(info.NoRun, info.NoActive, server.HistoryVideoUrl3, info.Time),
        };

        return result;
    }

    /// <summary>
    ///     產生客製化的視訊網址
    /// </summary>
    /// <param name="noRun"></param>
    /// <param name="noActive"></param>
    /// <param name="urlData"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private static string CustomVideoUrl(string    noRun
                                       , string    noActive
                                       , string?   urlData
                                       , DateTime? time)
    {
        
        if (time is null)
        {
            Console.WriteLine("視訊套件：時間為空值。");
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(urlData))
        {
            return string.Empty;
        }

        var date = time.Value.ToString("yyyyMMdd");

        //  取得局號後三碼
        var active = noActive.Substring(1, 3);

        //  取得URL+輪號/日期+局號後三碼
        return $"{urlData}{noRun.Substring(2, 4)}/{date}{active}.mp4";

    }
}