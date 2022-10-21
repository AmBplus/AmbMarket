namespace Shared.Dto;

public class ResultDto
{
    public bool IsSuccess { get;private set; }
    public string Message { get;private set; }

    public  static ResultDto BuildSuccessResult(string message)
    {
        return new ResultDto()
        {
            IsSuccess = true,
            Message = message,
        };
    }
    public  static ResultDto BuildSuccessResult()
    {
        return new ResultDto()
        {
            IsSuccess = true,
        };
    }
    public  static ResultDto BuildFailedResult(string message)
    {
        return new ResultDto()
        {
            IsSuccess = false,
            Message = message,
        };
    }
}
public class ResultDto<T> : ResultDto where T : new()
{
    //public ResultDto(T data)
    //{
    //    Data = data;
    //}
    //public ResultDto()
    //{
    //    Data = new T();
    //}
    //public T Data { get;private set; }
    //ResultDto BuildSuccessResult(string message,T data)
    //{
    //    var result = ResultDto<T>.BuildSuccessResult(message);
    //    result.IsSuccess= data;
        
    //}
    //public void SetDate(T data)
    //{
    //    Data = data; 
    //}
}
