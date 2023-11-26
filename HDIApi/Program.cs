using HDIApi.Bussines;
using HDIApi.Bussines.Interface;
using HDIApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// se inicia la configuracion de servicio
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
var key = configuration["JWT:Key"];
Console.WriteLine(key);
var stringConnection = configuration.GetConnectionString("mysql");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            // Agrega registros para capturar información sobre fallas de autenticación.
            Console.WriteLine(context.Exception);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("roleType", "admin"));
    options.AddPolicy("Ajustador", policy => policy.RequireClaim("roleType", "ajustador"));
    options.AddPolicy("Conductor", policy => policy.RequireClaim("roleType", "conductor"));
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

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
