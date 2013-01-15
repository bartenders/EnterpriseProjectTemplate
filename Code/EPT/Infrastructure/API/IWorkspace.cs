namespace EPT.Infrastructure.API {
    public interface IWorkspace {
        string Icon { get; }
        string IconName { get; }
        string Status { get; }

        void Show();
    }
}