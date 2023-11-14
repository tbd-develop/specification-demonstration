using Ardalis.Specification;
using demo.Data.Models;
using demo.Specifications.Filters;

namespace demo.Specifications;

public class UserWithFollowsSpec : Specification<User>
{
    public UserWithFollowsSpec()
    {
        Query.Include(c => c.Followers);
    }

    public UserWithFollowsSpec(UserFilter filter)
    {
        Query.Include(c => c.Followers)
            .Where(u => u.Name.Contains(filter.Name));
    }
}