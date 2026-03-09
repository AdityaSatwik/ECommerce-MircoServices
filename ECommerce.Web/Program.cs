/// <summary>
/// Entry point for the ECommerce Web application.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "AdminAuth";
        options.LoginPath = "/Account/Login";
    });

// Register Gateway Service
builder.Services.AddHttpClient<ECommerce.Web.Services.Interfaces.IGatewayService, ECommerce.Web.Services.GatewayService>(client =>
{
    var gatewayUrl = builder.Configuration["GatewayUrl"] ?? "http://api-gateway:8080";
    client.BaseAddress = new Uri(gatewayUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
