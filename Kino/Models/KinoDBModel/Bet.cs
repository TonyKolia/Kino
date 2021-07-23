using System;
using System.Collections.Generic;

#nullable disable

namespace Kino.Models.KinoDBModel
{
    public partial class Bet
    {
        public int Id { get; set; }
        public string DrawId { get; set; }
        public int? UserId { get; set; }
        public int? NoOfNumbers { get; set; }
        public string SelectedNumbers { get; set; }
        public decimal? BetAmount { get; set; }
        public DateTime? BetDate { get; set; }
        public int? Outcome { get; set; }
    }
}
