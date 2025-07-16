using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Domain.myLoanDbEntities;
using myLoan.Infrastructure.Persistence.Models;

namespace myLoan.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>,  IUserRepository
{ 
	public UserRepository(MyLoanDbContext context) : base(context) { }
	
}
