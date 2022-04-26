using Microsoft.Extensions.DependencyInjection;
using Repository.Concrete;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace payroc_shortener.Services
{
    public static class Services
    {
        public static void FakeStore(this IServiceCollection services)
        {
            services.AddSingleton<FakeStore>();
        }

        public static void ConfigureRepo(this IServiceCollection services)
        {
            services.AddScoped<IRepoWrapper, RepoWrapper>();
        }
    }
}
