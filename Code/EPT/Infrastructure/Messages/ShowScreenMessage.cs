using Caliburn.Micro;

namespace EPT.Infrastructure.Messages
{
    public class ShowScreenMessage
    {
        private readonly IScreen _module;

        public ShowScreenMessage(IScreen screen)
        {
            _module = screen;
        }

        /// <summary>
        /// The ViewModel to be dispalyed in the Shell
        /// </summary>
        /// <value>
        /// The Screen.
        /// </value>
        public IScreen Screen { get { return _module; } }
    }
}