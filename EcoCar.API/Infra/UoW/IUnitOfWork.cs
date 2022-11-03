using System.Data;

namespace EcoCar.API.Infra.UoW
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        IDbConnection NewConnectionIsolated();

        IDbTransaction Begin();

        bool Commit();

        bool IsConnectionOpen();
    }
}