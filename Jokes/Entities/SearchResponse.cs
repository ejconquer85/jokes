using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jokes.Entities
{
    public class SearchResponse
    {
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("next_page")]
        public int NextPage { get; set; }
        [JsonProperty("previous_page")]
        public int PreviousPage { get; set; }
        [JsonProperty("results")]
        public List<JokesResult> Results { get; set; }
    }

    public class JokesResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("joke")]
        public string Joke { get; set; }
    }
}