using MySql.Data.MySqlClient;
using System;
using System.Data;
using UOF.Core.Interfaces;
using UOF.Service.Repositories;

namespace UOF.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        IDbTransaction _transaction;
        IDbConnection _connection;
        
        bool _disposed;

        IUserRepository _userRepository;

        public UnitOfWork()
        {
            try
            {
                _connection = new MySqlConnection("");
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IUserRepository userRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_transaction));
            }
        }

        public bool Commit()
        {
            bool rtn = false;
            try
            {
                _transaction.Commit();
                rtn = true;
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
            return rtn;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Rollback()
        {
            bool rtn = false;
            try
            {
                _transaction?.Rollback();
                rtn = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
            return rtn;
        }

        private void resetRepositories()
        {
            _userRepository = null;         
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
