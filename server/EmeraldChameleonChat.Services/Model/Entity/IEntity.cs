namespace EmeraldChameleonChat.Services.Model.Entity
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }

    public interface IEntity : IEntity<Guid>
    {

    }
}
