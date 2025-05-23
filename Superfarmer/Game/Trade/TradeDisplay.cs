using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;

namespace Superfarmer.Game.Trade;

public class TradeDisplay
{
    public static void DisplayTrade(string traderName, Dictionary<Models.ITradable, int> offer, Dictionary<Models.ITradable, int> request)
    {
        // Header

        Console.WriteLine($"| {traderName} offers | Request |");
        Console.WriteLine("|-------------------|---------|");

        var allItems = new List<ITradable>()
        {
            new Rabbit(),
            new Sheep(),
            new Pig(),
            new Cow(),
            new Horse(),
            new SmallDog(),
            new BigDog()
        };

        foreach (var item in allItems)
        {
            var offerAmount = offer.TryGetValue(item, out var o) ? o : 0;
            var requestAmount = request.TryGetValue(item, out var r) ? r : 0;
            Console.WriteLine($"| {item.Name,-17} | {requestAmount,7} |");
        }
    }

    public static void DisplayTraders(List<ITrader> traders)
    {
        if (traders == null || traders.Count == 0)
        {
            Console.WriteLine("No traders available.");
            return;
        }

        var allItems = new List<ITradable>()
        {
            new Rabbit(),
            new Sheep(),
            new Pig(),
            new Cow(),
            new Horse(),
            new SmallDog(),
            new BigDog()
        };

        Console.Write("| Trader Name         ");
        foreach (var item in allItems)
        {
            Console.Write($"| {item.Name,-10} ");
        }
        Console.WriteLine("|");

        Console.Write("|---------------------");
        foreach (var item in allItems)
        {
            Console.Write("|------------");
        }
        Console.WriteLine("|");

        int i = 1;
        foreach (var trader in traders)
        {
            Console.Write($"| {i}. {trader.Name,-18}");
            i++;
            var tradable = trader.GetTradable();
            foreach (var item in allItems)
            {
                int amount = tradable.TryGetValue(item, out var value) ? value : 0;
                Console.Write($"| {amount,10} ");
            }
            Console.WriteLine("|");
        }
    }
}
