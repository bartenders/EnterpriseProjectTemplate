using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using EPT.Infrastructure.Interfaces;
using NLog;

namespace TDSKPro.Core.Logging
{
    using NLogger = NLog.Logger;

    /// <summary>
    ///     This logger implements the interface ILogger using NLog 2.0.
    ///     Following SAMPLE configuration can be added to the application configuration file to use the logging functionality:
    ///     <configSections>
    ///         <section name="nlog" type="NLog.Config.ConfigSectionHandler, NLog" />
    ///     </configSections>
    ///     <nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    ///         <targets>
    ///             <!-- Colored console logger -->
    ///             <target xsi:type="ColoredConsole" name="console"
    ///                 layout="${longdate} ${logger} ${message}"
    ///                 useDefaultRowHighlightingRules="true" />
    ///             <!-- Rolling FileLogger with max size and time -->
    ///             <target name="file" xsi:type="File"
    ///                 layout="${longdate} ${logger} ${message}"
    ///                 fileName="${basedir}/logs/logfile.txt"
    ///                 archiveFileName="${basedir}/archives/log.{#####}.txt"
    ///                 archiveAboveSize="10240000"
    ///                 archiveEvery="Minute"
    ///                 archiveNumbering="Rolling"
    ///                 maxArchiveFiles="99999"
    ///                 concurrentWrites="true"
    ///                 keepFileOpen="true"
    ///                 encoding="utf-8" />
    ///         </targets>
    ///         <rules>
    ///             <logger name="*" minlevel="Debug" writeTo="console,file" />
    ///         </rules>
    ///     </nlog>
    ///     For detailed information: <see cref="http://nlog-project.org/wiki/Documentation" />
    /// </summary>
    public sealed class Logger
        : ILogger
    {
        #region Public Functions

        /// <summary>
        ///     <see cref="ILogger.Debug(string)" />
        /// </summary>
        public void Debug(string message)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsDebugEnabled)
                {
                    logger.Debug(message);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Debug(string,System.Exception)" />
        /// </summary>
        public void Debug(string message, Exception exception)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsDebugEnabled)
                {
                    if (null != exception && null != exception.InnerException)
                    {
                        logger.DebugException(exception.InnerException.Message, exception.InnerException);
                    }

                    logger.DebugException(message, exception);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.DebugFormat(string,object[])" />
        /// </summary>
        public void DebugFormat(string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsDebugEnabled)
                {
                    logger.Debug(message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.DebugFormat(System.IFormatProvider,string,object[])" />
        /// </summary>
        public void DebugFormat(IFormatProvider provider, string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsDebugEnabled)
                {
                    logger.Debug(provider, message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Error(string)" />
        /// </summary>
        public void Error(string message)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsErrorEnabled)
                {
                    logger.Error(message);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Error(string,System.Exception)" />
        /// </summary>
        public void Error(string message, Exception exception)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsErrorEnabled)
                {
                    if (null != exception && null != exception.InnerException)
                    {
                        logger.ErrorException(exception.InnerException.Message, exception.InnerException);
                    }

                    logger.ErrorException(message, exception);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.ErrorFormat(string,object[])" />
        /// </summary>
        public void ErrorFormat(string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsErrorEnabled)
                {
                    logger.Error(message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.ErrorFormat(System.IFormatProvider,string,object[])" />
        /// </summary>
        public void ErrorFormat(IFormatProvider provider, string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsErrorEnabled)
                {
                    logger.Error(provider, message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Fatal(string)" />
        /// </summary>
        public void Fatal(string message)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsFatalEnabled)
                {
                    logger.Fatal(message);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Fatal(string,System.Exception)" />
        /// </summary>
        public void Fatal(string message, Exception exception)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsFatalEnabled)
                {
                    if (null != exception && null != exception.InnerException)
                    {
                        logger.FatalException(exception.InnerException.Message, exception.InnerException);
                    }

                    logger.FatalException(message, exception);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.FatalFormat(string,object[])" />
        /// </summary>
        public void FatalFormat(string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsFatalEnabled)
                {
                    logger.Fatal(message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.FatalFormat(System.IFormatProvider,string,object[])" />
        /// </summary>
        public void FatalFormat(IFormatProvider provider, string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsFatalEnabled)
                {
                    logger.Fatal(provider, message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Info(string)" />
        /// </summary>
        public void Info(string message)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsInfoEnabled)
                {
                    logger.Info(message);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Info(string,System.Exception)" />
        /// </summary>
        public void Info(string message, Exception exception)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsInfoEnabled)
                {
                    if (null != exception && null != exception.InnerException)
                    {
                        logger.InfoException(exception.InnerException.Message, exception.InnerException);
                    }

                    logger.InfoException(message, exception);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.InfoFormat(string,object[])" />
        /// </summary>
        public void InfoFormat(string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsInfoEnabled)
                {
                    logger.Info(message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.InfoFormat(System.IFormatProvider,string,object[])" />
        /// </summary>
        public void InfoFormat(IFormatProvider provider, string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsInfoEnabled)
                {
                    logger.Info(provider, message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Warn(string)" />
        /// </summary>
        public void Warn(string message)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsWarnEnabled)
                {
                    logger.Warn(message);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.Warn(string,System.Exception)" />
        /// </summary>
        public void Warn(string message, Exception exception)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsWarnEnabled)
                {
                    if (null != exception && null != exception.InnerException)
                    {
                        logger.WarnException(exception.InnerException.Message, exception.InnerException);
                    }

                    logger.WarnException(message, exception);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.WarnFormat(string,object[])" />
        /// </summary>
        public void WarnFormat(string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsWarnEnabled)
                {
                    logger.Warn(message, args);
                }
            }
        }

        /// <summary>
        ///     <see cref="ILogger.WarnFormat(System.IFormatProvider,string,object[])" />
        /// </summary>
        public void WarnFormat(IFormatProvider provider, string message, params object[] args)
        {
            if (LogManager.IsLoggingEnabled())
            {
                NLogger logger = GetLogger();

                if (logger.IsWarnEnabled)
                {
                    logger.Warn(provider, message, args);
                }
            }
        }

        #endregion Public Functions

        #region Private Functions

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <returns>Logger</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static NLogger GetLogger()
        {
            var stackTrace = new StackTrace(false);
            StackFrame[] stackFrames = stackTrace.GetFrames();

            if (null == stackFrames)
            {
                throw new ArgumentException("Stack frame array is null.");
            }

            StackFrame stackFrame;

            switch (stackFrames.Length)
            {
                case 0:
                    throw new ArgumentException("Length of stack frames is 0.");
                case 1:
                case 2:
                    stackFrame = stackFrames[stackFrames.Length - 1];
                    break;
                default:
                    stackFrame = stackTrace.GetFrame(2);
                    break;
            }

            Type type = stackFrame.GetMethod().DeclaringType;

            return null == type ? LogManager.GetCurrentClassLogger() : LogManager.GetLogger(type.FullName);
        }

        #endregion Private Functions
    }
}