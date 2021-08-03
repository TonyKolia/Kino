using Kino.Models.KinoDBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.API.Models
{
    public class OutcomeRepository : IOutcomeRepository
    {
        private readonly KinoDBContext db;

        public OutcomeRepository(KinoDBContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Outcome>> GetOutcomes()
        {
            return await db.Outcomes.ToListAsync();
        }
    }
}
