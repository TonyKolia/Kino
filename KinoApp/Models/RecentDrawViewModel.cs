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

        public RecentDrawViewModel()
        {
            WinningNumbers = new List<int>();
        }

        public RecentDrawViewModel(DateTime? drawDateTime)
        {
            WinningNumbers = new List<int>();
            var drawHour = drawDateTime.Value.Hour;
            var drawMinute = drawDateTime.Value.Minute;
            DrawTime = (drawHour < 10 ? "0" + drawHour.ToString() : drawHour.ToString()) + ":" + (drawMinute < 10 ? "0" + drawMinute.ToString() : drawMinute.ToString());
        }
    }

}
