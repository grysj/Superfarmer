using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;

namespace Superfarmer.Game.Trade;


public class TradeMaker
{

    public static Dictionary<ITradable, int> MakeOffer()
    {
        Dictionary<ITradable, int> offer = new Dictionary<ITradable, int>();
        List<ITradable> tradables = new List<ITradable>
        {
            new Rabbit(),
            new Sheep(),
            new Cow(),
            new Horse(),
            new Pig(),
            new SmallDog(),
            new BigDog()
        };

        Console.WriteLine("Enter the number of each animal to offer for trade:");
        foreach (var tradable in tradables)
        {
            int count = 0;
            while (true)
            {
                Console.Write($"{tradable.Name}: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out count) && count >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a non-negative integer.");
            }
            if (count > 0)
            {
                offer[tradable] = count;
            }
        }
        return offer;
    }



}
