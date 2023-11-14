using Ardalis.Specification;
using demo.Data.Models;

namespace demo.Specifications;

public class UserWithFollowsSpec : Specification<User>
{
    public UserWithFollowsSpec()
    {
        Query.Include(c => c.Followers);
    }
}