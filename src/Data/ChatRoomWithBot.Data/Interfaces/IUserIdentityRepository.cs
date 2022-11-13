using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.Interfaces.Repositories;

namespace ChatRoomWithBot.Data.Interfaces;

public interface IUserIdentityRepository : IRepository<UserIdentity>
{
    Task<UserIdentity> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<UserIdentity>> GetAllUsersAsync();
}