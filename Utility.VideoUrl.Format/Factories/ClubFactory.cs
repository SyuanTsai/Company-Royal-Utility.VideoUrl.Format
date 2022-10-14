using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using VideoUrlFormat.Context;
using VideoUrlFormat.Domain.Server;

#if NET5_0
#elif NET6_0
using VideoUrlFormat.Abstract.Clubs;
using VideoUrlFormat.Enums;
using VideoUrlFormat.Interface;
using VideoUrlFormat.Utility;
namespace VideoUrlFormat.Factories;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public class ClubFactory
{
    private const           string                    CacheKey        = "ServerVideoCache";
    private static readonly string                    VideoSwitchData = "VideoSwitchData";
    private readonly        ServerContext             db;
    private readonly        IMemoryCache              cache;
    private readonly        IEnumerable<IClubFactory> service;

    /// <summary>
    ///     DI
    /// </summary>
    /// <param name="service"></param>
    /// <param name="db"></param>
    /// <param name="cache"></param>
    public ClubFactory(IEnumerable<IClubFactory> service
                     , ServerContext             db
                     , IMemoryCache              cache)
    {
        this.service = service;
        this.db      = db;
        this.cache   = cache;
        Console.WriteLine("Club Factory");
    }

    private static Type Init(ClubEnum club)
    {
        var sDate = Environment.GetEnvironmentVariable(VideoSwitchData);

        //  沒環境參數 - 依照舊版執行
        if (string.IsNullOrEmpty(sDate))
        {
            Console.WriteLine("CommonClub Filter1");
            //  目前僅有Xg館有特殊規格
            //  完美目前是依照舊規格
            return club switch
                   {
                       ClubEnum.XG    => typeof(XgClubFilter)
                     , ClubEnum.GClub => typeof(GcClubFilter)
                     , ClubEnum.WM    => typeof(WmClubFilter)
                     , _              => typeof(OldCommonClubFilter)
                   };
        }

        //  僅有日期在設定之後的才用新的視訊
        var date = DateTime.ParseExact(sDate, "yyyyMMdd", CultureInfo.InvariantCulture);
        if (DateTime.Now.Date >= date)
        {
            Console.WriteLine("CommonClub Filter2");
            return club switch
                   {
                       ClubEnum.XG => typeof(XgClubFilter)
                     , _           => typeof(CommonClubFilter)
                   };
        }

        Console.WriteLine("CommonClub Filter3");
        //  目前僅有Xg館有特殊規格
        //  完美目前是依照舊規格
        return club switch
               {
                   ClubEnum.XG    => typeof(XgClubFilter)
                 , ClubEnum.GClub => typeof(GcClubFilter)
                 , ClubEnum.WM    => typeof(WmClubFilter)
                 , _              => typeof(OldCommonClubFilter)
               };
    }

    /// <summary>
    ///     取出對應的Service
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="server"></param>
    /// <returns></returns>
    public IClubFactory GetService(string     serverId
                                 , out Server server)
    {
        server = GetServerInfo(serverId)
            .Result;

        var clubType = GetClubType(server.LobbyNo);

        var club = service.SingleOrDefault(x => x.GetType() == Init(clubType));

        return club!;
    }

    /// <summary>
    ///     查詢伺服器設定
    /// </summary>
    /// <returns></returns>
    private async Task<List<Server>> CacheAllServerSetting()
    {
        var data = await db.Servers.ToListAsync();

        return data ?? throw new NullReferenceException("資料庫查詢Server無任何資料。");
    }

    /// <summary>
    ///     查詢伺服器設定
    /// </summary>
    /// <returns></returns>
    private async Task<Server> GetServerInfo(string serverId)
    {
        var data = await CacheServer();

        var result = data.FirstOrDefault(s => s.Id == serverId);

        return result ?? throw new NullReferenceException($"{serverId} 查詢無對應結果。");
    }

    /// <summary>
    ///     將資料快取並保存20分鐘。
    /// </summary>
    /// <param name="cacheMin"></param>
    /// <returns></returns>
    private async Task<List<Server>> CacheServer(int cacheMin = 10)
    {
        if (cache.TryGetValue(CacheKey, out List<Server> result))
        {
            return result;
        }

        var data = await CacheAllServerSetting();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheMin));

        cache.Set(CacheKey, data, cacheEntryOptions);

        return data;
    }

    /// <summary>
    ///     取得館別的設定
    /// </summary>
    /// <param name="lobbyNo"></param>
    /// <returns></returns>
    private ClubEnum GetClubType(int lobbyNo)
    {
        var result = EnumHelper.GetEnum<ClubEnum>(lobbyNo);

        return result;
    }
}