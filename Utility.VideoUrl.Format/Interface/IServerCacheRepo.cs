using System.Collections.Generic;
using System.Threading.Tasks;
using VideoUrlFormat.Domain.Server;

#if NET5_0
#elif NET6_0
using VideoUrlFormat.Enums;
namespace VideoUrlFormat.Interface;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public interface IServerCacheRepo
{
    
    /// <summary>
    ///     查詢伺服器設定
    /// </summary>
    /// <returns></returns>
    Task<List<Server>> CacheAllServerSetting();

    /// <summary>
    ///     查詢伺服器設定
    /// </summary>
    /// <returns></returns>
    Task<Server> GetServerInfo(string serverId);

    /// <summary>
    ///     將資料快取並保存20分鐘。
    /// </summary>
    /// <param name="cacheMin"></param>
    /// <returns></returns>
    Task<List<Server>> CacheServer(int cacheMin = 10);

    /// <summary>
    ///     取得館別的設定
    /// </summary>
    /// <param name="lobbyNo"></param>
    /// <returns></returns>
    ClubEnum GetClubType(int lobbyNo);

}