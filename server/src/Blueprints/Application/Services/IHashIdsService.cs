namespace Application.Services;

public interface IHashIdsService
{
    long GetLongId(string hash);
    bool TryGetLongId(string hash, out long value);
    string GetHash(long id);

}