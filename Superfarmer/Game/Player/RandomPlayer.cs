using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models;
using Superfarmer.Game.Trade;

namespace Superfarmer.Game.Player;

public class RandomPlayer : Player
{
    public RandomPlayer(Dice.Dice redDice, Dice.Dice blueDice, Game game) : base(redDice, blueDice, game)
    {
        Name = "AI_Bot";
    }







    public override void TakeTurn()
    {
        base.TakeTurn();
        RollDice();
    }


    public override bool DecideTrade(ITrader trader, Dictionary<ITradable, int> offer, Dictionary<ITradable, int> request)
    {
        var isValid = base.DecideTrade(trader, offer, request);

        if (!isValid)
        {
            Console.WriteLine($"{Name} rejects the deal");
            return false;
        }

        var shouldAccept = new Random().NextInt64() % 2 == 0;
        if (!shouldAccept)
        {
            Console.WriteLine($"{Name} rejects the deal");
        }

        Console.WriteLine($"{Name} accepts the deal");
        var toMe = TradeDiff.GetTradeDiff(request, offer);
        var toSender = TradeDiff.GetTradeDiff(offer, request);
        trader.Exchange(toSender);
        Exchange(toMe);
        return true;
    }
}
