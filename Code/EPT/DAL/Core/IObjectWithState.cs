namespace ETP.DAL.Core
{
    public interface IObjectWithState
    {
        State State { get; set; }
    }
}
