using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models.FarmAnimals;


public abstract class FarmAnimal : ITradable
{
    public abstract int Value { get; }

    public abstract string Name { get; }


    protected FarmAnimal()
    {
    }


}
