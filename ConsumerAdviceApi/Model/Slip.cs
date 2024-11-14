using System.Text.Json.Serialization;

namespace ConsumerAdviceApi.Models
{
    public class Slip
    {
        [JsonPropertyName("slip")]
        public SlipDetail? SlipDetail { get; set; }
    }

    public class SlipDetail
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("advice")]
        public string? Advice { get; set; }
    }
}
