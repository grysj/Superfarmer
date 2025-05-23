using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;
namespace Superfarmer.Game.Dice;


public class BlueDice : Dice
{


    public BlueDice() : base()
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
            new Sheep(),
            new Pig(),
            new Cow(),
            new Fox()
        };
    }
}
