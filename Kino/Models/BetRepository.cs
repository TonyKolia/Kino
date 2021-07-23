using Kino.Models.KinoDBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models
{
    public class BetRepository : IBetRepository
    {
        private readonly KinoDBContext db;

        public BetRepository(KinoDBContext db)
        {
            this.db = db;
        }

        public async Task<Bet> AddBet(Bet bet)
        {
            db.Bets.Add(bet);
            await db.SaveChangesAsync();
            return bet;
        }

        public async Task<IEnumerable<Bet>> GetUserBets(int userId)
        {
            return await db.Bets.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Bet>> GetUserBetsByDate(DateTime? dateFrom, DateTime? dateTo, int userId)
        {
            IQueryable<Bet> query = db.Bets.Where(b => b.UserId == userId);

            if (dateFrom.HasValue)
            {
                query = query.Where(b => b.BetDate >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(b => b.BetDate <= dateTo.Value);
            }

            return  await query.ToListAsync();
        }

        public async Task<IEnumerable<Bet>> GetUserBetsByOutcome(int outcome, int userId)
        {
            return await db.Bets.Where(b => b.UserId == userId && b.Outcome == outcome).ToListAsync();
        }

        public async Task<Bet> UpdateBetOutcome(Bet bet, int outcome)
        {
            var existingBet = db.Bets.Where(b => b.Id == bet.Id).FirstOrDefault();
            if(existingBet != null)
            {
                existingBet.Outcome = outcome;
                db.Entry(existingBet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return existingBet;
        }
    }
}
