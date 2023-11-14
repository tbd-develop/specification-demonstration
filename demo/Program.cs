// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using demo;
using demo.Data;
using demo.Infrastructure;
using demo.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        services.AddPooledDbContextFactory<SampleContext>(
            configure =>
                configure.UseSqlServer(
                    host.Configuration.GetConnectionString("sample")
                ).LogTo((msg) => Debug.WriteLine(msg)));

        services.AddScoped<SampleContext>((provider) =>
        {
            var factory = provider.GetRequiredService<IDbContextFactory<SampleContext>>();

            return factory.CreateDbContext();
        });

        services.AddTransient<LargeService>();
        services.AddTransient<DoWorkWithLargeService>();
        services.AddTransient<DoWorkWithRepositories>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    })
    .Build();

// var worker = host.Services.GetRequiredService<DoWorkWithLargeService>();
//
// await worker.GetUsersWithFollowerCount();
//
// await worker.GetUsersWithFollowerCountNoToList();
//
// await worker.GetUsersWithFollowerCountNoDispose();

var simpler = host.Services.GetRequiredService<DoWorkWithRepositories>();

await simpler.GetUsersWithFollowerCount();

await simpler.GetUsersFollowerCountProjection();

await simpler.GetUsersWhoFollowMe();

await host.RunAsync();