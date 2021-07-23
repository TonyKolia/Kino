using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.API.Models.ServiceResponse
{
    public class AddOn
    {
        public double amount { get; set; }
        public string gameType { get; set; }
    }

    public class PricePoints
    {
        public List<AddOn> addOn { get; set; }
        public double amount { get; set; }
    }

    public class ColumnNumbers
    {
        public List<int> _1 { get; set; }
        public List<int> _2 { get; set; }
        public List<int> _3 { get; set; }
        public List<int> _4 { get; set; }
        public List<int> _5 { get; set; }
        public List<object> _6 { get; set; }
        public List<int> _7 { get; set; }
        public List<int> _8 { get; set; }
        public List<object> _9 { get; set; }
        public List<int> _10 { get; set; }
    }

    public class Sidebets
    {
        public int evenNumbersCount { get; set; }
        public int oddNumbersCount { get; set; }
        public int winningColumn { get; set; }
        public string winningParity { get; set; }
        public List<int> oddNumbers { get; set; }
        public List<int> evenNumbers { get; set; }
        public ColumnNumbers columnNumbers { get; set; }
    }

    public class WinningNumbers
    {
        public List<int> list { get; set; }
        public List<int> bonus { get; set; }
        public Sidebets sidebets { get; set; }
    }

    public class PrizeCategory
    {
        public int id { get; set; }
        public double divident { get; set; }
        public int winners { get; set; }
        public double distributed { get; set; }
        public double jackpot { get; set; }
        public double @fixed { get; set; }
        public int categoryType { get; set; }
        public string gameType { get; set; }
    }

    public class WagerStatistics
    {
        public int columns { get; set; }
        public int wagers { get; set; }
        public List<object> addOn { get; set; }
    }

    public class Last
    {
        public int gameId { get; set; }
        public int drawId { get; set; }
        public long drawTime { get; set; }
        public string status { get; set; }
        public int drawBreak { get; set; }
        public int visualDraw { get; set; }
        public PricePoints pricePoints { get; set; }
        public WinningNumbers winningNumbers { get; set; }
        public List<PrizeCategory> prizeCategories { get; set; }
        public WagerStatistics wagerStatistics { get; set; }
    }

    public class Active
    {
        public int gameId { get; set; }
        public int drawId { get; set; }
        public long drawTime { get; set; }
        public string status { get; set; }
        public int drawBreak { get; set; }
        public int visualDraw { get; set; }
        public PricePoints pricePoints { get; set; }
        public List<PrizeCategory> prizeCategories { get; set; }
        public WagerStatistics wagerStatistics { get; set; }
    }

    public class Root
    {
        public Last last { get; set; }
        public Active active { get; set; }
    }
}
