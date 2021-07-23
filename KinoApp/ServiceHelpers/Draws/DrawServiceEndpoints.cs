using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.ServiceHelpers.Draws
{
    public static class DrawServiceEndpoints
    {
        public const string GetDraws = "Draw/GetDraws";
        public const string GetDraw = "Draw/GetDraw?drawId={0}";
        public const string AddDraw = "Draw/AddDraw";
    }
}
