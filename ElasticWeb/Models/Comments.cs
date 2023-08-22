using System.Text.Json.Serialization;

namespace ElasticWeb.Models
{
    public class Comments
    {
        [JsonPropertyName("postId")]
        public long PostId { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
