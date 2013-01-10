using System.Windows.Controls;

namespace EPT.Infrastructure.API
{
    /// <summary>
    /// Shell Modules are top level Modules
    /// </summary>
    public interface IShellModule
    {
        /// <summary>
        /// Gets the icon image.
        /// </summary>
        Image Icon { get; }

        /// <summary>
        /// Gets the order priority.
        /// </summary>
        int OrderPriority { get; }

        /// <summary>
        /// Gets a value indicating whether to Show this Module in the Menu Entry
        /// </summary>
        /// <value>
        ///   <c>true</c> if [active menu entry]; otherwise, <c>false</c>.
        /// </value>
        bool ActiveMenuEntry { get; }
    }
}