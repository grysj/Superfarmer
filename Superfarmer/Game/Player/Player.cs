using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;
using Superfarmer.Game.Dice;
using Superfarmer.Game.Trade;

namespace Superfarmer.Game.Player;

public abstract class Player : IPlayer
{
    public string Name { get; set; }
    public Dictionary<FarmAnimal, int> Inventory { get; }
    public Dictionary<string, int> Dogs { get; set; }

    protected Dice.Dice BlueDice;
    protected Dice.Dice RedDice;
    protected readonly Game Game;

    public Player(Dice.Dice redDice, Dice.Dice blueDice, Game game)
    {
        Name = "Player";
        this.RedDice = redDice;
        this.BlueDice = blueDice;
        this.Game = game;
        Inventory = new Dictionary<FarmAnimal, int>
        {
            { new Rabbit(), 1 },
            { new Sheep(), 0 },
            { new Cow(), 0 },
            { new Horse(), 0 },
            { new Pig(), 0 }
        };
        Dogs = new Dictionary<string, int>
        {
            { "Small", 0 },
            { "Big", 0 }
        };
    }

    public virtual bool DecideTrade(ITrader trader, Dictionary<ITradable, int> offer,
        Dictionary<ITradable, int> request)
    {
        var isValid = TradeValidator.ValidateTrade(offer, request);

        foreach (var item in offer)
        {
            var farmAnimal = item.Key as FarmAnimal;
            if (farmAnimal == null || item.Value > Inventory[farmAnimal])
            {
                return false;
            }
        }

        return true;
    }

    public void Exchange(Dictionary<ITradable, int> changes)
    {
        foreach (var change in changes)
        {
            if (change.Key is FarmAnimal farmAnimal)
            {
                Inventory[farmAnimal] += change.Value;
            }
            else
            {
                throw new InvalidOperationException("Only FarmAnimal types can be exchanged.");
            }
        }
    }

    public virtual void TakeTurn()
    {
        TradeDisplay.DisplayTraders(new List<ITrader>(){this});
    }


    public void RollDice()
    {
        Console.WriteLine($"{Name} is rolling the dice...");
        IAnimal firstRoll = RedDice.Roll();
        IAnimal secondRoll = BlueDice.Roll();

        Console.WriteLine($"Rolled: {firstRoll.Name} and {secondRoll.Name}");
        List<IAnimal> animalsToAdd = new List<IAnimal>();
        bool isWolf = firstRoll.Name.Equals("Wolf") ? true : false;

        if (isWolf == false)
        {
            animalsToAdd.Add(firstRoll);
        }

        bool isFox = secondRoll.Name.Equals("Fox") ? true : false;
        if (isFox == false)
        {
            animalsToAdd.Add(secondRoll);
        }

        if (firstRoll.Name.Equals(secondRoll.Name))
        {
            int toAdd = (Inventory.GetValueOrDefault(firstRoll as FarmAnimal, 0) + 2)/2;

            Inventory[firstRoll as FarmAnimal] += Game.Ranch.GetFromRanch(firstRoll as ITradable, toAdd);
            return;
        }


        if (animalsToAdd.Count >= 1)
        {
            foreach (var animal in animalsToAdd)
            {
                var toAdd = (Inventory.GetValueOrDefault(animal as FarmAnimal, 0) + 1) / 2;
                if (toAdd > 0)
                {
                    Inventory[animal as FarmAnimal] += Game.Ranch.GetFromRanch(animal as FarmAnimal, toAdd);
                }
            }
        }

        if (isWolf)
        {
            HandleWolf();
        }
        if (isFox)
        {
            HandleFox();
        }

    }

    private void HandleWolf()
    {
        if (Dogs["Big"] > 0)
        {
            Dogs["Big"]--;
            return;
        }

        List<FarmAnimal> animalsToDelete =
        [
            new Sheep(),
            new Cow(),
            new Pig()

        ];

        foreach (var animal in animalsToDelete)
        {
            var toReturn = Inventory[animal];
            if (toReturn > 0)
            {
                Inventory[animal] = 0;
                Game.Ranch.ReturnToRanch(animal, toReturn);
            }
        }

        return;

    }

    private void HandleFox()
    {
        if (Dogs["Small"] > 0)
        {
            Dogs["Small"]--;
            return;
        }

        var toReturn = Inventory[new Rabbit()];
        Game.Ranch.ReturnToRanch(new Rabbit(), toReturn - 1);
        Inventory[new Rabbit()] = 1;
        return;
    }


    public Dictionary<ITradable, int> GetTradable()
    {

        Dictionary<ITradable, int> tradable = new Dictionary<ITradable, int>();
        foreach (var item in Inventory)
        {
            if (item.Value > 0)
            {
                tradable.Add(item.Key, item.Value);
            }
        }
        tradable[new SmallDog()] = Dogs["Small"];
        tradable[new BigDog()] = Dogs["Big"];
        return tradable;

    }



    public bool IsWinner()
    {
        return Inventory[new Horse()] >= 1 && Inventory[new Cow()] >= 1 && Inventory[new Pig()] >= 1 &&
               Inventory[new Sheep()] >= 1 && Inventory[new Rabbit()] >= 1;
    }



}
