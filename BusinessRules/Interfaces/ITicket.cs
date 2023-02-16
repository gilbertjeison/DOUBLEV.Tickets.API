namespace BusinessRules.Interfaces
{
    public interface ITicket: DataAccess.Interfaces.ITicketDao, Common.Interfaces.IBaseBusinessRules<Entities.Entities.Ticket>
    {
    }
}
