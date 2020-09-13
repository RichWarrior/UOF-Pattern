using System;

namespace UOF.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository userRepository { get; }        
        bool Commit();
        bool Rollback();
    }
}
