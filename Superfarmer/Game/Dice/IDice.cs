using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Models;
namespace Superfarmer.Game.Dice;


public interface IDice
{
    IAnimal Roll();
}
