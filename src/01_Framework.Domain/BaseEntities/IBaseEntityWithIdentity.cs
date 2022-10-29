namespace _01_Framework.Domain.BaseEntities;

public interface IBaseEntityWithIdentity<T> : IBaseEntity
{
    T Id { get; set; }
}