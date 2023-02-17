using Auth.Application;
using Auth.Application.Interfaces;
using Auth.Services.Interfaces;
using Auth.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infra.Ioc
{
    public class IoC
    {
        public static void RegisterServices(IServiceCollection services)
        {

            // Services
            services.AddTransient<IEmailService, EmailService>();

            // Application
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<ITokenAppService, TokenAppService>();
        }
    }
}
