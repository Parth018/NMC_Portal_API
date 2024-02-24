using NLog;
using System;

namespace ITMS.Business.Logger
{
    public static class clsLogger
    {
        //private const int V = 100;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        //public static void Main()
        //{
        //    try
        //    {
        //        int ii = 0;
        //        int t = (V / ii);
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog(ex, LogLevel.Error, "main()", 10);
        //    }
        //}

        
        public static void WriteLog(string message, Exception exception, LogLevel logLevel, int userId)
        {
            try
            {
                //Logger.Info("my info message");

                LogEventInfo theEvent = new LogEventInfo(LogLevel.Error,null, message);
                theEvent.Properties["StackTrace"] = exception.StackTrace;
                theEvent.Properties["TargetSite"] = exception.TargetSite;
                theEvent.Properties["Source"] = exception.Source;
                theEvent.Properties["userId"] = userId;
                
                Logger.Log(theEvent);

                //Logger.Log(logLevel, exception.Message, exception.StackTrace, exception.TargetSite, exception.Source);

                //System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "WriteLog");
            }
        }
         
    }
}
