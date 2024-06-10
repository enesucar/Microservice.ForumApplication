namespace Quesify.Web.Models;

public class BaseApiResponse<TData, TError> 
    where TData : class
    where TError : class
{
    public string Title { get; set; }

    public string Detail { get; set; }

    public int Status { get; set; }

    public string Instance { get; set; }

    public string Type { get; set; }

    public TData? Data { get; set; }

    public TError? Errors { get; set; }

    public bool IsSuccess => Status < 400;

    public bool IsFail => !IsSuccess;

    public BaseApiResponse()
    {
        Title = null!;
        Detail = null!;
        Instance = null!;
        Type = null!;
        Data = null;
        Errors = null;
    }
}
