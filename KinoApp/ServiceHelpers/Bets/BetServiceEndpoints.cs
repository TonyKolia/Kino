using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.ServiceHelpers.Bets
{
    public static class BetServiceEndpoints
    {
        public const string AddBet = "UserBets/AddBet?numberOfDraws={0}";
        public const string GetUserBets = "UserBets/GetUserBets?userId={0}";
    }
}
