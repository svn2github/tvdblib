using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector
{
  internal class Log
  {
    internal enum LOGLEVEL { Debug, Info, Warn, Error, Fatal }

    internal static void Debug(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }
    internal static void Debug(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }


    internal static void Info(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    internal static void Info(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }

    internal static void Warn(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    internal static void Warn(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }

    internal static void Error(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    internal static void Error(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }

    internal static void Fatal(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    internal static void Fatal(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }


    internal static void Debug(String _logMessage, LOGLEVEL _level)
    {
      switch (_level)
      {
        case LOGLEVEL.Debug:
          //debug log processing
          Console.WriteLine(_logMessage);
          break;
        case LOGLEVEL.Info:
          //debug log processing
          Console.WriteLine(_logMessage);
          break;
        case LOGLEVEL.Warn:
          //debug log processing
          Console.WriteLine(_logMessage);
          break;
        case LOGLEVEL.Error:
          //debug log processing
          Console.WriteLine(_logMessage);
          break;
        case LOGLEVEL.Fatal:
          //debug log processing
          Console.WriteLine(_logMessage);
          break;
      }

    }
  }
}
