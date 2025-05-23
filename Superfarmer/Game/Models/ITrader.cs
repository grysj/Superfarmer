using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Models;


public interface ITrader
{

    string Name { get; }
    bool DecideTrade(ITrader trader, Dictionary<Models.ITradable, int> offer, Dictionary<Models.ITradable, int> request);


    void Exchange(Dictionary<Models.ITradable, int> changes);

    Dictionary<ITradable, int> GetTradable();


}