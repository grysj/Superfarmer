using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models;
using Superfarmer.Game.Trade;

namespace Superfarmer.Game.Player;


public class HumanPlayer : Player
{
    public HumanPlayer(Dice.Dice redDice, Dice.Dice blueDice, Game game) : base(redDice, blueDice, game)
    {
        Name = "Human Player";
    }
    
    public override bool DecideTrade(ITrader trader, Dictionary<ITradable, int> offer,
        Dictionary<ITradable, int> request)
    {
        var isValid =  base.DecideTrade(trader, offer, request);
        if (!isValid)
        {
            return false;
        }
        TradeDisplay.DisplayTrade(trader.Name, offer, request);
        var accept = TradeDecider();
        if (!accept)
        {
            return false;
        }

        var toMe = TradeDiff.GetTradeDiff(request, offer);
        var toSender = TradeDiff.GetTradeDiff(offer, request);
        trader.Exchange(toSender);
        Exchange(toMe);
        return true;

    }

    public override void TakeTurn()
    {
        base.TakeTurn();
        bool traded = false;
        while (!traded)
        {
            Console.WriteLine("Do you want to trade? (y/n): ");
            var input = Console.ReadLine();
            if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase))
            {
                var traders = Game.GetAllTraders(this);
                TradeDisplay.DisplayTraders(traders);
                Console.WriteLine("Select a trader by entering their number: ");
                var inputTrader = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputTrader))
                {
                    Console.WriteLine("No input provided. Skipping trader selection.");
                    traded = true;
                    break;
                }
                int traderIndex = int.Parse(inputTrader) - 1;
                if (traderIndex < 0 || traderIndex >= traders.Count)
                {
                    Console.WriteLine("Invalid trader selection. Please try again.");
                    continue;
                }
                var trader = traders[traderIndex];
                Console.WriteLine($"You selected {trader.Name}.");
                Console.WriteLine("Make offer what you want to give.");
                var offer = TradeMaker.MakeOffer();
                Console.WriteLine("Make offer what you want to get.");
                var request = TradeMaker.MakeOffer();
                var tradeDone = trader.DecideTrade(this, offer, request);
                if (tradeDone)
                {
                    Console.WriteLine("Trade successful!");
                    traded = true;
                }
                else
                {
                    Console.WriteLine("Trade failed.");
                    continue;
                }

            }
            else if (string.Equals(input, "n", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You chose not to trade this turn.");
                traded = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n': ");
            }
        }
        RollDice();
    }


    private bool TradeDecider()
    {
        Console.WriteLine("Do you want to proceed with this trade? (y/n): ");
        while (true)
        {
            var input = Console.ReadLine();
            if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase))
                return true;
            if (string.Equals(input, "n", StringComparison.OrdinalIgnoreCase))
                return false;
            Console.WriteLine("Invalid input. Please enter 'y' or 'n': ");
        }
    }
}