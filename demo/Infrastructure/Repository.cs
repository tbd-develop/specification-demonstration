using Ardalis.Specification.EntityFrameworkCore;
using demo.Data;
using demo.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace demo.Infrastructure;

public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public Repository(SampleContext dbContext) : base(dbContext)
    {
    }
}