using System;
using System.Collections.Generic;

#nullable disable

namespace Kino.Models.KinoDBModel
{
    public partial class Draw
    {
        public string DrawId { get; set; }
        public DateTime? DrawDateTime { get; set; }
        public string WinningNumbers { get; set; }
        public string KinoBonus { get; set; }
    }
}
