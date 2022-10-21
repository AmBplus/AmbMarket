using Microsoft.AspNetCore.Identity;
using Shared;
using Shared.Dto;

namespace _01_Framework.Domain.BaseEntities;

public abstract class BaseEntityWithIdentity : IdentityUser ,  IBaseEntity
{
    #region Fields
    private DateTime _createDate;
    private DateTime _updateDate;
    private bool _removed;
    private DateTime? _removedTime;
    #endregion /Fields

    #region Constructors
    public BaseEntityWithIdentity(DateTime createDate)
    {
        _updateDate = _createDate = createDate;
    }

    public BaseEntityWithIdentity()
    {
        _updateDate = _createDate = Shared.Utility.Now;
    }
    #endregion /Constructors
   
    #region Properties

    public DateTime CreateDate => _createDate;

    public DateTime UpdateDate => _updateDate;

    public bool Removed => _removed;

    public DateTime? RemovedTime => _removedTime;

    #endregion /Properties
    
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