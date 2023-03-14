namespace Infrastructure.Services.HashIds
{
    public class HashIdsConfiguration
    {
        public string Salt { get; init; }
        public int MinLength { get; init; }
    }
}