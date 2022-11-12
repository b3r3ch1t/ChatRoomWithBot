using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Domain.IoC;

public class DependencyResolver : IDependencyResolver
{
    private readonly IServiceProvider _serviceProvider;

    public DependencyResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDependencyType Resolve<TDependencyType>()
    {
       
        return _serviceProvider.GetRequiredService<TDependencyType>();
    }
 
}