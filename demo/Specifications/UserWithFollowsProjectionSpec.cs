using Ardalis.Specification;
using demo.Data.Models;
using demo.Infrastructure;

namespace demo.Specifications;

public class UserWithFollowsProjectionSpec : Specification<User, UserData>
{
    public UserWithFollowsProjectionSpec()
    {
        Query.Include(c => c.Followers);

        Query.Select(x => new UserData
        {
            Name = x.Name,
            Followers = x.Followers.Count
        });
    }
}