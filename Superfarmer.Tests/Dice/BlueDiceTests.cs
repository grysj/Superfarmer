using NUnit.Framework;
using Superfarmer.Game.Dice;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;
using Superfarmer.Game.Models;

namespace Superfarmer.Tests.Dice
{
    public class BlueDiceTests
    {
        [Test]
        public void BlueDice_Initialize_SetsCorrectAnimals()
        {
            var dice = new BlueDice();

            var animalsField = typeof(Superfarmer.Game.Dice.Dice).GetField("animals", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var animals = (IAnimal[])animalsField.GetValue(dice);

            Assert.That(animals.Length, Is.EqualTo(12));
            Assert.That(animals.Count(a => a is Rabbit), Is.EqualTo(6));
            Assert.That(animals.Count(a => a is Sheep), Is.EqualTo(3));
            Assert.That(animals.Count(a => a is Pig), Is.EqualTo(1));
            Assert.That(animals.Count(a => a is Cow), Is.EqualTo(1));
            Assert.That(animals.Count(a => a is Wolf), Is.EqualTo(1));
        }

        [Test]
        public void BlueDice_Roll_ReturnsAnimalFromSet()
        {
            var dice = new BlueDice();
            var validTypes = new[] { typeof(Rabbit), typeof(Sheep), typeof(Pig), typeof(Cow), typeof(Wolf) };
            for (int i = 0; i < 20; i++)
            {
                var animal = dice.Roll();
                Assert.That(validTypes, Does.Contain(animal.GetType()));
            }
        }
    }
}
