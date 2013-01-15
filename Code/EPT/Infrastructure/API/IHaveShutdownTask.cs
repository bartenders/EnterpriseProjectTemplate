using Caliburn.Micro;

namespace EPT.Infrastructure.API {
    public interface IHaveShutdownTask {
        IResult GetShutdownTask();
    }
}