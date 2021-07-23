using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.Helpers
{
    public class OperationResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
