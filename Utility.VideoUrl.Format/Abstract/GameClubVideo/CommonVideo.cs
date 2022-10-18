using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using VideoUrlFormat.Domain.Server;

#if NETCOREAPP3_1
#elif NET5_0
#elif NET6_0
using VideoUrlFormat.Model;
namespace VideoUrlFormat.Abstract.GameClubVideo;
#else
#error This code block does not match csproj TargetFrameworks list
#endif

public class CommonVideo : BaseVideo
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
            VideoOne   = CustomVideoUrl(info.NoRun, info.NoActive, server.HistoryVideoUrl1)
          , VideoTwo   = CustomVideoUrl(info.NoRun, info.NoActive, server.HistoryVideoUrl2)
          , VideoThree = CustomVideoUrl(info.NoRun, info.NoActive, server.HistoryVideoUrl3),
        };

        return result;
    }

    /// <summary>
    ///     產生客製化的視訊網址
    /// </summary>
    /// <param name="noRun">
    ///     輪號-應該為中文日期 yymmdd0001
    /// </param>
    /// <param name="noActive">
    ///     局號-應該為4位數中文
    /// </param>
    /// <param name="urlData"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private string CustomVideoUrl(string  noRun
                                       , string  noActive
                                       , string? urlData)
    {

        if (!Validation(noRun, noActive, urlData))
        {
            return string.Empty;
        }
        var date = noRun.Substring(2,4);

        //  20221018與雷哥確認，IT切檔時間依據「輪號」的日期。
        //  故如果發生該輪局玩到1點，視訊應該也是根據輪號的日期儲存。        
        return $"{urlData}{date}/{noRun}{noActive}.mp4";
    }
}