using Auth.Domain.Entities;
using Auth.Infra.Data.Context;
using Auth.Infra.Ioc;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection;
using System.Text;
using Auth.WebAPI;
using Auth.WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddFluentValidation(options =>
    {
        // Validate child properties and root collection elements
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;

        // Automatic registration of validators in assembly
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:5173", "https://auth-front-ten.vercel.app")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .AllowCredentials();
                          });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ConfigServices
builder.Services
       .AddDbContext<AuthContext>(options =>
            options.UseSqlServer(
                  builder.Configuration.GetConnectionString("DefaultConnection")
            )
       );


IoC.RegisterServices(builder.Services);

builder.Services
    .AddDefaultIdentity<User>()
    .AddEntityFrameworkStores<AuthContext>();

JwtBearerConfiguration.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
