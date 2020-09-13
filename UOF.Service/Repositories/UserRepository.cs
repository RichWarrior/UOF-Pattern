using System.Data;
using UOF.Core.Interfaces;

namespace UOF.Service.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) 
            : base(transaction)
        {
        }
    }
}
