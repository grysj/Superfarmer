using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models.FarmAnimals;

public class Sheep : FarmAnimal
{
    public override int Value => 6;
    public override string Name => "Sheep";
    public Sheep() : base()
    {
    }

    public override bool Equals(object? obj) => obj is Sheep;
    public override int GetHashCode() => Name.GetHashCode();
}