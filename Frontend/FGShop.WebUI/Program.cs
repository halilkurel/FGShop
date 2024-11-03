using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// HttpClient servisini ekleyin
builder.Services.AddHttpClient();

// Oturum ve cache yap�land�rmas�
builder.Services.AddDistributedMemoryCache(); // Cache i�in


var requireAuthorizePolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddAuthorization(options =>
{
    // Admin rol� i�in bir politika tan�mlay�n
    options.AddPolicy("RequireAdministratorRole", policy =>
        policy.RequireRole("Admin"));

    // User rol� i�in bir politika tan�mlay�n
    options.AddPolicy("RequireUserRole", policy =>
        policy.RequireRole("User"));
});

// Kimlik do�rulama ve Cookie Authentication yap�land�rmas�
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/LoginSignIn/Index"; // Giri� sayfas� yolu
        options.AccessDeniedPath = "/LoginSignIn/Index"; // Eri�im reddedildi�inde y�nlendirme
    });

// MVC ve API denetleyicilerini ekleyin
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy)); // T�m denetleyicilere kimlik do�rulama gerektir
});

var app = builder.Build();

// HTTP istek hatt�n� yap�land�rma
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kimlik do�rulama ve yetkilendirme orta katmanlar�n� ekleyin
app.UseAuthentication();
app.UseAuthorization();

// MVC rota yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

// Area destekli rota yap�land�rmas�
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
