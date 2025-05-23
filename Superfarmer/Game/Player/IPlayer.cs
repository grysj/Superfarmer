using Superfarmer.Game.Models;
using Superfarmer.Game.Models.FarmAnimals;
using Superfarmer.Game.Models.SpecialAnimals;

namespace Superfarmer.Game.Player;


public interface IPlayer: ITrader
{
    
    string Name { get; set; }
    Dictionary<string, int> Dogs { get; }


    void TakeTurn();


    void RollDice();

    bool IsWinner();



}
