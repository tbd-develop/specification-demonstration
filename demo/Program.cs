// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Ardalis.Result;
using demo;
using demo.Data;
using demo.Infrastructure;
using demo.Infrastructure.Contracts;
using demo.Queries;
using MediatR;
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
        services.AddTransient<BlazorFrontEndMock>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddMediatR(configure =>
            configure.RegisterServicesFromAssembly(typeof(GetUsersWithFollowCount).Assembly));
    })
    .Build();


var output = (IEnumerable<UserData> userData) =>
{
    foreach (var user in userData)
    {
        Console.WriteLine($"{user.Name} has {user.Followers} followers");
    }
};

// Current Version - Uncomment to run

//await DoWorkHowAsItsUsedNow();

// Improved using specs - Uncomment to run

//await DoWorkWithSpecs();

// Improved using MediatR and Specs and Result

//await DoWorkWithMediator();

// Verify Blazor Behavior of manipulating connection

await DemonstrateRetrievingFilteredData();

await host.RunAsync();

async Task DoWorkHowAsItsUsedNow()
{
    var worker = host.Services.GetRequiredService<DoWorkWithLargeService>();

    await worker.GetUsersWithFollowerCount();

    await worker.GetUsersWithFollowerCountNoToList();

    await worker.GetUsersWithFollowerCountNoDispose();
}

async Task DoWorkWithSpecs()
{
    var simpler = host.Services.GetRequiredService<DoWorkWithRepositories>();

    await simpler.GetUsersWithFollowerCount();

    await simpler.GetUsersFollowerCountProjection();

    await simpler.GetUsersWhoFollowMe();
}

async Task DoWorkWithMediator()
{
    var mediator = host.Services.GetRequiredService<IMediator>();

// All users
    var result = await mediator.Send(new GetUsersWithFollowCount.Query());

    if (result.IsSuccess)
    {
        output(result.Value);
    }

    var filtered = await mediator.Send(new GetUsersWithFollowCount.Query("Bob"));

    if (filtered.IsSuccess)
    {
        output(filtered.Value);
    }
}

async Task DemonstrateRetrievingFilteredData()
{
    var blazor = host.Services.GetRequiredService<BlazorFrontEndMock>();

    await blazor.GetUsersWithFollowerCount(); // Pull the full data set 

    var users = blazor.ShowUsers(string.Empty);

    output(users);

    var filtered = blazor.ShowUsers("Bob");

    output(users);
}