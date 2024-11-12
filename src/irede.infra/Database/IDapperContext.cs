using irede.shared.Notifications;
using System.Data;

namespace irede.infra.Database
{
    public interface IDapperContext: INotifiable, IDisposable
    {
        IDbConnection CreateConnection();
    }
}