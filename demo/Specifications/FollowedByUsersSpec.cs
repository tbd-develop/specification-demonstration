using Ardalis.Specification;
using demo.Data.Models;

namespace demo.Specifications;

public class FollowedByUsersSpec : Specification<Follow>
{
    public FollowedByUsersSpec(int userId)
    {
        Query.Include(f => f.FollowsUser)
            .Include(f => f.User);

        Query.Where(f => f.FollowsUserId == userId && f.UserId != userId);
    }
}