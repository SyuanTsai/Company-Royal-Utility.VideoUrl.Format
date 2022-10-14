using System.Threading.Tasks;
using VideoUrlFormat.Domain.Server;

#if NET5_0
#elif NET6_0
using VideoUrlFormat.Model;
namespace VideoUrlFormat.Interface;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public interface IClubFactory
{
    Task<Video>? GenerateVideoByGame(GameInfo info, Server server);
}