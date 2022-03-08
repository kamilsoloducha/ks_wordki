namespace Blueprints.Application.Requests
{
    public class ResponseBase<TResponse>
    {
        public TResponse Response { get; private set; }
        public string Error { get; private set; }
        public bool IsCorrect => Response is not null;

        protected ResponseBase() { }

        public static ResponseBase<TResponse> Create(TResponse response)
            => new ResponseBase<TResponse>() { Response = response };

        public static ResponseBase<TResponse> Create(string error)
            => new ResponseBase<TResponse>() { Error = error };
    }
}