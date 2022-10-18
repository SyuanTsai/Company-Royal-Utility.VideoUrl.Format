

using System;
using System.Collections.Generic;
using System.Linq;

#if NETCOREAPP3_1
#elif NET5_0 
#elif NET6_0
using VideoUrlFormat.Abstract.GameClubVideo;
using VideoUrlFormat.Enums;
using VideoUrlFormat.Interface;
namespace VideoUrlFormat.Factories;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
/// <summary>
///     Gc館視訊工廠
/// </summary>
public class GcVideoFactory
{
    private readonly IEnumerable<IGenerateVideo> videos;


    public GcVideoFactory(IEnumerable<IGenerateVideo> videos)
    {
        this.videos = videos;
    }

    /// <summary>
    ///     設定遊戲要使用的Class
    /// </summary>
    /// <param name="videoByGame"></param>
    /// <returns></returns>
    private static Type Init(VideoByGameTypeEnum videoByGame)
    {
        //  目前僅有Xg館有特殊規格
        //  完美目前是依照舊規格
        return videoByGame switch
               {
                   VideoByGameTypeEnum.RgRacing => typeof(GcRgRacingVideo)
                 , _                     => typeof(OldCommonVideo)
               };
    }

    /// <summary>
    ///     取出對應的Service
    /// </summary>
    /// <param name="videoByGameType"></param>
    /// <returns></returns>
    public IGenerateVideo GetService(VideoByGameTypeEnum videoByGameType)
    {
        var service = videos.SingleOrDefault(x => x.GetType() == Init(videoByGameType));

        return service!;
    }
}