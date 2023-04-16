using Newtonsoft.Json;
using Woltage.Models;
using Woltage.Services;

namespace Woltage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Woltage v0.5");
            Start();
        }

        public static void Start() 
        {
            RestaurantService restaurantService = new();
            IOService ioService = new();

            Console.WriteLine("Enter search terms separated by comma");
            var terms = Console.ReadLine();

            if (terms.Equals("refresh"))
            {
                RefreshRestaurants(restaurantService, ioService);
            }

            var results = GetRestaurantResults(restaurantService, ioService, terms.Split(','));

            DisplayRestaurants(results);

            Console.WriteLine("Search again? y/n");
            var response = Console.ReadLine();

            if (response.Equals("y"))
            {
                Console.Clear();
                Start();
            }
        }

        private static List<RestaurantResultModel> GetRestaurantResults(RestaurantService restaurantService, IOService ioService, string[] terms)
        {
            var restaurants = ioService.ReadFromFile<List<Restaurant>>("RestaurantsResults.txt");
            var filteredRestaurants = restaurantService.FilterRestaurantItemsByName(restaurants, terms);

            var restaurauntResults = restaurantService.SortRestaurantsByCheapest(filteredRestaurants, 20);

            return restaurauntResults;
        }

        public static void RefreshRestaurants(RestaurantService restaurantService, IOService ioService)
        {
            Console.WriteLine($"Refreshing restaurants.. this will take some time");

            var configs = ioService.ReadFromFile<List<Config>>("config.txt");

            var overview = restaurantService.GetRestaurantsOverview(configs.Find(x => x.Name.Equals("latitude")).Value, configs.Find(x => x.Name.Equals("longitude")).Value);
            ioService.WriteToFile(JsonConvert.SerializeObject(overview), "OverviewResults.txt");

            var restaurants = restaurantService.GetAllRestaurants(overview);

            ioService.WriteToFile(JsonConvert.SerializeObject(restaurants), "RestaurantsResults.txt");

            Console.WriteLine($"Refreshed {restaurants.Count} restaurants!");
            Console.WriteLine($"Restarting Woltage in 10 seconds...");

            Thread.Sleep(5000);

            Console.Clear();
            Start();
        }

        public static void DisplayRestaurants(List<RestaurantResultModel> restaurants)
        {
            //Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine(" ---------------------------------------------------------------------------------------------- ");
            Console.WriteLine("|             Restaurant             |                 Rettens navn                | pris (kr) |");
            Console.WriteLine(" ---------------------------------------------------------------------------------------------- ");

            Random random = new Random();

            foreach (var restaurant in restaurants)
            {
                Thread.Sleep(random.Next(30, 200)); //idk I like it

                if (restaurant == null || restaurant.RestaurantName == null || restaurant.ItemName == null) continue;

                var restaurantName = restaurant.RestaurantName;

                var itemName = restaurant.ItemName;

                for (int i = restaurantName.Length; i < 35; i++)
                    restaurantName += " ";

                for (int k = itemName.Length; k < 43; k++)
                    itemName += " ";

                Console.WriteLine($"| {restaurantName}| {itemName} | {restaurant.ItemPrice} kr.    |");
            }

            //Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}