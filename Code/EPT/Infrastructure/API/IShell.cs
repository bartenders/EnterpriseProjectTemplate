using Caliburn.Micro;

namespace EPT.Infrastructure.API
{
    public interface IShell : IConductor, IGuardClose
    {
        IDialogManager Dialogs { get; }
        IFlyout Flyouts { get; }
    }
}