using System;
using System.IO;
using System.Threading.Tasks;
using VideoUrlFormat.Domain.Server;
using static System.Int16;

#if NET5_0
#elif NET6_0
using VideoUrlFormat.Enums;
using VideoUrlFormat.Interface;
using VideoUrlFormat.Model;
using VideoUrlFormat.Model.Xg;
using VideoUrlFormat.Utility;
namespace VideoUrlFormat.Abstract.GameClubVideo;
#else
#error This code block does not match csproj TargetFrameworks list
#endif


public class XgCommonVideo : BaseVideo
{
    private readonly IApiManageRepo api;

    /// <summary>
    ///     DI
    /// </summary>
    /// <param name="api"></param>
    public XgCommonVideo(IApiManageRepo api)
    {
        this.api = api;
    }

    /// <summary>
    ///     產生單一輪局的視訊網址
    /// </summary>
    /// <param name="info"></param>
    /// <param name="server"></param>
    /// <returns></returns>
    public override async Task<Video> GenerateVideo(GameInfo info
                                                  , Server   server)
    {
        var game = EnumHelper.GetName<VideoByGameTypeEnum>(server.GameNo);

        var param = new XgVideoRequest()
        {
            NoRun  = info.NoRun, NoActive = info.NoActive, ServerId = server.Id
          , GameType = game
        };

        var xgRun = await api.QueryXgVideo(param);

        var result = new Video()
        {
            VideoOne   = CustomVideoUrl(server.HistoryVideoUrl1, xgRun, info.NoActive)
          , VideoTwo   = CustomVideoUrl(server.HistoryVideoUrl2, xgRun, info.NoActive)
          , VideoThree = CustomVideoUrl(server.HistoryVideoUrl3, xgRun, info.NoActive),
        };

        return result;
    }


    /// <summary>
    ///     Xg視訊
    /// </summary>
    /// <param name="sourceUri"></param>
    /// <param name="xgNoRun"></param>
    /// <param name="noActive"></param>
    /// <returns></returns>
    private static string CustomVideoUrl(string? sourceUri
                                       , string  xgNoRun
                                       , string  noActive)
    {
        if (string.IsNullOrWhiteSpace(sourceUri))
        {
            return string.Empty;
        }

        var uri = new Uri(sourceUri);

        //  這好HardCode 但是無解，網址格式不一致。除非動底層
        var data = new Uri
            (uri.Scheme + Uri.SchemeDelimiter + uri.Host + uri.Segments[0] + uri.Segments[1] + uri.Segments[2]);

        var path = Path.Combine(xgNoRun, xgNoRun + "-" + Parse(noActive) + ".mp4");

        var result = new Uri(data, path);

        return result.ToString();
    }
}