namespace EPT.Infrastructure.API {
    public interface IDocumentWorkspace : IWorkspace {
        void Edit(object document);
    }
}