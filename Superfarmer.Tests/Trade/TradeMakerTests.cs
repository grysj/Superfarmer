using System;
using System.Collections.Generic;
using System.IO;
using Superfarmer.Game.Models;
using Superfarmer.Game.Trade;
using NUnit.Framework;
using Superfarmer.Game.Models.FarmAnimals;

namespace Superfarmer.Tests.Trade
{
    [TestFixture]
    public class TradeMakerTests
    {
        private TextReader _originalIn;
        private TextWriter _originalOut;

        [SetUp]
        public void SetUp()
        {
            _originalIn = Console.In;
            _originalOut = Console.Out;
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetIn(_originalIn);
            Console.SetOut(_originalOut);
        }

        [Test]
        public void MakeOffer_ReturnsCorrectOffer_WhenValidInputProvided()
        {
            var input = string.Join(Environment.NewLine, new[] { "1", "0", "2", "0", "0", "0", "3" });
            var stringReader = new StringReader(input);
            Console.SetIn(stringReader);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            var offer = TradeMaker.MakeOffer();

            // Assert
            Assert.NotNull(offer);
            Assert.That(offer.Count, Is.EqualTo(3));

            // Check that the correct animals and counts are present
            Assert.That(offer, Has.Some.Matches<KeyValuePair<ITradable, int>>(kvp => kvp.Key.Name == "Rabbit" && kvp.Value == 1));
            Assert.That(offer, Has.Some.Matches<KeyValuePair<ITradable, int>>(kvp => kvp.Key.Name == "Cow" && kvp.Value == 2));
            Assert.That(offer, Has.Some.Matches<KeyValuePair<ITradable, int>>(kvp => kvp.Key.Name == "BigDog" && kvp.Value == 3));
        }

        [Test]
        public void MakeOffer_IgnoresInvalidInput_AndPromptsAgain()
        {
            // Arrange: invalid input for Rabbit, then valid; rest are zeros
            var input = string.Join(Environment.NewLine, new[] { "abc", "-1", "2", "0", "0", "0", "0", "0", "0" });
            var stringReader = new StringReader(input);
            Console.SetIn(stringReader);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            var offer = TradeMaker.MakeOffer();

            // Assert
            Assert.NotNull(offer);
            Assert.That(offer.Count, Is.EqualTo(1));
            Assert.That(offer, Has.Some.Matches<KeyValuePair<ITradable, int>>(kvp => kvp.Key.Name == "Rabbit" && kvp.Value == 2));

            // Check that error message was printed for invalid input
            var output = stringWriter.ToString();
            StringAssert.Contains("Invalid input", output);
        }
    }
}