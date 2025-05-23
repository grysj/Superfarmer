using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models.SpecialAnimals;


public abstract class Dog : ITradable
{
    public abstract int Value { get;  }

    public abstract string Name { get; }

    public Dog() : base()
    {
    }

}
