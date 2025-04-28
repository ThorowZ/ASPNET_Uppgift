
//Depedency Injection
using Microsoft.AspNetCore.Rewrite;
using WebApp_Uppgift.Models;
using WebApp_Uppgift.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<SignUpViewModel>();
builder.Services.AddScoped<ClientService>();


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
