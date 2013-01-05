using System;

namespace EPT.Infrastructure.Interfaces
{
    /// <summary>
    /// This interface specifies the methods of a logger.
    /// </summary>
    public interface ILogger
    {

        /// <summary>
        /// Logs the message, if the log level is DEBUG.
        /// </summary>
        /// <param name="message">Message</param>
        void Debug(string message);

        /// <summary>
        /// Logs the message and the exception, if the log level is DEBUG.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Logs the formatted message with the given parameters, if the log level is DEBUG.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void DebugFormat(string message, params object[] args);

        /// <summary>
        /// Logs the formatted message with the given parameters using the given format provider, if the log level is DEBUG.
        /// </summary>
        /// <param name="provider">Format provider</param>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void DebugFormat(IFormatProvider provider, string message, params object[] args);

        /// <summary>
        /// Logs the message, if the log level is ERROR.
        /// </summary>
        /// <param name="message">Message</param>
        void Error(string message);

        /// <summary>
        /// Logs the message and the exception, if the log level is ERROR.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Logs the formatted message with the given parameters, if the log level is ERROR.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void ErrorFormat(string message, params object[] args);

        /// <summary>
        /// Logs the formatted message with the given parameters using the given format provider, if the log level is ERROR.
        /// </summary>
        /// <param name="provider">Format provider</param>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void ErrorFormat(IFormatProvider provider, string message, params object[] args);

        /// <summary>
        /// Logs the message, if the log level is FATAL.
        /// </summary>
        /// <param name="message">Message</param>
        void Fatal(string message);

        /// <summary>
        /// Logs the message and the exception, if the log level is FATAL.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        void Fatal(string message, Exception exception);

        /// <summary>
        /// Logs the formatted message with the given parameters, if the log level is FATAL.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void FatalFormat(string message, params object[] args);

        /// <summary>
        /// Logs the formatted message with the given parameters using the given format provider, if the log level is FATAL.
        /// </summary>
        /// <param name="provider">Format provider</param>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void FatalFormat(IFormatProvider provider, string message, params object[] args);

        /// <summary>
        /// Logs the message, if the log level is INFO.
        /// </summary>
        /// <param name="message">Message</param>
        void Info(string message);

        /// <summary>
        /// Logs the message and the exception, if the log level is INFO.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Logs the formatted message with the given parameters, if the log level is INFO.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void InfoFormat(string message, params object[] args);

        /// <summary>
        /// Logs the formatted message with the given parameters using the given format provider, if the log level is INFO.
        /// </summary>
        /// <param name="provider">Format provider</param>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void InfoFormat(IFormatProvider provider, string message, params object[] args);

        /// <summary>
        /// Logs the message, if the log level is WARN.
        /// </summary>
        /// <param name="message">Message</param>
        void Warn(string message);

        /// <summary>
        /// Logs the message and the exception, if the log level is WARN.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        void Warn(string message, Exception exception);

        /// <summary>
        /// Logs the formatted message with the given parameters, if the log level is WARN.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void WarnFormat(string message, params object[] args);

        /// <summary>
        /// Logs the formatted message with the given parameters using the given format provider, if the log level is WARN.
        /// </summary>
        /// <param name="provider">Format provider</param>
        /// <param name="message">Message</param>
        /// <param name="args">Parameter of the formatted message</param>
        void WarnFormat(IFormatProvider provider, string message, params object[] args);

 
    }
}