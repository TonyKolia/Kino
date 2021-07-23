using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder().SetBasePath(Path.Combine(AppContext.BaseDirectory))
                                                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                              .Build();
        }
    }
}
