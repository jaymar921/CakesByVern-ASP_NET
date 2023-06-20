using CakesByVern_ASP_NET_WEB.Data;
using CakesByVern_Data.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();

// add the db context
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source=CakesByVernSqLiteDB"));
// responsible for database
builder.Services.AddScoped<IDataRepository, SqLiteDataRepository>();

// responsible for adding authentication
builder.Services.AddAuthentication(
    option =>
    {
        option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }
).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// responsible for adding authentication
app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
