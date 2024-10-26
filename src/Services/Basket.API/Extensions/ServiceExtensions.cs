using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Basket.API.Service;
using Basket.API.Service.Interfaces;
using Common.Logging;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Infrastructure.Extensions;
using Infrastructure.Policies;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Configurations;

namespace Basket.API.Extensions;

public static class ServiceExtensions
{
    internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        var eventBusSettings = configuration.GetSection(nameof(EventBusSettings))
            .Get<EventBusSettings>();
        services.AddSingleton(eventBusSettings);


        var cacheSettings = configuration.GetSection(nameof(CacheSettings))
            .Get<CacheSettings>();
        services.AddSingleton(cacheSettings);

        var grpcSettings = configuration.GetSection(nameof(GrpcSettings))
            .Get<GrpcSettings>();
        services.AddSingleton(grpcSettings);

        var backgroundJobSettings = configuration.GetSection(nameof(BackgroundJobSettings))
            .Get<BackgroundJobSettings>();
        services.AddSingleton(backgroundJobSettings);

        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
        services.AddScoped<IBasketRepository, BasketRepository>()
                .AddTransient<ISerializeService, SerializeService>()
                .AddTransient<IEmailTemplateService, BasketEmailTemplateService>()
                .AddTransient<LoggingDelegatingHandler>();

    public static void ConfigureHttpClinetService(this IServiceCollection services)
    {
        services.AddHttpClient<BackgroundJobHttpService>()
            //.AddHttpMessageHandler<LoggingDelegatingHandler>()
            .UseImmediateHttpRetryPolicy();
    }

    public static void ConfigureRedis(this IServiceCollection services)
    {
        var settings = services.GetOptions<CacheSettings>("CacheSettings");
        if (string.IsNullOrEmpty(settings.ConnectionString))
            throw new ArgumentNullException("Redis Connection string is not configured.");

        //Redis configuration
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = settings.ConnectionString;
        });
    }

    public static IServiceCollection ConfigureGrpcServices(this IServiceCollection services)
    {
        //var settings = services.GetOptions<GrpcSettings>(nameof(GrpcSettings));
        //services.AddGrpcClient<StockProtoService.StockProtoServiceClient>(x 
        //    => x.Address = new Uri(settings.StockUrl));
        //services.AddScoped<StockItemGrpcService>();

        return services;
    }

    public static void ConfigureMasstransit(this IServiceCollection services)
    {
        var settings = services.GetOptions<EventBusSettings>("EventBusSettings");
        if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
            throw new ArgumentNullException("EventBusSettings is not configured.");

        var mqConnection = new Uri(settings.HostAddress);
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(mqConnection);
            });

            //Publish submit order message
            //config.AddRequestClient<IBasketCheckoutEvent>();
        });
    }
}
