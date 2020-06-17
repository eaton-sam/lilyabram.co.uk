using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LilyAbramSite.Prismic.Helpers
{
    public class PrismicLogger : prismic.ILogger
    {
        private ILogger Logger { get; }

        public PrismicLogger(ILogger logger)
        {
            Logger = logger;
        }

        public void log(string level, string msg)
        {
            Logger.Log(LogLevel.Information, msg);
        }
    }
}
