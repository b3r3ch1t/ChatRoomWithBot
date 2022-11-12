using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Data.Interfaces;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatRoomWithBot.Data.Repository
{
    internal class UserIdentityRepository:IUserIdentityRepository 
    {

        private readonly UserManager<UserIdentity> _userManager; 
        private readonly IError _error;
        private readonly ChatRoomWithBotContext _context; 
        public UserIdentityRepository(IDependencyResolver dependencyResolver)
        {
            _userManager = dependencyResolver.Resolve<UserManager<UserIdentity>>(); 
            _error = dependencyResolver.Resolve<IError>();
            _context = dependencyResolver.Resolve<ChatRoomWithBotContext>(); 

        }
        public async Task AddAsync(UserIdentity user, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    _error.Information("the user was created !");
                }

            }
            catch (Exception e)
            {
               _error.Error(e);
            }
           
            
        }

        public async  Task<List<UserIdentity>> GetAllAsync()
        {
            var result =await  _context.Users.ToListAsync();
            return result; 
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _context.Dispose();
            _userManager.Dispose();
        }


        public async  Task<UserIdentity> GetUserByIdAsync(Guid userId)
        {
            return await  _context.Users.FirstOrDefaultAsync(x => x.Id == userId); 
        }

        public async  Task<IEnumerable<UserIdentity>> GetAllUsersAsync()
        {
            var result =   _context.Users.AsEnumerable();

            return   result ;
        }
    }
}
