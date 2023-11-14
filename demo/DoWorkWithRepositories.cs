using demo.Data.Models;
using demo.Infrastructure.Contracts;
using demo.Specifications;

namespace demo.Infrastructure;

public class DoWorkWithRepositories
{
    private readonly IRepository<User> _repository;
    private readonly IRepository<Follow> _followRepository;

    public DoWorkWithRepositories(IRepository<User> repository, IRepository<Follow> followRepository)
    {
        _repository = repository;
        _followRepository = followRepository;
    }

    public async Task GetUsersWithFollowerCount()
    {
        var users = from u in await _repository.ListAsync(new UserWithFollowsSpec())
            select new UserData
            {
                Name = u.Name,
                Followers = u.Followers.Count
            };

        foreach (var user in users)
        {
            Console.WriteLine($"{user.Name} has {user.Followers} followers");
        }
    }

    public async Task GetUsersFollowerCountProjection()
    {
        var users = await _repository.ListAsync(new UserWithFollowsProjectionSpec());

        foreach (var user in users)
        {
            Console.WriteLine($"{user.Name} has {user.Followers} followers");
        }
    }

    public async Task GetUsersWhoFollowMe()
    {
        var spec = new FollowedByUsersSpec(1);

        var followers = from f in await _followRepository.ListAsync(spec)
            select new UserData
            {
                Name = f.User.Name,
                Followers = f.User.Follows.Count
            };

        foreach (var follow in followers)
        {
            Console.WriteLine($"{follow.Name} has {follow.Followers} followers");
        }
    }
}