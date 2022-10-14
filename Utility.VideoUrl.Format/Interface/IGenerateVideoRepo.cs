using System.Threading.Tasks;
using VideoUrlFormat.Domain.Server;

#if NET5_0
#elif NET6_0
using VideoUrlFormat.Model;
namespace VideoUrlFormat.Interface;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public interface IGenerateVideo
{
    /// <summary>
    ///     產生視訊網址
    /// </summary>
    /// <param name="info"></param>
    /// <param name="server"></param>
    /// <returns></returns>
    Task<Video> GenerateVideo(GameInfo info
                            , Server   server);
}