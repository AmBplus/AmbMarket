using Shared;
using Shared.Dto;

namespace _01_Framework.Domain.BaseEntities;

public abstract class BaseEntity
{

    #region Constructors
 
    public BaseEntity()
    {
        UpdateDate = CreateDate = Shared.Utility.Now;
    }

    #endregion /Constructors

    #region Properties
    public DateTime CreateDate { get; }
    public DateTime UpdateDate { get; private set; }
    public bool Removed { get; private set; }
    public DateTime? RemovedTime { get; private set; }
    #endregion

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
 