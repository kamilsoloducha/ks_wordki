namespace Application.Requests;

public class ResponseBase<TResponse>
{
    public TResponse Response { get; private init; }
    public string Error { get; private init; } = string.Empty;
    public bool IsCorrect => string.IsNullOrEmpty(Error);

    public static ResponseBase<TResponse> Create(TResponse response)
        => new() { Response = response };

    public static ResponseBase<TResponse> CreateError(string message)
        => new() { Error = message };
}