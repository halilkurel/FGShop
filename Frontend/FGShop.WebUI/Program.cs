using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();



// HttpClient servisini ekleyin
builder.Services.AddHttpClient();

// Oturum ve cache yapýlandýrmasý
builder.Services.AddDistributedMemoryCache(); // Cache için


var requireAuthorizePolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddAuthorization(options =>
{
    // Admin rolü için bir politika tanýmlayýn
    options.AddPolicy("RequireAdministratorRole", policy =>
        policy.RequireRole("Admin"));

    // User rolü için bir politika tanýmlayýn
    options.AddPolicy("RequireUserRole", policy =>
        policy.RequireRole("User"));
});

// Kimlik doðrulama ve Cookie Authentication yapýlandýrmasý
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/LoginSignIn/Index"; // Giriþ sayfasý yolu
        options.AccessDeniedPath = "/LoginSignIn/Index"; // Eriþim reddedildiðinde yönlendirme
    });

// MVC ve API denetleyicilerini ekleyin
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy)); // Tüm denetleyicilere kimlik doðrulama gerektir
});

var app = builder.Build();

// HTTP istek hattýný yapýlandýrma
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

app.UseSession(); // Session orta katmanýný ekleyin

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");


app.Run();
