using System.Net.Http.Headers;

namespace Shared.Dto;

public class ResultDto
{
    public bool IsSuccess { get; private set; }
    public List<string> Message { get; private set; }

    public static ResultDto BuildSuccessResult(string message)
    {
        return new ResultDto()
        {
            IsSuccess = true,
            Message = new List<string>(){message},
        };
    }
    public static ResultDto BuildSuccessResult()
    {
        return new ResultDto()
        {
            IsSuccess = true,
            Message = new List<string>()
        };
    }
    public static ResultDto BuildFailedResult(string message)
    {
        return new ResultDto()
        {
            IsSuccess = false,
            Message = new List<string>() { message },
        };
    }
    public static ResultDto BuildFailedResult(List<string> message)
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
    public List<string> Message { get; private set; }
    private ResultDto()
    {

    }
    public T Data { get; private set; }
    public static ResultDto<T> BuildSuccessResult<T>(T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = true,
            Message = new List<string>()
        };
    }
    public static ResultDto<T> BuildSuccessResult(string message, T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = true,
            Message = new List<string>() { message },
        };
    }
    public static ResultDto<T> BuildSuccessResult(List<string> message, T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = true,
            Message = message,
        };
    }
    public static ResultDto<T> BuildFailedResult()
    {
        return new ResultDto<T>()
        {
            Message = new List<string>() {  },
            IsSuccess = false,
        };
    }
    public static ResultDto<T> BuildFailedResult(string message)
    {
        return new ResultDto<T>()
        {
            Message = new List<string>() { message },
            IsSuccess = false,
        };
    }
    public static ResultDto<T> BuildFailedResult(List<string> message)
    {
        return new ResultDto<T>()
        {
            Message = message,
            IsSuccess = false,
        };
    }
    public static ResultDto<T> BuildFailedResult(string message, T data)
    {
        return new ResultDto<T>()
        {
            Data = data,
            IsSuccess = false,
            Message = new List<string>() { message }
        };
    }
    public static ResultDto<T> BuildFailedResult(List<string> message, T data)
    {
        return new ResultDto<T>()
        {
            IsSuccess = false,
            Data = data,
            Message = message
        };
    }
}
