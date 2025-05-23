using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer.Game.Trade;


public class TradeValidator
{
    public static bool ValidateTrade(Dictionary<Models.ITradable, int> offer, Dictionary<Models.ITradable, int> request)
    {
        // Check if the offer and request are valid
        if (offer == null || request == null)
        {
            return false;
        }
        int offerCount = offer.Values.Sum();
        int requestCount = request.Values.Sum();

        if (offerCount != 1 && requestCount != 1)
        {
            return false;
        }

        int offerValue = offer.Sum(item => item.Key.Value * item.Value);
        int requestValue = request.Sum(item => item.Key.Value * item.Value);

        if (offerValue != requestValue)
        {
            return false;
        }
        return true;

    }
}
