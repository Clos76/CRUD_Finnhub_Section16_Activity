using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>
    (builder.Configuration.GetSection("TradingOptions"));

builder.Services.AddHttpClient<IFinnhubService, FinnhubService>();

var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();


app.Run();
