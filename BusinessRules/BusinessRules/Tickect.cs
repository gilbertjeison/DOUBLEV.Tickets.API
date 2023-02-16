using DataAccess.Interfaces;

namespace BusinessRules.BusinessRules
{
    public class Tickect : Common.BaseBusinessRules<Entities.Entities.Ticket, DataAccess.Interfaces.ITicketDao>, Interfaces.ITicket
    {
        public Tickect(ITicketDao daoNegocio) : base(daoNegocio)
        {
        }
    }
}
