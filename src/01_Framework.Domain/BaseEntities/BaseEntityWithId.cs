using Shared;
using Shared.Dto;

namespace _01_Framework.Domain.BaseEntities;

public class BaseEntityWithId<T>
{
    public T Id { get;private set; }
    public DateTime CreateDate { get;}
    public DateTime UpdateDate { get;private set; }
    public bool Removed { get;private set;  }
    public DateTime? RemovedTime { get; private set;  }

    public BaseEntityWithId()
    {
        UpdateDate = CreateDate = Utility.Now;
    }
    #region /Methods
    public ResultDto SetUpdateDate()
    {
        UpdateDate = Utility.Now;
        return ResultDto.BuildSuccessResult();
    }

    public ResultDto SetRemove()
    {
        Removed = true;
        RemovedTime = Utility.Now;
        return ResultDto.BuildSuccessResult();
    }

    public ResultDto UnRemove()
    {
        Removed = false;
        RemovedTime = null;
        return ResultDto.BuildSuccessResult();
    }
    #endregion /Methods
}