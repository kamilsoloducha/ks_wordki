using System;
using Application.Services;
using HashidsNet;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.HashIds
{
    internal class HashIdsService : IHashIdsService
    {
        private readonly Hashids _hash;
        public HashIdsService(IOptions<HashIdsConfiguration> config)
        {
            _hash = new Hashids(config.Value.Salt, config.Value.MinLength);
        }

        public bool TryGetLongId(string hash, out long value)
            => _hash.TryDecodeSingleLong(hash, out value);

        public string GetHash(long id)
            => _hash.EncodeLong(id);

        public long GetLongId(string hash)
        {
            var ids = _hash.DecodeLong(hash);
            if (ids.Length == 0)
            {
                throw new ArgumentException($"{hash} can not be paresed to long", nameof(hash));
            }
            return ids[0];
        }
    }

    public class TestHashIdsService : IHashIdsService
    {
        public bool TryGetLongId(string hash, out long value)
            => long.TryParse(hash, out value);

        public string GetHash(long id)
            => id.ToString();

        public long GetLongId(string hash)
            => long.Parse(hash);
    }
}