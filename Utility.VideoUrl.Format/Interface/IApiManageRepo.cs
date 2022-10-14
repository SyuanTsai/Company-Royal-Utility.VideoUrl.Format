
using System.Threading.Tasks;

#if NET5_0
#elif NET6_0
using VideoUrlFormat.Model.Xg;
namespace VideoUrlFormat.Interface;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public interface IApiManageRepo
{
    /// <summary>
    ///     取回XG館的視訊
    /// </summary>
    Task<string> QueryXgVideo(XgVideoRequest request);
}