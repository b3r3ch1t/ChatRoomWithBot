 

using ChatRoomWithBot.Domain.Interfaces;

namespace ChatRoomWithBot.Domain
{
    public  class ClassBase
    {
        private readonly IError _error; 
        public ClassBase(IDependencyResolver dependencyResolver)
        {
            _error = dependencyResolver.Resolve<IError>(); 
        }

        public  void ExecuteSafe(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                _error.Error(exception );
            }
        }
    }
}
