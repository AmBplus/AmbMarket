using Shared.Dto;

namespace _01_Framework.Domain.BaseEntities;

public interface IBaseEntity
{
    public DateTime CreateDate { get; }
    public DateTime UpdateDate { get; }
    public bool Removed { get;  }
    public DateTime? RemovedTime { get; }

    public ResultDto SetUpdateDate();
    public ResultDto SetRemove();
    public ResultDto UnRemove();

}