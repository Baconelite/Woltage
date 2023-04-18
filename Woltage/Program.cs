using Newtonsoft.Json;
using Woltage.Models;
using Woltage.Services;

namespace Woltage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Start();
        }

        public static void Start() 
        {
            RestaurantService restaurantService = new();
            TerminalService terminalService = new();

            terminalService.Print("assets/logo.txt", ConsoleColor.DarkYellow);
            //terminalService.Print("assets/woltage.txt", ConsoleColor.DarkBlue);
            terminalService.Print("assets/search.txt");

            var terms = Console.ReadLine();

            if (terms.Equals("refresh"))
            {
                RefreshRestaurants(restaurantService);
            }

            var results = restaurantService.GetRestaurantResults(terms.Split(','));

            terminalService.DisplayRestaurants(results);

            Console.WriteLine("Search again? y/n");
            var response = Console.ReadLine();

            if (response.Equals("y"))
            {
                Console.Clear();
                Start();
            }
        }

        public static void RefreshRestaurants(RestaurantService restaurantService)
        {
            Console.WriteLine($"Refreshing restaurants.. this will take some time");

            var count = restaurantService.RefreshRestaurants();

            Console.WriteLine($"Refreshed {count} restaurants!");
            Console.WriteLine($"Restarting Woltage in 5 seconds...");

            Thread.Sleep(5000);

            Console.Clear();
            Start();
        }
    }
}