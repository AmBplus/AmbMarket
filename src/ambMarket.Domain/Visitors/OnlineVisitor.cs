namespace ambMarket.Domain.Visitors;

public class OnlineVisitor
{
    public Guid Id { get; set; }
    public DateTime CreateDateTime { get; set; }
    public string ClientId { get; set; }
}