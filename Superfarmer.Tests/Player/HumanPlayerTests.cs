using NUnit.Framework;
using Superfarmer.Game.Player;
using Superfarmer.Game.Models;
using Superfarmer.Game.Trade;
using System.Collections.Generic;
using System.IO;
using System;

namespace Superfarmer.Tests.Player
{
    // Dummy implementations for dependencies
    public class DummyTradable : ITradable
    {
        public string Name { get; }
        public int Value { get; }
        public DummyTradable(string name, int value = 1) { Name = name; Value = value; }
        public override bool Equals(object obj) => obj is DummyTradable d && Name == d.Name;
        public override int GetHashCode() => Name.GetHashCode();
    }

    public class DummyTrader : ITrader
    {
        public string Name { get; set; } = "DummyTrader";
        public bool DecideTradeReturn { get; set; } = true;
        public Dictionary<ITradable, int> LastOffer { get; private set; }
        public Dictionary<ITradable, int> LastRequest { get; private set; }
        public bool DecideTrade(ITrader trader, Dictionary<ITradable, int> offer, Dictionary<ITradable, int> request)
        {
            LastOffer = offer;
            LastRequest = request;
            return DecideTradeReturn;
        }
        public void Exchange(Dictionary<ITradable, int> changes) { }
        public Dictionary<ITradable, int> GetTradable() => new();
    }

    public class DummyGame : Superfarmer.Game.Game
    {
        public List<ITrader> Traders { get; set; } = new();
        // Remove 'override' since base method is not virtual/abstract/override
        public new List<ITrader> GetAllTraders(ITrader except)
        {
            return Traders;
        }
    }

    public class DummyDice : Superfarmer.Game.Dice.Dice
    {
        public DummyDice() : base() { }
        public override void Initialize()
        {
        }
    }

    [TestFixture]
    public class HumanPlayerTests
    {
        [Test]
        public void DecideTrade_ReturnsFalse_WhenBaseReturnsFalse()
        {
            var game = new DummyGame();
            var player = new HumanPlayer(new DummyDice(), new DummyDice(), game);
            var result = player.DecideTrade(new DummyTrader(), null, null);
            Assert.IsFalse(result);
        }

        [Test]
        public void DecideTrade_ExecutesTrade_WhenAccepted()
        {
            var game = new DummyGame();
            var player = new HumanPlayer(new DummyDice(), new DummyDice(), game);
            var trader = new DummyTrader();
            var offer = new Dictionary<ITradable, int> { { new DummyTradable("Rabbit"), 1 } };
            var request = new Dictionary<ITradable, int> { { new DummyTradable("Sheep"), 2 } };

            var input = new StringReader("y" + Environment.NewLine);
            Console.SetIn(input);

            var result = player.DecideTrade(trader, offer, request);

            Assert.IsTrue(result);
            Assert.AreEqual(offer, trader.LastRequest);
        }

        [Test]
        public void DecideTrade_ReturnsFalse_WhenUserDeclines()
        {
            var game = new DummyGame();
            var player = new HumanPlayer(new DummyDice(), new DummyDice(), game);
            var trader = new DummyTrader();
            var offer = new Dictionary<ITradable, int> { { new DummyTradable("Rabbit"), 1 } };
            var request = new Dictionary<ITradable, int> { { new DummyTradable("Sheep"), 2 } };

            var input = new StringReader("n" + Environment.NewLine);
            Console.SetIn(input);

            var result = player.DecideTrade(trader, offer, request);

            Assert.IsFalse(result);
        }

        [Test]
        public void TakeTurn_HandlesNoTradeInput()
        {
            var game = new DummyGame();
            var player = new HumanPlayer(new DummyDice(), new DummyDice(), game);
            var input = new StringReader("n" + Environment.NewLine);
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            player.TakeTurn();

            StringAssert.Contains("You chose not to trade this turn.", output.ToString());
        }

        [Test]
        public void TakeTurn_HandlesInvalidInput_ThenNoTrade()
        {
            var game = new DummyGame();
            var player = new HumanPlayer(new DummyDice(), new DummyDice(), game);

            var input = new StringReader("maybe" + Environment.NewLine + "n" + Environment.NewLine);
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            player.TakeTurn();

            StringAssert.Contains("Invalid input", output.ToString());
            StringAssert.Contains("You chose not to trade this turn.", output.ToString());
        }
    }
}