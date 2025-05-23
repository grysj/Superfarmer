using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models.FarmAnimals;

namespace Superfarmer.Game.Models.SpecialAnimals;

public class BigDog : Dog
{

    public override string Name => "BigDog";
    public override int Value => 36;

    public BigDog() : base() { }
    public override bool Equals(object? obj) => obj is BigDog;
    public override int GetHashCode() => Name.GetHashCode();
}