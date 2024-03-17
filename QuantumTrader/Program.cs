using Binance.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuantumTrader.Data;
using QuantumTrader.Data.Repositories;
using QuantumTrader.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<TradeTransactionRepository>();
builder.Services.AddScoped<BinanceSpotAccountTradeService>();
builder.Services.AddScoped<BinanceAccountTrade>();
builder.Services.AddScoped<BinanceWebSocketStreamService>();
builder.Services.AddScoped<BinanceSpotAccountTradeService>();
builder.Services.AddScoped<BinanceAccountTrade>();
builder.Services.AddScoped<BinanceGeneral>();
builder.Services.AddScoped<BinanceMarket>();
builder.Services.AddScoped<BinanceUserDataStream>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<TradingService>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();