using Microsoft.Extensions.DependencyInjection;
using Models.Concrete;
using Repository.Concrete;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public static class Services
    {
        public static void FakeStore(this IServiceCollection services)
        {
            services.AddSingleton<FakeContext<Group>>();
            services.AddSingleton<StoreBuild<Group>>();
            services.AddSingleton<FakeContext<Transaction>>();
            services.AddSingleton<StoreBuild<Transaction>>();
            services.AddSingleton<FakeContext<Ledger>>();
            services.AddSingleton<StoreBuild<Ledger>>();
        }

        public static void ConfigureRepo(this IServiceCollection services)
        {
            services.AddScoped<IRepoWrapper<Ledger>, RepoWrapper<Ledger>>();
            services.AddScoped<IRepoWrapper<Group>, RepoWrapper<Group>>();
            services.AddScoped<IRepoWrapper<Transaction>, RepoWrapper<Transaction>>();
            services.AddScoped<IRepoWrapper<GroupMember>, RepoWrapper<GroupMember>>();
        }
    }
}
