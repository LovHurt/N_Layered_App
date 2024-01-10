using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LoggingConfig
    {
        public static void Configure()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(@"C:\Users\buse\Desktop\Projects\N_Layered_App\Logs\log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


        }
    }
}
