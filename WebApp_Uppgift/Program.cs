
//Depedency Injection
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using WebApp_Uppgift.Models;
using WebApp_Uppgift.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<test_ProjectService>();
builder.Services.AddScoped<SignUpViewModel>();
builder.Services.AddScoped<test_ClientService>();
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
    x.ExpireTimeSpan = TimeSpan.FromDays(30);
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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
