using DataAccess.Common.Interfaces;

namespace DataAccess.Dao
{
    public class TicketDao : Common.RepositoryBaseDao<Entities.Entities.Ticket>, Interfaces.ITicketDao
    {
        public TicketDao(IMainContext contexto) : base(contexto)
        {}
    }
}
