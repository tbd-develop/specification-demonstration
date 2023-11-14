using Ardalis.Specification;
using demo.Data;

namespace demo.Infrastructure.Contracts;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}