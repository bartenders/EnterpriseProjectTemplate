using System;

namespace EPT.Infrastructure.API
{
    /// <summary>
    /// Will display a Window Command in the Shell Window
    /// </summary>
    public interface IWindowCommand
    {
        /// <summary>
        /// Gets the display name of the command.
        /// </summary>
        /// <value>
        /// The display name of the command.
        /// </value>
        string CommandDisplayName { get; }

        /// <summary>
        /// Will be executed on the Command Click
        /// </summary>
        /// <value>
        /// The command action.
        /// </value>
        Action CommandAction { get; }

    }
}