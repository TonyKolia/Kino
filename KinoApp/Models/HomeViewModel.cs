using Kino.Models.KinoDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.Models
{
    public class HomeViewModel
    {
        public string DrawId { get; set; }
        public List<int> WinningNumbers { get; set; }
        public int KinoBonus { get; set; }
        public string DrawTime { get; set; }
        public List<Bet> UserBets { get; set; }
        public List<Outcome> Outcomes { get; set; }

        public HomeViewModel()
        {
            WinningNumbers = new List<int>();
            UserBets = new List<Bet>();
            Outcomes = new List<Outcome>();
        }

        public HomeViewModel(DateTime? drawDateTime)
        {
            WinningNumbers = new List<int>();
            UserBets = new List<Bet>();
            Outcomes = new List<Outcome>();
            var drawHour = drawDateTime.Value.Hour;
            var drawMinute = drawDateTime.Value.Minute;
            DrawTime = (drawHour < 10 ? "0" + drawHour.ToString() : drawHour.ToString()) + ":" + (drawMinute < 10 ? "0" + drawMinute.ToString() : drawMinute.ToString());
        }
    }

}
