using Kino.Models.KinoDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.API.Models
{
    public interface IOutcomeRepository
    {
        Task<IEnumerable<Outcome>> GetOutcomes();
    }
}
