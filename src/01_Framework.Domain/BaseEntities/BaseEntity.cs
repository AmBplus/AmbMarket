using Shared;
using Shared.Dto;

namespace _01_Framework.Domain.BaseEntities;

public abstract class BaseEntity<T> : IBaseEntity
{
    #region Fileds

    private readonly DateTime _createDate;
    private DateTime _updateDate;
    private bool _removed;
    private DateTime? _removedTime;
    protected T Id { get;private set; }

    #endregion /Fileds

    #region Constructors
    public BaseEntity(DateTime createDate)
    {
        _updateDate = _createDate = createDate;
    }

    public BaseEntity()
    {
        _updateDate = _createDate = Shared.Utility.Now;
    }

    #endregion /Constructors
 
    #region Properties
    public DateTime CreateDate => _createDate;

    public DateTime UpdateDate => _updateDate;

    public bool Removed => _removed;

    public DateTime? RemovedTime => _removedTime;
    #endregion
 
    #region Methods
    public ResultDto SetUpdateDate()
    {
        _updateDate = Utility.Now;
        return ResultDto.BuildSuccessResult();
    }

    public ResultDto SetRemove()
    {
        _removed = true;
        _removedTime = Utility.Now;
        return ResultDto.BuildSuccessResult();
    }

    public ResultDto UnRemove()
    {
        _removed = false;
        _removedTime = null;
        return ResultDto.BuildSuccessResult();
    }
    #endregion /Methods
}