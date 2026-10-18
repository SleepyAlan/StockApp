using FinnhubServices.Extensions;
using StockApp.Options;
using Services;
using ServiceContracts;

var builder = WebApplication.CreateBuilder(args);

var mvcBuilder = builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

builder.Services
    .AddFinnHubServices(builder.Configuration)
    .AddScoped<IStocksService, StocksService>()
    .Configure<TradingOptions>(
        builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseWebSockets();
app.MapControllers();

app.Run();
