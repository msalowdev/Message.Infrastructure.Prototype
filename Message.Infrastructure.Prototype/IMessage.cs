
namespace Message.Infrastructure.Prototype
{
    public interface IMessage
    {
        string CorrelationId { get; set; }
    }
}
