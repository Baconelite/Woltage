using Newtonsoft.Json;
using Woltage.Models;
using Woltage.Services;

namespace Woltage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Woltage v 0.5");

            //Console.WriteLine("Refresh restaurants? y/n");

            //var refreshRestaurantsInput = Console.ReadLine();

            RestaurantService restaurantService = new RestaurantService();
            IOService ioService = new IOService();

            RestaurantOverview overview = null;
            List<Restaurant> restaurants = null;

            //if (!string.IsNullOrEmpty(refreshRestaurantsInput) && (refreshRestaurantsInput.Equals("y") || refreshRestaurantsInput.Equals("yes")))
            //{
            //    overview = restaurantService.GetRestaurantsOverview("55.68311888415391", "12.53212172538042");
            //    ioService.WriteToFile(JsonConvert.SerializeObject(overview), "OverviewResults.txt");

            //    restaurants = restaurantService.GetAllRestaurants(overview);

            //    ioService.WriteToFile(JsonConvert.SerializeObject(restaurants), "RestaurantsResults.txt");
            //}

            //if (overview == null)
            //    overview = ioService.ReadFromFile<RestaurantOverview>("OverviewResults.txt");

            //var filteredOverviewByTag = restaurantService.FilterRestaurantOverviewByTag(overviewFromFile, new string[] { "kebab", "pizza" });

            if (restaurants == null)
                restaurants = ioService.ReadFromFile<List<Restaurant>>("RestaurantsResults.txt");

            Console.WriteLine("Enter search terms separated by comma");
            var terms = Console.ReadLine();

            var filteredRestaurants = restaurantService.FilterRestaurantItemsByName(restaurants, terms.Split(','));

            Console.WriteLine(" ---------------------------------------------------------------------------------------------- ");
            Console.WriteLine("|             Restaurant             |                 Rettens navn                | pris (kr) |");
            Console.WriteLine(" ---------------------------------------------------------------------------------------------- ");
            Console.WriteLine("Sorted by cheapest");

            var restaurauntResults = restaurantService.SortRestaurantsByCheapest(filteredRestaurants, 20);

            foreach (var restaurant in restaurauntResults)
            {
                if (restaurant == null || restaurant.RestaurantName == null || restaurant.ItemName == null) continue;

                var restaurantName = restaurant.RestaurantName;

                var itemName = restaurant.ItemName;

                for (int i = restaurantName.Length; i < 35; i++)
                    restaurantName += " ";

                for (int k = itemName.Length; k < 43; k++)
                    itemName += " ";

                Console.WriteLine($"| {restaurantName}| {itemName} | {restaurant.ItemPrice} kr.    |");
            }

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}