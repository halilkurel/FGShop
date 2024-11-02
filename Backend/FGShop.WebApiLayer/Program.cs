using FGShop.BussinessLayer.DependencyResolvers.Microsoft;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// JWT kimlik do�rulama ayarlar�n� yap�land�r�n
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;  //HTTPS gereklili�ini devre d��� b�rak�r. Canl�ya al�nd���nda true olmal�d�r
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = "http://localhost:7171",      // Token kim taraf�ndan olu�turuldu
        ValidAudience = "https://localhost:7163",       //Kim taraf�ndan kullan�ls�n
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetcoreapiapiaspnetcoreapiapi")),    //Token imzas�d�r. Tam ne i�e yarar anlayamad�m . Ara�t�ralacak!!
        ValidateIssuerSigningKey = true,    //Token��n ge�erli bir imzaya sahip olup olmad���n� do�rular. E�er token �zerinde oynama olduysa token ge�ersiz olacak
        ValidateLifetime = true,    //Tokenin ge�erlilik s�resini kontrol eder
        ClockSkew = TimeSpan.Zero   //Zaman uyumsuzluk pay�n� s�f�r olarak ayarlar. Varsay�lan olarak 5 dakikad�r.
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

app.UseAuthentication(); // Kimlik do�rulama middleware'�n� ekleyin
app.UseAuthorization();

app.MapControllers();

app.Run();
