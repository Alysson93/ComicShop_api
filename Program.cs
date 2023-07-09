using Microsoft.EntityFrameworkCore;
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

DotNetEnv.Env.Load();
var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("Secret"));

var builder = WebApplication.CreateBuilder(args);

/// Serviços que devem ser adicionado à aplicação:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        Environment.GetEnvironmentVariable("ConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 26))
));
builder.Services.AddScoped<ComicService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CouponService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddCors();
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
////////////////////////////////////////////////


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context => {
    var filePath = "./index.html";
    await context.Response.SendFileAsync(filePath);
});

app.Run();
