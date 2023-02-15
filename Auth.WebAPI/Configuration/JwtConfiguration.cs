using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Auth.WebAPI.Configuration
{
    public class JwtBearerConfiguration
    {
        public static void Configure(IServiceCollection services, ConfigurationManager configuration)
        {
            services
                  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(option =>
                  {
                      option.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = false,
                          ValidateAudience = false,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,

                          ValidIssuer = configuration["Jwt:Issuer"],
                          ValidAudience = configuration["Jwt:Audience"],
                          IssuerSigningKey = JwtSecurityKey.Create(configuration["Jwt:SecretKey"])
                      };

                      option.Events = new JwtBearerEvents
                      {
                          OnAuthenticationFailed = context =>
                          {
                              Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                              return Task.CompletedTask;
                          },
                          OnTokenValidated = context =>
                          {
                              Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                              return Task.CompletedTask;
                          }
                      };
                  });
        }
    }
}
