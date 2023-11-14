using demo.Data;
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
}

public class UserData
{
    public string Name { get; set; } = null!;
    public int Followers { get; set; }
}