using Superfarmer.Game.Models.FarmAnimals;

namespace Superfarmer.Game.Models.SpecialAnimals;


public class Fox : IAnimal
{
    public string Name => "Fox";

    public Fox()
    {

    }
    public override bool Equals(object? obj) => obj is Fox;
    public override int GetHashCode() => Name.GetHashCode();
}