

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IDependencyResolver
{
    TDependencyType Resolve<TDependencyType>();

}