using Familia.WebApp.MVC.Configuration;
using Familia.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Clients.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityConfiguration();
builder.Services.AddControllersWithViews();
builder.Services.AddMvcConfiguration();
builder.Services.RegisterServices();

JsonConfigure(builder);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Register IHttpContextAccessor and HttpClientAuthorizationDelegatingHandler
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

// Configure HttpClient with JWT token
builder.Services.AddHttpClient("FamiliaresAPI", client =>
{
    var apiUrl = builder.Configuration["AppSettings:FamiliaUrl"];
    if (string.IsNullOrEmpty(apiUrl))
    {
        throw new ArgumentNullException("AppSettings:FamiliaUrl", "The API URL cannot be null or empty.");
    }
    client.BaseAddress = new Uri(apiUrl);
})
.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseIdentityConfiguration();
app.UseMvcConfiguration(app.Environment);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Familia}/{id?}");

app.Run();

static void JsonConfigure(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.MaxDepth = 64; // Increase the maximum depth if needed
                });
}
