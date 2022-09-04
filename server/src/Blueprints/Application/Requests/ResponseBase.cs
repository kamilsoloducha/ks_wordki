namespace Application.Requests;

public class ResponseBase<TResponse>
{
    public TResponse Response { get; set; }
    public string Error { get; set; }
    public bool IsCorrect => Response is not null;

    public static ResponseBase<TResponse> Create(TResponse response)
        => new ResponseBase<TResponse>() { Response = response };

}