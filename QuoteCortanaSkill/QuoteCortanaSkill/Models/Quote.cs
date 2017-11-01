using Newtonsoft.Json;

namespace QuoteCortanaSkill.Models
{
    public class Quote
    {
        [JsonProperty("quoteAuthor")]
        public string QuoteAuthor { get; set; }

        [JsonProperty("quoteLink")]
        public string QuoteLink { get; set; }

        [JsonProperty("quoteText")]
        public string QuoteText { get; set; }

        [JsonProperty("senderLink")]
        public string SenderLink { get; set; }

        [JsonProperty("senderName")]
        public string SenderName { get; set; }
    }
}
