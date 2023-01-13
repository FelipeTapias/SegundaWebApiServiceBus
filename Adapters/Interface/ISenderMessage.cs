namespace Adapters.Interface
{
    public interface ISenderMessage
    {
        Task CreateMessage(string message);
    }
}
