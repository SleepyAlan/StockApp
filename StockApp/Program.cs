using FinnhubServices.Extensions;
using FinnhubServices.Options;
using StockApp.Options;

var builder = WebApplication.CreateBuilder(args);

var mvcBuilder = builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

builder.Services
    .AddFinnHubServices(builder.Configuration)
    .Configure<TradingOptions>(
        builder.Configuration.GetSection("TradingOptions"))
    .Configure<ApiOptions>(
        builder.Configuration.GetSection("finnhub"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseWebSockets();
app.MapControllers();

app.Run();
