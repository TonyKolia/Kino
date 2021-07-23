using Kino.Models.KinoDBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models
{
    public class DrawRepository : IDrawRepository
    {
        private readonly KinoDBContext db;

        public DrawRepository(KinoDBContext db)
        {
            this.db = db;
        }

        public async Task<Draw> AddDraw(Draw draw)
        {
            db.Draws.Add(draw);
            await db.SaveChangesAsync();
            return draw;
        }

        public async Task<Draw> GetDraw(string DrawId)
        {
            return await db.Draws.Where(d => d.DrawId == DrawId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Draw>> GetDraws()
        {
            return await db.Draws.ToListAsync();
        }

        public async Task<IEnumerable<Draw>> SearchDraw(string DrawId, DateTime? dateFrom, DateTime? dateTo)
        {
            IQueryable<Draw> query = db.Draws;

            if(!string.IsNullOrEmpty(DrawId))
            {
                query = query.Where(d => d.DrawId.StartsWith(DrawId));
            }

            if (dateFrom.HasValue)
            {
                query = query.Where(d => d.DrawDateTime >= dateFrom);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(d => d.DrawDateTime <= dateTo);
            }

            return await query.ToListAsync();
        }
    }
}
