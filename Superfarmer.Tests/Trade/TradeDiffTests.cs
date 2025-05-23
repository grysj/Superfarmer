using NUnit.Framework;
using Superfarmer.Game.Trade;
using System.Collections.Generic;
using Superfarmer.Game.Models;

namespace Superfarmer.Tests.Trade
{
    public class DummyAnimal : ITradable
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public DummyAnimal(string name, int value = 1)
        {
            Name = name;
            Value = value;
        }
        public override bool Equals(object obj)
        {
            return obj is DummyAnimal animal && Name == animal.Name;
        }
        public override int GetHashCode() => Name.GetHashCode();
    }

    [TestFixture]
    public class TradeDiffTests
    {
        [Test]
        public void GetTradeDiff_ReturnsCorrectDiff_WhenKeysOverlap()
        {
            var rabbit = new DummyAnimal("Rabbit");
            var sheep = new DummyAnimal("Sheep");
            var offer = new Dictionary<ITradable, int> { { rabbit, 3 }, { sheep, 2 } };
            var request = new Dictionary<ITradable, int> { { rabbit, 1 }, { sheep, 5 } };

            var diff = TradeDiff.GetTradeDiff(offer, request);

            Assert.That(diff[rabbit], Is.EqualTo(2));
            Assert.That(diff[sheep], Is.EqualTo(-3));
        }

        [Test]
        public void GetTradeDiff_ReturnsCorrectDiff_WhenKeysDoNotOverlap()
        {
            var rabbit = new DummyAnimal("Rabbit");
            var cow = new DummyAnimal("Cow");
            var offer = new Dictionary<ITradable, int> { { rabbit, 2 } };
            var request = new Dictionary<ITradable, int> { { cow, 1 } };

            var diff = TradeDiff.GetTradeDiff(offer, request);

            Assert.That(diff[rabbit], Is.EqualTo(2));
            Assert.That(diff[cow], Is.EqualTo(-1));
        }

        [Test]
        public void GetTradeDiff_ReturnsEmpty_WhenBothDictionariesEmpty()
        {
            var offer = new Dictionary<ITradable, int>();
            var request = new Dictionary<ITradable, int>();

            var diff = TradeDiff.GetTradeDiff(offer, request);

            Assert.That(diff, Is.Empty);
        }

        [Test]
        public void GetTradeDiff_ReturnsOffer_WhenRequestIsEmpty()
        {
            var rabbit = new DummyAnimal("Rabbit");
            var offer = new Dictionary<ITradable, int> { { rabbit, 4 } };
            var request = new Dictionary<ITradable, int>();

            var diff = TradeDiff.GetTradeDiff(offer, request);

            Assert.That(diff[rabbit], Is.EqualTo(4));
        }

        [Test]
        public void GetTradeDiff_ReturnsNegativeRequest_WhenOfferIsEmpty()
        {
            var sheep = new DummyAnimal("Sheep");
            var offer = new Dictionary<ITradable, int>();
            var request = new Dictionary<ITradable, int> { { sheep, 3 } };

            var diff = TradeDiff.GetTradeDiff(offer, request);

            Assert.That(diff[sheep], Is.EqualTo(-3));
        }
    }
}