using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models.SpecialAnimals;

public class SmallDog : Dog
{
    public override int Value => 6;
    public override string Name => "SmallDog";
    public SmallDog() : base()
    {}
    public override bool Equals(object? obj) => obj is SmallDog;
    public override int GetHashCode() => Name.GetHashCode();
}

