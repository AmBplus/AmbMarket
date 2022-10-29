using System.Net.Http.Headers;

namespace Shared.Dto;

public class ResultDto
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    public static ResultDto BuildSuccessResult(string message)
    {
        return new ResultDto()
        {
            IsSuccess = true,
            Message = message,
        };
    }
    public static ResultDto BuildSuccessResult()
    {
        return new ResultDto()
        {
            IsSuccess = true,
        };
    }
    public static ResultDto BuildFailedResult(string message)
    {
        return new ResultDto()
        {
            IsSuccess = false,
            Message = message,
        };
    }
}
public class ResultDto<T>
{
    public bool IsSuccess { get; private set; }
    public string[] Message { get; private set; }
    public ResultDto()
    {

    }
    public T Data { get; private set; }
    ResultDto<T> BuildSuccessResult<T>(T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = true,
        };
    }
    ResultDto<T> BuildSuccessResult(string message, T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = true,
            Message = new[] { message }
        };
    }
    ResultDto<T> BuildSuccessResult(string[] message, T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = true,
            Message = message,
        };
    }
    ResultDto<T> BuildFailedResult(T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = false,
        };
    }
    ResultDto<T> BuildFailedResult(string message, T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = false,
            Message = new[] { message }
        };
    }
    ResultDto<T> BuildFailedResult(string[] message, T data)
    {
        return new ResultDto<T>()
        {
            IsSuccess = false,
            Data = data,
            Message = message
        };
    }
}
