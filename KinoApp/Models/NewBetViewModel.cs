using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    public class NewBetViewModel
    {
        public string SelectedNumbers { get; set; }
        public int? NumberOfSelectedNumbers { get; set; }
        public int? NumberOfDraws { get; set; }
        public decimal? BetAmount { get; set; }
    }
}
