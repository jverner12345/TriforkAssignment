using LoggerServices.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public static class LoggerService
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<LoggerManager, LoggerManager>();
        }
    }
}
