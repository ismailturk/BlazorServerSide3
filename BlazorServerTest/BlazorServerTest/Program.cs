using BlazorServerTest.Data;
using DataAccessLibrary;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorServerTest.Hubs; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorServerTest.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BlazorServerTestContextConnection") ?? throw new InvalidOperationException("Connection string 'BlazorServerTestContextConnection' not found.");

builder.Services.AddDbContext<BlazorServerTestContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BlazorServerTestContext>();;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IPlayersData,PlayersData>();
builder.Services.AddTransient<IClubsData, ClubsData>();
builder.Services.AddTransient<ICoachsData, CoachsData>();
builder.Services.AddTransient< CacheCoachData>();
builder.Services.AddTransient<IAdminData, AdminData>();
//builder.Services.AddTransient<ICacheCoachData,CacheCoachData>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<TokenProvider>();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
   
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapHub<DemoHub>("demohub");
app.MapHub<ClubHub>("clubhub");
app.MapFallbackToPage("/_Host");
app.UseAuthentication();;
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
