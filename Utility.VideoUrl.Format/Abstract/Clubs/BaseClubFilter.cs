using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using VideoUrlFormat.Context;
using VideoUrlFormat.Domain.Server;


#if NETCOREAPP3_1
#elif NET5_0 
#elif NET6_0
using VideoUrlFormat.Enums;
using VideoUrlFormat.Interface;
using VideoUrlFormat.Model;
using VideoUrlFormat.Utility;
namespace VideoUrlFormat.Abstract.Clubs;
#else
#error This code block does not match csproj TargetFrameworks list
#endif

/// <summary>
///     館別分類
/// </summary>
public abstract class BaseClubFilter : IClubFactory
{
    private readonly ServerContext db;
    private readonly IMemoryCache  cache;
    private const    string        CacheKey = "ServerVideoCache";


    /// <summary>
    ///     館別建構子
    /// </summary>
    /// <param name="db"></param>
    /// <param name="cache"></param>
    protected BaseClubFilter( ServerContext db
                           , IMemoryCache  cache)
    {
         this.db    = db;
         this.cache = cache;
    }


    protected static VideoByGameTypeEnum GetGameType(int gameNo)
    {
        var result = EnumHelper.GetEnum<VideoByGameTypeEnum>(gameNo);

        return result;
    }

    public virtual Task<Video>? GenerateVideoByGame(GameInfo info
                                                  , Server   server)
    {
        throw new NotImplementedException();
    }
}