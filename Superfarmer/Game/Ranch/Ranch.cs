using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;
using Superfarmer.Game.Models;
using Superfarmer.Game.Trade;
namespace Superfarmer.Game.Ranch;


public class Ranch : ITrader
{

    public Dictionary<Models.ITradable, int> Inventory { get; } = new Dictionary<Models.ITradable, int>
    {
        {new Rabbit(), 60 },
        {new Sheep(), 24},
        {new Pig(), 20},
        {new Cow(), 12},
        {new Horse(), 6},
        {new SmallDog(), 4},
        {new BigDog(), 2},
    };

    public string Name => "Ranch";

    public Ranch()
    {

    }

    public void Exchange(Dictionary<ITradable, int> changes)
    {
        foreach (var item in changes)
        {
                Inventory[item.Key] += item.Value;
        }
    }

    public bool DecideTrade(ITrader trader, Dictionary<ITradable, int> offer, Dictionary<ITradable, int> request)
    {
        bool isValid = TradeValidator.ValidateTrade(offer, request);
        if (!isValid)
        {
            return false;
        }

        Dictionary<ITradable, int> requestMax = new Dictionary<ITradable, int>();
        foreach (var item in request)
        {
            int inventoryCount = Inventory.TryGetValue(item.Key, out int count) ? count : 0;
            if (inventoryCount < item.Value)
            {
               requestMax[item.Key] = inventoryCount;
            }
            else
            {
                requestMax[item.Key] = item.Value;
            }
        }

        Dictionary<ITradable, int> toSender = TradeDiff.GetTradeDiff(requestMax, request);
        Dictionary<ITradable, int> toReceiver = TradeDiff.GetTradeDiff(offer, requestMax);

        trader.Exchange(toSender);
        Exchange(toReceiver);
        return true;
    }

    public Dictionary<ITradable, int> GetTradable()
    {
        // Return a shallow copy of the inventory with only items that have at least 1 in stock
        Dictionary<ITradable, int> tradable = new Dictionary<ITradable, int>();
        foreach (var item in Inventory)
        {

            tradable[item.Key] = item.Value;

        }
        return tradable;
    }

    public int GetFromRanch(ITradable animal, int count)
    {
        int toGet = Inventory[animal];
        if (toGet < count)
        {
            Inventory[animal] -= toGet;
            return toGet;
        }

        Inventory[animal] -= count;
        return count;

    }


    public void ReturnToRanch(ITradable animal, int count)
    {
        if (Inventory.ContainsKey(animal))
        {
            Inventory[animal] += count;
        }

    }



}
