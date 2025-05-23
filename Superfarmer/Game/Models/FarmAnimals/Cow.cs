namespace Superfarmer.Game.Models.FarmAnimals;

public class Cow : FarmAnimal
{
    public override int Value => 36;
    public override string Name => "Cow";
    public Cow() : base()
    {
    }

    public override bool Equals(object? obj) => obj is Cow;
    public override int GetHashCode() => Name.GetHashCode();

}