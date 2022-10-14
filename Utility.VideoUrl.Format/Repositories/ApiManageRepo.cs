

using System;
using System.Net.Http;
using System.Threading.Tasks;

#if NET5_0
#elif NET6_0
using VideoUrlFormat.Interface;
using VideoUrlFormat.Model.Xg;
namespace VideoUrlFormat.Repositories;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
public class ApiManageRepo : IApiManageRepo
{
    private readonly IHttpClientFactory clientFactory;


    private readonly Uri ksApi;

    /// <summary>
    ///     DI
    /// </summary>
    /// <param name="clientFactory"></param>
    /// <param name="ksApi"></param>
    public ApiManageRepo(IHttpClientFactory clientFactory
                       , Uri                ksApi)
    {
        this.clientFactory = clientFactory;
        this.ksApi         = ksApi;
    }


    /// <summary>
    ///     取回XG館的視訊
    /// </summary>
    public async Task<string> QueryXgVideo(XgVideoRequest request)
    {
        const string router = "/api/GetNoRunXG";

        var uri  = new Uri(ksApi, $"{router}/{request.GameType}");
        var data = await PostAsync<XgVideoRequest, XgVideoResponse>(uri, request);

        return data?.Data.NoRunXg ?? throw new NullReferenceException("Xg Api 無此輪局資料。");
    }

    /// <summary>
    ///     HttpPost
    /// </summary>
    /// <typeparam name="TRequest">請求的Model</typeparam>
    /// <typeparam name="TResponse">回應的Model</typeparam>
    /// <param name="url">請求的Url網址</param>
    /// <param name="data">請求資料</param>
    /// <returns></returns>
    private async Task<TResponse> PostAsync<TRequest, TResponse>(Uri      url
                                                               , TRequest data)
    {
        try
        {
            using var client = clientFactory.CreateClient();

            var response = await client.PostAsJsonAsync(url, data);

            response.EnsureSuccessStatusCode();

            var product = await response.Content.ReadAsAsync<TResponse>();

            return product;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Api：{url} 呼叫失敗。", e);
        }
    }
}