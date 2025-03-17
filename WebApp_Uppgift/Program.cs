
//Depedency Injection
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();



var app = builder.Build();



app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProjectManagement}/{action=Home}/{id?}")
    .WithStaticAssets();


app.Run();
