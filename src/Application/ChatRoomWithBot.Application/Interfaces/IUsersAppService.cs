using ChatRoomWithBot.Application.ViewModel;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IUsersAppService:IDisposable
{
    Task<UserViewModel> GetUserByIdAsync(Guid id);
    Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    Task<UserViewModel> GetCurrentUserAsync();

    bool IsAuthenticated(); 
}