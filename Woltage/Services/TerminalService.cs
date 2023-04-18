using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woltage.Models;

namespace Woltage.Services
{

    public class TerminalService
    {
        private IOService _ioService { get; set; }

        //add proper DI
        public TerminalService()
        {
            if(_ioService == null)
                _ioService = new IOService();
        }

        public void DisplayRestaurants(List<RestaurantResultModel> restaurants)
        {
            Console.WriteLine(" ---------------------------------------------------------------------------------------------- ");
            Console.WriteLine("|             Restaurant             |                 Rettens navn                | pris (kr) |");
            Console.WriteLine(" ---------------------------------------------------------------------------------------------- ");

            foreach (var restaurant in restaurants)
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
        }

        public void Print(string path, ConsoleColor color = ConsoleColor.Gray)
        {
            var logoLines = _ioService.ReadLinesFromFile(path);
            Console.ForegroundColor = color;

            foreach (var line in logoLines)
            {
                Console.WriteLine(line);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
