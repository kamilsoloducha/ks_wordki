namespace Infrastructure.Services.HashIds;

public class HashIdsConfiguration
{
    public string Salt { get; set; }
    public int MinLength { get; set; }
}