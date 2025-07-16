using FluentResults;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myLoan.Application.Interface.MyLoanRepository
{
    public interface IRepository<T>
    {
        public Task<Result<IEnumerable<T>>> GetAsync(CancellationToken cancellationToken);

        public Task<Result<IEnumerable<T>>> GetByAsync(Expression<Func<T,bool>> expression, CancellationToken cancellationToken);

        public Task<Result<bool>> GetAnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);

        public Task<Result<int>> CreateAsync(T entity, CancellationToken cancellationToken);

        public Task<Result<int>> UpdateAsync(T entity, CancellationToken cancellationToken);

    }
}
