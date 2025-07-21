using BloomBridge.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=BloomBridge.db"));
builder.Services.AddScoped<ITherapistMatcherService, TherapistMatcherService>();

// Add CORS policy
var allowedOrigins = new[] { "http://localhost:8081" };
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhostFrontend", policy =>
  {
    policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  .SetIsOriginAllowed(origin => string.IsNullOrEmpty(origin) || allowedOrigins.Contains(origin));
  });
});


var app = builder.Build();

app.UseCors("AllowLocalhostFrontend");

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

// Use minimal routing by mapping controllers directly
app.MapControllers();

// so that my phone can make requests too
app.Urls.Add("http://0.0.0.0:5087");

app.Run();
