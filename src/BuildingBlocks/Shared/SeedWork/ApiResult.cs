using System.Text.Json.Serialization;

namespace Shared.SeedWork;

public class ApiResult<T>
{

    [JsonConstructor]
    public ApiResult(bool isSuccessed, string message = null)
    {
        Message = message;
        IsSuccessed = isSuccessed;  
    }

    public ApiResult(bool isSuccessed, T data, string message = null)
    {
        Message = message;
        IsSuccessed = isSuccessed;
        Data = data;
    }

    public string Message { get; set; }
    public bool IsSuccessed { get; set; }
    public T Data { get; set; }
}


