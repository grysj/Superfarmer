using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models;

namespace Superfarmer.Game.Dice;


public abstract class Dice : IDice
{
    private static readonly Random _random = new Random();
    protected List<IAnimal> animals;

    public Dice()
    {
    }

    public IAnimal Roll()
    {
        var rolled = _random.Next(12);
        return animals[rolled];
    }

    public abstract void Initialize();
}



