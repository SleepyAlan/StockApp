using FinnhubServices.Interfaces;
using FinnhubServices.Implementations;
using FinnhubServices.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinnhubServices.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddFinnHubServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ApiOptions>(configuration.GetSection("finnhub"));

        services.AddHttpClient<IFinnhubService, FinnhubService>(client =>
        {
            client.BaseAddress = new Uri("https://finnhub.io");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return services;
    }
}
