using HDIApi.Bussines;
using HDIApi.Bussines.Interface;
using HDIApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// se inicia la configuracion de servicio
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
var stringConnection = configuration.GetConnectionString("mysql");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    { 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //ValidIssuer = configuration["JWT:Issuer"],
            //ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
        };
    });



builder.Services.AddDbContext<InsurancedbContext>(options =>
                options.UseMySql(
                    stringConnection, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"
                )));
builder.Services.AddScoped<IUsersProvider, UsersProvider>();
builder.Services.AddScoped<IPolicyProvider, PolicyProvider>();
builder.Services.AddScoped<IReportProvider, ReportProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    // Obtener los encabezados de la solicitud
    var headers = context.Request.Headers;

    // Recorrer los encabezados y mostrarlos en la consola
    foreach (var (headerName, headerValues) in headers)
    {
        Console.WriteLine($"{headerName}: {string.Join(", ", headerValues)}");
    }

    await next.Invoke();
});
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
