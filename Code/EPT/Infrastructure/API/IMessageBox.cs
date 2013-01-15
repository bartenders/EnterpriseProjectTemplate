using Caliburn.Micro;
using EPT.Infrastructure.Framework;

namespace EPT.Infrastructure.API
{
    public interface IMessageBox : IScreen
    {
        string Message { get; set; }
        MessageBoxOptions Options { get; set; }

        void Ok();
        void Cancel();
        void Yes();
        void No();

        bool WasSelected(MessageBoxOptions option);
    }
}