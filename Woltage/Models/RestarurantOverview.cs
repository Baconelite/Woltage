using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woltage.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Badge
    {
        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("variant")]
        public string? Variant { get; set; }
    }

    public class Created
    {
        [JsonProperty("$date")]
        public long Date { get; set; }
    }

    public class EndOfSection
    {
        [JsonProperty("link")]
        public Link? Link { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public class Filter
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public class Filtering
    {
        [JsonProperty("filters")]
        public List<Filter>? Filters { get; set; }
    }

    public class OverviewImage
    {
        [JsonProperty("blurhash")]
        public string? Blurhash { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

    public class OverviewItem
    {
        [JsonProperty("image")]
        public OverviewImage? Image { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("filtering")]
        public Filtering? Filtering { get; set; }

        [JsonProperty("sorting")]
        public Sorting? Sorting { get; set; }

        [JsonProperty("venue")]
        public Venue Venue { get; set; }
    }

    public class Link
    {
        [JsonProperty("target")]
        public string? Target { get; set; }

        [JsonProperty("target_sort")]
        public string? TargetSort { get; set; }

        [JsonProperty("target_title")]
        public string? TargetTitle { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("selected_delivery_method")]
        public string? SelectedDeliveryMethod { get; set; }

        [JsonProperty("venue_mainimage_blurhash")]
        public string? VenueMainimageBlurhash { get; set; }
    }

    public class OverviewRating
    {
        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }

    public class RestaurantOverview
    { 
        [JsonProperty("filtering")]
        public Filtering? Filtering { get; set; }

        [JsonProperty("sections")]
        public List<Section>? Sections { get; set; }
    }

    public class Section
    {
        [JsonProperty("items")]
        public List<OverviewItem>? Items { get; set; }
    }

    public class Sortable
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public class Sorting
    {
        [JsonProperty("sortables")]
        public List<Sortable> Sortables { get; set; }
    }

    public class Venue
    {

        [JsonProperty("delivers")]
        public bool Delivers { get; set; }

        [JsonProperty("delivery_price")]
        public string? DeliveryPrice { get; set; }

        [JsonProperty("delivery_price_highlight")]
        public bool DeliveryPriceHighlight { get; set; }

        [JsonProperty("delivery_price_int")]
        public int DeliveryPriceInt { get; set; }

        [JsonProperty("distance")]
        public string? Distance { get; set; }

        [JsonProperty("estimate")]
        public int Estimate { get; set; }

        [JsonProperty("estimate_range")]
        public string? EstimateRange { get; set; }

        [JsonProperty("franchise")]
        public string? Franchise { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("location")]
        public List<double> Location { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("price_range")]
        public int PriceRange { get; set; }

        [JsonProperty("product_line")]
        public string? ProductLine { get; set; }

        [JsonProperty("show_wolt_plus")]
        public bool ShowWoltPlus { get; set; }

        [JsonProperty("slug")]
        public string? Slug { get; set; }

        [JsonProperty("tags")]
        public List<string?> Tags { get; set; }
    }
}
