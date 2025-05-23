using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models.FarmAnimals;

public class Pig : FarmAnimal
{
    public override int Value => 12;
    public override string Name => "Pig";
    public Pig()
    {

    }

    public override bool Equals(object? obj) => obj is Pig;
    public override int GetHashCode() => Name.GetHashCode();
}