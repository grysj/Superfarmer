using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Dice;
using Superfarmer.Game.Models;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;

namespace Superfarmer.Game.Dice;

public class RedDice : Dice
{
    public RedDice() : base()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.animals = new List<IAnimal>()
        {
            new Rabbit(),
            new Rabbit(),
            new Rabbit(),
            new Rabbit(),
            new Rabbit(),
            new Rabbit(),
            new Sheep(),
            new Sheep(),
            new Pig(),
            new Pig(),
            new Horse(),
            new Wolf()
        };
    }
}