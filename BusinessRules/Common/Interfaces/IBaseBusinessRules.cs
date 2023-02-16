namespace BusinessRules.Common.Interfaces
{
    public interface IBaseBusinessRules<T>
         where T : class, new()
    {
        string NameClassReference { get; }
    }
}
