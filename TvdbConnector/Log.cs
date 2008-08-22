using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvdbConnector
{
  internal class Log
  {
    /// <summary>
    /// Loglevel
    /// </summary>
    internal enum LOGLEVEL { Debug, Info, Warn, Error, Fatal }

    /// <summary>
    /// Logs the message at level Debug
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Debug(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level Debug
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Debug(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level info
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Info(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level info
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Info(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level Warn
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Warn(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level Warn
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Warn(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level Error
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Error(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level Error
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Error(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level Fatal
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Fatal(String _logMessage)
    {
      Debug(_logMessage, LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at level Fatal
    /// </summary>
    /// <param name="_logMessage"></param>
    internal static void Fatal(String _logMessage, Exception _ex)
    {
      Debug(_logMessage + _ex.ToString(), LOGLEVEL.Debug);
    }

    /// <summary>
    /// Logs the message at the given level
    /// </summary>
    /// <param name="_logMessage"></param>
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
