using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Woltage.Models;

namespace Woltage.Services
{
    public class RestaurantService
    {

        private CallServerService CallServerService { get; set; }
        private IOService IOService { get; set; }

        public RestaurantService()
        {
            CallServerService = new CallServerService();
            IOService = new IOService();
        }


        public RestaurantOverview GetRestaurantsOverview(string lat, string lon)
        {
            var url = $"https://consumer-api.wolt.com/v1/pages/restaurants?lat={lat}&lon={lon}";

            var result = CallServerService.Get<RestaurantOverview>(url);

            result.Sections[1].Items = result.Sections[1].Items.Where(x => x.Venue.Delivers && x.Venue.Online).ToList();

            return result;
        }

        public Restaurant GetRestaurant(string slug)
        {
            var url = $"https://restaurant-api.wolt.com/v4/venues/slug/{slug}/menu/data?unit_prices=true&show_weighted_items=true&show_subcategories=true";

            return CallServerService.Get<Restaurant>(url);
        }


        public List<Restaurant> GetAllRestaurants(RestaurantOverview overview)
        {
            if (overview.Sections == null || overview.Sections[1]?.Items == null)
                return null;

            List<Restaurant> restaurants = new List<Restaurant>();
            foreach (var overviewItem in overview.Sections[1].Items)
            {
                var slug = overviewItem.Venue.Slug;

                if (string.IsNullOrEmpty(slug))
                    continue;

                var restaurant = GetRestaurant(slug);

                if (restaurant != null)
                    restaurants.Add(restaurant);
            }

            return restaurants;
        }

        public List<Restaurant> FilterRestaurantItemsByName(List<Restaurant> restaurants, string[] tags)
        {
            foreach (var restaurant in restaurants)
            {
                if (restaurant == null || restaurant.Id == null) continue;

                var items = new List<RestaurantItem>();

                foreach (var tag in tags)
                {
                    items.AddRange(restaurant.Items?.Where(y => !string.IsNullOrEmpty(y.Name) && y.Name.ToLower().Contains(tag)));
                }

                restaurant.Items = items;
            }
            return restaurants;
        }

        public RestaurantOverview FilterRestaurantOverviewByTag(RestaurantOverview overview, string[] tags = null)
        {
            if (tags == null)
                return overview;

            if (overview == null || overview.Sections == null || overview.Sections[1] == null || overview.Sections[1].Items == null)
                return null;

            var items = new List<OverviewItem>();

            foreach (var tag in tags)
            {
                items.AddRange(overview.Sections[1].Items.Where(x => x.Venue.Tags.Contains(tag)));
            }

            overview.Sections[1].Items = items;

            return overview;
        }

        public List<RestaurantItem> SortRestaurantItemsByCheapest(List<RestaurantItem> items)
        {
            return items.OrderBy(x => x.Baseprice).ToList();
        }


        public List<RestaurantResultModel> SortRestaurantsByCheapest(List<Restaurant> restaurants, int minimumPrice)
        {
            List<RestaurantResultModel> result = new List<RestaurantResultModel>();
            foreach (var restaurant in restaurants)
            {
                if (restaurant.Items == null || !restaurant.Items.Any())
                    continue;

                var cheapestItem = SortRestaurantItemsByCheapest(restaurant.Items).FirstOrDefault();
                string name = restaurant.Name;
                string itemName = cheapestItem.Name;
                int itemPrice = cheapestItem.Baseprice / 100;

                RestaurantResultModel model = new RestaurantResultModel();
                model.RestaurantName = name;
                model.ItemName = itemName;
                model.ItemPrice = itemPrice;

                if (model.ItemPrice > minimumPrice)
                    result.Add(model);
            }

            return result.OrderBy(x => x.ItemPrice).ToList();
        }

        public int RefreshRestaurants()
        {
            var configs = IOService.ReadFromFile<List<Config>>("configs/config.txt");

            var overview = GetRestaurantsOverview(configs.Find(x => x.Name.Equals("latitude")).Value, configs.Find(x => x.Name.Equals("longitude")).Value);
            IOService.WriteToFile(JsonConvert.SerializeObject(overview), "OverviewResults.txt");

            var restaurants = GetAllRestaurants(overview);
            IOService.WriteToFile(JsonConvert.SerializeObject(restaurants), "RestaurantsResults.txt");

            return restaurants.Count;
        }

        public List<RestaurantResultModel> GetRestaurantResults(string[] terms)
        {
            var restaurants = IOService.ReadFromFile<List<Restaurant>>("RestaurantsResults.txt");
            var filteredRestaurants = FilterRestaurantItemsByName(restaurants, terms);

            var restaurauntResults = SortRestaurantsByCheapest(filteredRestaurants, 20);

            return restaurauntResults;
        }
    }
}
