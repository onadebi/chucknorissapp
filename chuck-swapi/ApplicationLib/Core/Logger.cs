using System.Diagnostics;

namespace chuck_swapi.ApplicationLib.Core
{
    public class Logger
    {
        public static void Log(string message, EventLoggerType loggerTye = EventLoggerType.INFORMATION)
        {
            try
            {
                using (EventLog eventLog = new("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry($"{loggerTye.ToString()}: {message}", EventLogEntryType.Information, 101, 1);
                }
            }
            catch (Exception) { }

        }
    }

    public enum EventLoggerType
    {
        INFORMATION,
        ERROR
    }
}
