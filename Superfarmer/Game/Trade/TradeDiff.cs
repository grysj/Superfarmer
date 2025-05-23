using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Trade;


public class TradeDiff
{
    public static Dictionary<Models.ITradable, int> GetTradeDiff(Dictionary<Models.ITradable, int> offer, Dictionary<Models.ITradable, int> request)
    {
        LinkedList<Models.ITradable> resultKeys = new LinkedList<Models.ITradable>(offer.Keys.Union(request.Keys));

        Dictionary<Models.ITradable, int> result = new Dictionary<Models.ITradable, int>();
        foreach (var key in resultKeys)
        {
            int offerCount = offer.TryGetValue(key, out int offerValue) ? offerValue : 0;
            int requestCount = request.TryGetValue(key, out int requestValue) ? requestValue : 0;
            int diff = offerCount - requestCount;
            result.Add(key, diff);
        }

        return result;
    }
}
