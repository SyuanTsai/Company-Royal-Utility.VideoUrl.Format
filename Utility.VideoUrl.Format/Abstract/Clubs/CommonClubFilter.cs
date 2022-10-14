using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using VideoUrlFormat.Context;
using VideoUrlFormat.Domain.Server;


#if NET5_0
#elif NET6_0
using VideoUrlFormat.Factories;
using VideoUrlFormat.Model;
namespace VideoUrlFormat.Abstract.Clubs;
#else
#error This code block does not match csproj TargetFrameworks list
#endif

/// <summary>
///     館別分類
/// </summary>
public class CommonClubFilter : BaseClubFilter
{
    private readonly CommonVideoFactory factory;

    /// <summary>
    ///     館別建構子
    /// </summary>
    /// <param name="db"></param>
    /// <param name="cache"></param>
    /// <param name="factory"></param>
    public CommonClubFilter( ServerContext      db
                           , IMemoryCache       cache
                          ,  CommonVideoFactory factory) : base(db, cache)
    {
        this.factory = factory;
        Console.WriteLine("Old CommonClub Filter");
    }

    /// <summary>
    ///     呼叫相對應的館別遊戲
    /// </summary>
    /// <returns></returns>
    public override async Task<Video> GenerateVideoByGame(GameInfo info
                                                        , Server   server)
    {
        var gameType = GetGameType(server.GameNo);
        var service  = factory.GetService(gameType);

        var result = await service.GenerateVideo(info, server);

        return result;
    }
}