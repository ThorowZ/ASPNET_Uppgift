
//Depedency Injection
using WebApp_Uppgift.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProjectService>();


var app = builder.Build();



app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Tagit hjälp av ChatGPT

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/login");

    }
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RegisterAuthController}/{action=login}/{id?}")
    .WithStaticAssets();


app.Run();
