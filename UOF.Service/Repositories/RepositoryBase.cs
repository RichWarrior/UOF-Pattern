using System.Data;
using UOF.Core.Interfaces;

namespace UOF.Service.Repositories
{
    public class RepositoryBase
    {
        private IDbTransaction _transaction { get; set; }

        private IDbContext _instance { get; set; }

        protected IDbContext _connection
        {
            get
            {
                return _instance ?? (_instance = new DbContext(_transaction));
            }
        }

     

        //private  IDbConnection _connection
        //{
        //    get
        //    {
        //        return _transaction.Connection;
        //    }
        //}


        public RepositoryBase(IDbTransaction transaction)
        {
            _transaction = transaction;
        }
    }
}
