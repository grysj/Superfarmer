using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models.FarmAnimals;

namespace Superfarmer.Game.Models.SpecialAnimals;


public class Wolf : IAnimal
{
    public string Name => "Wolf";

    public Wolf()
    {
    }

    public override bool Equals(object? obj) => obj is Wolf;
    public override int GetHashCode() => Name.GetHashCode();
}
