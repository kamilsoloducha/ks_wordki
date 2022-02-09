using System;
using Newtonsoft.Json;

namespace Cards.Application.Queries
{
    public class RepeatCount
    {
        public int Count { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public Guid OwnerId { get; set; }
    }
}