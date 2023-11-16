using demo.Data;
using demo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace demo.Infrastructure;

public class LargeService
{
    private readonly IDbContextFactory<SampleContext> _factory;

    public LargeService(IDbContextFactory<SampleContext> factory)
    {
        _factory = factory;
    }

    public async Task<IQueryable<UserData>> GetUsers()
    {
        await using var context = await _factory.CreateDbContextAsync();

        var results = await context.Users
            .Include(f => f.Follows)
            .Include(f => f.Followers)
            .Select(user => new UserData
            {
                Name = user.Name,
                Followers = user.Followers.Count()
            }).ToListAsync();

        return results.AsQueryable();
    }

    public async Task<IQueryable<UserData>> GetUsersNoToList()
    {
        await using var context = await _factory.CreateDbContextAsync();

        var results = context.Users
            .Include(f => f.Follows)
            .Include(f => f.Followers)
            .Select(user => new UserData
            {
                Name = user.Name,
                Followers = user.Followers.Count()
            });

        return results.AsQueryable();
    }

    public async Task<IQueryable<UserData>> GetUsersNoDispose()
    {
        Console.WriteLine("Getting Users With No Dispose");
        
        var context = await _factory.CreateDbContextAsync();

        var results = context.Users
            .Include(f => f.Follows)
            .Include(f => f.Followers)
            .Select(user => new UserData
            {
                Name = user.Name,
                Followers = user.Followers.Count()
            });

        return results.AsQueryable();
    }

    public async Task<DbSet<User>> GetUsersDbSetWithNoDispose()
    {
        Console.WriteLine("Getting Users With No Dispose");
        
        var context = await _factory.CreateDbContextAsync();

        var results = context.Users;

        return results;
    }
}

public class UserData
{
    public string Name { get; set; } = null!;
    public int Followers { get; set; }
}