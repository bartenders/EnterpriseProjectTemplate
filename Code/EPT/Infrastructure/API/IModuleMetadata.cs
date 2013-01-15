
namespace EPT.Infrastructure.API
{
	public interface IModuleMetadata
	{
		int Order { get; }
		string Title { get; }
	}
}
