using Kino.Models.KinoDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models
{
    public interface IBetRepository
    {
        public Task<IEnumerable<Bet>> GetUserBets(int userId);
        public Task<Bet> AddBet(Bet bet);
        public Task<IEnumerable<Bet>> GetUserBetsByOutcome(int outcome, int userId);
        public Task<Bet> UpdateBetOutcome(Bet bet, int outcome);
        public Task<IEnumerable<Bet>> GetUserBetsByDate(DateTime? dateFrom, DateTime? dateTo, int userId);
    }
}
