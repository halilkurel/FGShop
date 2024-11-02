using FGShop.BussinessLayer.DependencyResolvers.Microsoft;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// JWT kimlik doðrulama ayarlarýný yapýlandýrýn
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;  //HTTPS gerekliliðini devre dýþý býrakýr. Canlýya alýndýðýnda true olmalýdýr
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = "http://localhost:7171",      // Token kim tarafýndan oluþturuldu
        ValidAudience = "https://localhost:7163",       //Kim tarafýndan kullanýlsýn
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetcoreapiapiaspnetcoreapiapi")),    //Token imzasýdýr. Tam ne iþe yarar anlayamadým . Araþtýralacak!!
        ValidateIssuerSigningKey = true,    //Token’ýn geçerli bir imzaya sahip olup olmadýðýný doðrular. Eðer token üzerinde oynama olduysa token geçersiz olacak
        ValidateLifetime = true,    //Tokenin geçerlilik süresini kontrol eder
        ClockSkew = TimeSpan.Zero   //Zaman uyumsuzluk payýný sýfýr olarak ayarlar. Varsayýlan olarak 5 dakikadýr.
    };
});

// Add services to the container.
builder.Services.AddDependencies();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Kimlik doðrulama middleware'ýný ekleyin
app.UseAuthorization();

app.MapControllers();

app.Run();
