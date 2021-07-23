using Kino.Models.KinoDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models
{
    public interface IDrawRepository
    {
        Task<IEnumerable<Draw>> GetDraws();
        Task<IEnumerable<Draw>> SearchDraw(string DrawId, DateTime? dateFrom, DateTime? dateTo);
        Task<Draw> GetDraw(string DrawId);
        Task<Draw> AddDraw(Draw draw);
    }
}
