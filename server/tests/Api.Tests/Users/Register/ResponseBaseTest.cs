namespace Api.Tests.Users
{
    public class ResponseBaseTest<T>
    {
        public T Response { get; set; }
        public string Error { get; set; }
        public bool IsCorrect { get; set; }
    }
}