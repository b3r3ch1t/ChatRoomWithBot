using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Data.Interfaces;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatRoomWithBot.Data.Repository
{
    internal class UserIdentityRepository : IUserIdentityRepository
    {

        private readonly UserManager<UserIdentity> _userManager;
        private readonly IBerechitLogger _berechitLogger;
        private readonly ChatRoomWithBotContext _context;

        public UserIdentityRepository(UserManager<UserIdentity> userManager, IBerechitLogger berechitLogger, ChatRoomWithBotContext context)
        {
            _userManager = userManager;
            _berechitLogger = berechitLogger;
            _context = context;
        }

        public async Task AddAsync(UserIdentity user, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    _berechitLogger.Information("the user was created !");
                }

            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
            }


        }

        public async Task<IEnumerable<UserIdentity>> GetAllAsync()
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }

        public async Task<UserIdentity> GetByIdAsync(Guid id)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _context.Dispose();
            _userManager.Dispose();
        }


        public async Task<UserIdentity> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<IEnumerable<UserIdentity>> GetAllUsersAsync()
        {
            var result = _context.Users.AsEnumerable();

            return result;
        }
    }
}
