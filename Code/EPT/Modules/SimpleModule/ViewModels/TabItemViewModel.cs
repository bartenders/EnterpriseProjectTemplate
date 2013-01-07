using Caliburn.Micro;

namespace EPT.Modules.SimpleModule.ViewModels
{
    public class TabItemViewModel : Screen
    {
        public override void CanClose(System.Action<bool> callback)
        {
            base.CanClose(callback);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }
    }
}
