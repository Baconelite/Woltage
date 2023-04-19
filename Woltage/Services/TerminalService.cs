using Woltage.Models;
using System;

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
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|             Restaurant             |                 Rettens navn                |   pris   |  Lev. Pris  |");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------------");

            foreach (var restaurant in restaurants)
            {
                if (restaurant == null || restaurant.RestaurantName == null || restaurant.ItemName == null) continue;

                var restaurantName = restaurant.RestaurantName;

                var itemName = restaurant.ItemName;

                var itemPrice = restaurant.ItemPrice.ToString() + " kr.";

                var deliveryPrice = restaurant.DeliveryPrice.ToString() + " kr.";

                if(restaurantName.Length > 34)
                {
                    restaurantName = string.Concat(restaurantName.AsSpan(0, 32), ".. ");
                }
                else
                {
                    for (int i = restaurantName.Length; i < 35; i++)
                        restaurantName += " ";
                }

                if(itemName.Length > 42)
                {
                    itemName = string.Concat(itemName.AsSpan(0, 40), ".. ");
                }
                else
                {
                    for (int j = itemName.Length; j < 43; j++)
                        itemName += " ";
                }

                for (int k = itemPrice.Length; k < 8; k++)
                    itemPrice += " ";

                for(int l = deliveryPrice.Length; l < 9; l++)
                    deliveryPrice += " ";

                Console.WriteLine($"| {restaurantName}| {itemName} |  {itemPrice}|    {deliveryPrice}|");
            }
            Console.WriteLine();
            Console.WriteLine($"{restaurants.Count} Results!");
            Console.WriteLine();
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
