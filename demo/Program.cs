// See https://aka.ms/new-console-template for more information

using demo;
using demo.Data;
using demo.Infrastructure;
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
                ).LogTo(Console.WriteLine));

        services.AddTransient<LargeService>();
        services.AddTransient<DoWorkWithLargeService>();
    })
    .Build();

var worker = host.Services.GetRequiredService<DoWorkWithLargeService>();

await worker.GetUsersWithFollowerCount();

await host.RunAsync();