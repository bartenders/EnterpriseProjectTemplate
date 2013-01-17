using Caliburn.Micro;

namespace EPT.Infrastructure.API
{
    public interface IShell : IConductor, IGuardClose
    {

        IFlyout Flyouts { get; }

        /// <summary>
        /// Logs some Infomrations in the Shell Log
        /// </summary>
        /// <param name="message">the message to Log</param>
        void Log(string message);
    }
}