using System.Text.Json.Serialization;

namespace HackerNewsClient
{
    public class HackerStorySummary
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("postedBy")]
        public string? By { get; set; }

        [JsonPropertyName("uri")]
        public string? url { get; set; }

        [JsonPropertyName("time")]
        public string? time { get; set; }

        [JsonPropertyName("score")]
        public int score { get; set; }
      
        [JsonPropertyName("commentCount")]
        public int commentCount { get; set; }


    }
}
