namespace EmeraldChameleonChat.Model.DTO
{
    public interface IDTO
    {
        public interface IDTO<T>
        {
            T Id { get; set; }
        }

        public interface IDTO : IDTO<Guid> 
        { 
        
        }
    }
}
