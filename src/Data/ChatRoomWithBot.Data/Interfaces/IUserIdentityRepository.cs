using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Domain.Interfaces;

namespace ChatRoomWithBot.Data.Interfaces;

public interface IUserIdentityRepository : IRepository<UserIdentity>
{
    Task<UserIdentity> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<UserIdentity>> GetAllUsersAsync();
}