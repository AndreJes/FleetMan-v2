using SimpleWPFLogger.UserControls;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManApiController.Services
{
    public class LoggerManager
    {
        public ConcurrentDictionary<string, Logger> Loggers;

        private static LoggerManager _instance;

        private LoggerManager()
        {
            Loggers = new ConcurrentDictionary<string, Logger>();
        }

        public static LoggerManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LoggerManager();
            }

            return _instance;
        }

        public void AddLogger(string name, Logger logger)
        {
            Loggers[name] = logger;
        }

        public void RemoveLogger(string name)
        {
            try
            {
                Logger logger = null;
                Loggers.TryRemove(name, out logger);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
