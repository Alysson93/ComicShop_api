using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/// Serviços que devem ser adicionado à aplicação:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration["ConnectionString"], 
        new MySqlServerVersion(new Version(8, 0, 26))
));
builder.Services.AddScoped<ComicService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CouponService>();
builder.Services.AddScoped<PurchaseService>();
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

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context => {
    var filePath = "./index.html";
    await context.Response.SendFileAsync(filePath);
});

app.Run();
