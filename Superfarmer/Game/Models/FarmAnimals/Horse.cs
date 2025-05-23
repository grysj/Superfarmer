using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models.FarmAnimals;

public class Horse : FarmAnimal
{
    public override int Value => 72;
    public override string Name => "Horse";
    public Horse() : base()
    {
    }

    public override bool Equals(object? obj) => obj is Horse;
    public override int GetHashCode() => Name.GetHashCode();
}
