using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    public class RecentDrawViewModel
    {
        public string DrawId { get; set; }
        public List<int> WinningNumbers { get; set; }
        public int KinoBonus { get; set; }
        public string DrawTime { get; set; }
    }
}
