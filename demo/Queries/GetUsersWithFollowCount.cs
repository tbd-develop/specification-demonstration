using Ardalis.Result;
using demo.Data.Models;
using demo.Infrastructure;
using demo.Infrastructure.Contracts;
using demo.Specifications;
using demo.Specifications.Filters;
using MediatR;

namespace demo.Queries;

public class GetUsersWithFollowCount
{
    public record Query(string? Name = null) : IRequest<Result<IEnumerable<UserData>>>;

    public class Handler : IRequestHandler<Query, Result<IEnumerable<UserData>>>
    {
        private readonly IRepository<User> _repository;

        public Handler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<UserData>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var specification = string.IsNullOrEmpty(request.Name)
                ? new UserWithFollowsSpec()
                : new UserWithFollowsSpec(new UserFilter { Name = request.Name });

            var results = (from u in await _repository.ListAsync(specification, cancellationToken)
                select new UserData
                {
                    Name = u.Name,
                    Followers = u.Followers.Count
                }).ToList();

            return results.Any() ? Result.Success(results.AsEnumerable()) : Result<IEnumerable<UserData>>.NotFound();
        }
    }
}