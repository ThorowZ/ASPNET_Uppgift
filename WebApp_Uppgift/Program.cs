
//Depedency Injection
using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using WebApp_Uppgift.Models;
using WebApp_Uppgift.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<SignUpViewModel>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddIdentity<UserEntity, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/RegisterAuth/Login";
    x.LogoutPath = "/RegisterAuth/Logout";
    x.AccessDeniedPath = "/RegisterAuth/AccessDenied";
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;
    x.SlidingExpiration = true;
    x.Cookie.Expiration = TimeSpan.FromDays(30);
});


var app = builder.Build();



app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();


//app.UseRewriter(new RewriteOptions)

// Tagit hjälp av ChatGPT

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/projects");
        return;
    }
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RegisterAuth}/{action=projects}/{id?}")
    .WithStaticAssets();


app.Run();
