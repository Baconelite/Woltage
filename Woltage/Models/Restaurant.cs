using Newtonsoft.Json;

namespace Woltage.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class RestaurantImage
    {
        [JsonProperty("blurhash")]
        public string? Blurhash { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

    public class RestaurantItem
    {
        [JsonProperty("baseprice")]
        public int Baseprice { get; set; }

        [JsonProperty("deposit")]
        public object? Deposit { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("lowest_historical_price")]
        public object? LowestHistoricalPrice { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("unit_info")]
        public object? UnitInfo { get; set; }

        [JsonProperty("unit_price")]
        public object? UnitPrice { get; set; }

        [JsonProperty("vat_percentage")]
        public int VatPercentage { get; set; }

        [JsonProperty("wolt_plus_only")]
        public bool WoltPlusOnly { get; set; }
    }

    public class Restaurant
    { 
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("items")]
        public List<RestaurantItem>? Items { get; set; }


        [JsonProperty("name")]
        public string? Name { get; set; }
    }

}
