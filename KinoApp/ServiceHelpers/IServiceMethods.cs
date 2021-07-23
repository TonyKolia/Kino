using KinoApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.ServiceHelpers
{
    public interface IServiceMethods
    {
        T PerformGet<T>(string endpoint, string[] parameters, out OperationResult result);
        void PerformPost(string endpoint, string[] parameters, object data, out OperationResult result);
        void PerformDelete(string endpoint, string[] parameters, out OperationResult result);
        void PerformUpdate(string endpoint, string[] parameters, object data, out OperationResult result);
    }
}
