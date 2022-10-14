using System;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoUrlFormat.Context;

#if NET5_0
#elif NET6_0
using VideoUrlFormat.Abstract.Clubs;
using VideoUrlFormat.Abstract.GameClubVideo;
using VideoUrlFormat.Factories;
using VideoUrlFormat.Interface;
using VideoUrlFormat.Model.Base;
using VideoUrlFormat.Repositories;
namespace VideoUrlFormat.DependencyInjections;
#else
#error This code block does not match csproj TargetFrameworks list
#endif

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVideoUrlFormat(this IServiceCollection services
                                                     , IConfiguration          config)
    {
        Console.WriteLine("套建載入");
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var setting = config.GetSection("VideoSetting")
                            .Get<VideoSetting>();

        //  註冊ServerContext
        services.AddDbContext<ServerContext>
            (options => { options.UseSqlServer(setting.ServerConnection, ContextSetting()); });

        services.AddScoped<IApiManageRepo>
        (
            provider =>
                new ApiManageRepo(provider.GetService<IHttpClientFactory>()!, setting.KsApi)
        );

        //  遊戲客製視訊
        services.AddScoped<IGenerateVideo, OldCommonVideo>();  //  標準視訊(舊版)
        services.AddScoped<IGenerateVideo, CommonVideo>();     //  標準視訊
        services.AddScoped<IGenerateVideo, GcRgRacingVideo>(); //  GC賽車
        services.AddScoped<IGenerateVideo, XgCommonVideo>();   //  Xg全部
        services.AddScoped<IGenerateVideo, WmCommonVideo>();   //  Wm全部
        
        //  視訊工廠 - 
        services.AddScoped<OldCommonVideoFactory>(); //  標準格式(舊版)
        services.AddScoped<CommonVideoFactory>();    //  標準格式
        services.AddScoped<GcVideoFactory>();        //  Gc館遊戲輸出
        services.AddScoped<XgVideoFactory>();        //  Xg館遊戲輸出
        services.AddScoped<WmVideoFactory>();        //  Wm館遊戲輸出
        //  館別Factory
        services.AddScoped<ClubFactory>();
        services.AddScoped<IClubFactory, OldCommonClubFilter>(); //  基本館的實做(舊版)
        services.AddScoped<IClubFactory, CommonClubFilter>();    //  基本館的實做
        services.AddScoped<IClubFactory, GcClubFilter>();        //  Gc館的實做
        services.AddScoped<IClubFactory, XgClubFilter>();        //  Xg館的實做
        services.AddScoped<IClubFactory, WmClubFilter>();        //  Wm館的實做

        return services;
    }

    /// <summary>
    ///     Sql Server 的設定
    /// </summary>
    /// <returns></returns>
    private static Action<SqlServerDbContextOptionsBuilder> ContextSetting()
    {
        return options =>
        {
            options.CommandTimeout(10);
            options.EnableRetryOnFailure(3);
        };
    }
}