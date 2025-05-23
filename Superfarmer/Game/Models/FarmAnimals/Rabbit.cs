using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models.FarmAnimals;

public class Rabbit : FarmAnimal
{
    public override int Value => 1;
    public override string Name => "Rabbit";

    
    public Rabbit(): base()
    {
    }

    public override bool Equals(object? obj) => obj is Rabbit;
    public override int GetHashCode() => Name.GetHashCode();
}
