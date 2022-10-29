using _01_Framework.Domain.BaseEntities;

namespace ambMarket.Domain.Visitors;

public class Visitor 
{
    public Guid Id { get; set; }
    public string Ip { get; set; }
    public string CurrentLink { get; set; }
    public string RefererLink { get; set; }
    public string Method { get; set; }
    public string Protocol { get; set; }
    public string PhysicalPath { get; set; }
    public VisitorVersion OperationSystem { get; set; }
    public VisitorVersion Browser { get; set; }
    public Device Device { get; set; }
    public DateTime CreateDateTime { get; set; }
    public string VisitorId { get; set; }
}