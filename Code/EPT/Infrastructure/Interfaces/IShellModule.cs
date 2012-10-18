using System.Windows.Controls;

namespace EPT.Infrastructure.Interfaces
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
    }
}