using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IUsersAppService:IDisposable
{
    Task<UserViewModel> GetUserByIdAsync(Guid id);
    Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    Task<UserViewModel> GetCurrentUserAsync();

    bool IsAuthenticated();


    string GetUserName();

    string GetTenantId();


    Task<IEnumerable<ChatRoom>> GetChats();
    Task<ChatRoom?> GetChat(Guid id);
    Task<IEnumerable<AuditModel>> GetAudits();
    Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync();
}