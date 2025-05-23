using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models;

public interface ITradable: IAnimal
{
    int Value { get; }
}
