namespace Infrastructure.Services
{
    public class HashIdsConfiguration
    {
        public string Salt { get; set; }
        public int MinLength { get; set; }
    }
}