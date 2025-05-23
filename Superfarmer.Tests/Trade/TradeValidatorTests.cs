
    using Moq;
    using Superfarmer.Game;
    using Superfarmer.Game.Trade;
    using NUnit.Framework;
    using Superfarmer;
    using Superfarmer.Game.Models;

    namespace Superfarmer.Tests.Trade
    {
        public class TradeValidatorTests
        {
            private Mock<ITradable> CreateTradable(int value)
            {
                var mock = new Mock<ITradable>();
                mock.Setup(t => t.Value).Returns(value);
                return mock;
            }

            [Test]
            public void ValidateTrade_ReturnsFalse_WhenOfferIsNull()
            {
                var result = TradeValidator.ValidateTrade(null, new Dictionary<ITradable, int>());
                Assert.False(result);
            }

            [Test]
            public void ValidateTrade_ReturnsFalse_WhenRequestIsNull()
            {
                var result = TradeValidator.ValidateTrade(new Dictionary<ITradable, int>(), null);
                Assert.False(result);
            }

            [Test]
            public void ValidateTrade_ReturnsFalse_WhenOfferCountNotOne()
            {
                var tradable = CreateTradable(6).Object;
                var offer = new Dictionary<ITradable, int> { { tradable, 2 } };
                var request = new Dictionary<ITradable, int> { { tradable, 1 } };
                var result = TradeValidator.ValidateTrade(offer, request);
                Assert.False(result);
            }

            [Test]
            public void ValidateTrade_ReturnsFalse_WhenRequestCountNotOne()
            {
                var tradable = CreateTradable(6).Object;
                var offer = new Dictionary<ITradable, int> { { tradable, 1 } };
                var request = new Dictionary<ITradable, int> { { tradable, 2 } };
                var result = TradeValidator.ValidateTrade(offer, request);
                Assert.False(result);
            }

            [Test]
            public void ValidateTrade_ReturnsFalse_WhenOfferAndRequestCountNotOne()
            {
                var tradable = CreateTradable(6).Object;
                var offer = new Dictionary<ITradable, int> { { tradable, 2 } };
                var request = new Dictionary<ITradable, int> { { tradable, 2 } };
                var result = TradeValidator.ValidateTrade(offer, request);
                Assert.False(result);
            }

            [Test]
            public void ValidateTrade_ReturnsFalse_WhenOfferValueNotEqualRequestValue()
            {
                var tradable1 = CreateTradable(6).Object;
                var tradable2 = CreateTradable(12).Object;
                var offer = new Dictionary<ITradable, int> { { tradable1, 1 } };
                var request = new Dictionary<ITradable, int> { { tradable2, 1 } };
                var result = TradeValidator.ValidateTrade(offer, request);
                Assert.False(result);
            }

            [Test]
            public void ValidateTrade_ReturnsTrue_WhenValidTrade()
            {
                var tradable1 = CreateTradable(6).Object;
                var tradable2 = CreateTradable(6).Object;
                var offer = new Dictionary<ITradable, int> { { tradable1, 1 } };
                var request = new Dictionary<ITradable, int> { { tradable2, 1 } };
                var result = TradeValidator.ValidateTrade(offer, request);
                Assert.True(result);
            }
        }
    }
