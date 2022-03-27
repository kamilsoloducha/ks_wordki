namespace Application.Services
{
    public interface IHashIdsService
    {
        long GetLongId(string hash);
        string GetHash(long id);

    }
}