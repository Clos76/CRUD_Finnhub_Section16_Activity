using Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>
    (builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();


app.Run();
