using FluentResults;
using Microsoft.EntityFrameworkCore;
using myLoan.Application.Interface.MyLoanRepository;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace myLoan.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _dbcontext;

    public Repository(DbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Result<int>> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbcontext.Entry(entity).State = EntityState.Added;
        int rowsAffected = await _dbcontext.SaveChangesAsync(cancellationToken);

        return rowsAffected > 0 ? Result.Ok(rowsAffected) : Result.Fail("Error on creating record!");
    }

    public async Task<Result<bool>> GetAnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        var result = await _dbcontext.Set<T>().AnyAsync(expression, cancellationToken);
        return result;
    }

    public async Task<Result<IEnumerable<T>>> GetAsync(CancellationToken cancellationToken)
    {
        var response = await _dbcontext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        return response.Any() ? Result.Ok((IEnumerable<T>)response) : Result.Fail("No record found!");
    }

    public async Task<Result<IEnumerable<T>>> GetByAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        var response = await _dbcontext.Set<T>().AsNoTracking().Where(expression).ToListAsync(cancellationToken);
        return response.Any() ? Result.Ok((IEnumerable<T>)response) : Result.Fail("No record found!");
    }

    public async Task<Result<int>> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbcontext.Entry(entity).State = EntityState.Modified;
        int rowsAffected = await _dbcontext.SaveChangesAsync(cancellationToken);

        return rowsAffected > 0 ? Result.Ok(rowsAffected) : Result.Fail("Error on updating record!");
    }
}
