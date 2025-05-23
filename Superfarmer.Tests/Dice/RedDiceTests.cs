using NUnit.Framework;
using Superfarmer.Game.Dice;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;
using Superfarmer.Game.Models;

namespace Superfarmer.Tests.Game.Dice
{
    [TestFixture]
    public class RedDiceTests
    {
        [Test]
        public void Constructor_InitializesAnimalsArrayCorrectly()
        {
            // Arrange & Act
            var dice = new RedDice();

            // Assert
            var animalsField = typeof(Superfarmer.Game.Dice.Dice).GetField("animals", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var animals = (IAnimal[])animalsField.GetValue(dice);

            Assert.That(animals, Is.Not.Null);
            Assert.That(animals.Length, Is.EqualTo(12));
            Assert.That(animals.Count(a => a is Rabbit), Is.EqualTo(6));
            Assert.That(animals.Count(a => a is Sheep), Is.EqualTo(2));
            Assert.That(animals.Count(a => a is Pig), Is.EqualTo(2));
            Assert.That(animals.Count(a => a is Horse), Is.EqualTo(1));
            Assert.That(animals.Count(a => a is Wolf), Is.EqualTo(1));
        }

        [Test]
        public void Roll_ReturnsAnimalFromAnimalsArray()
        {
            // Arrange
            var dice = new RedDice();
            var animalsField = typeof(Superfarmer.Game.Dice.Dice).GetField("animals", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var animals = (IAnimal[])animalsField.GetValue(dice);

            // Act
            var result = dice.Roll();

            // Assert
            Assert.That(animals, Does.Contain(result));
        }

        [Test]
        public void Initialize_ResetsAnimalsArray()
        {
            // Arrange
            var dice = new RedDice();
            var animalsField = typeof(Superfarmer.Game.Dice.Dice).GetField("animals", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var originalAnimals = ((IAnimal[])animalsField.GetValue(dice)).ToArray();

            // Act
            dice.Initialize();
            var newAnimals = (IAnimal[])animalsField.GetValue(dice);

            // Assert
            Assert.That(newAnimals, Is.Not.Null);
            Assert.That(newAnimals.Length, Is.EqualTo(12));
            Assert.That(newAnimals, Is.Not.SameAs(originalAnimals));
        }
    }
}
