using AutoMapper;
using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ChatRoomWithBot.Application.Services
{
    public  class UsersAppService : IUsersAppService
    {
        private readonly IUserIdentityRepository _userIdentityRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        private readonly UserManager<UserIdentity> _userManager;

        public UsersAppService(IUserIdentityRepository userIdentityRepository, IMapper mapper, IHttpContextAccessor accessor, UserManager<UserIdentity> userManager)
        {
            _userIdentityRepository = userIdentityRepository;
            _mapper = mapper;
            _accessor = accessor;
            _userManager = userManager;
        }

        public void Dispose()
        {
            _userIdentityRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid userId)
        {
            var result = _userIdentityRepository.GetUserByIdAsync(userId);

            var map = _mapper.Map<UserViewModel>(result);

            return map;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var result = await _userIdentityRepository.GetAllUsersAsync();

            var map = _mapper.Map<IEnumerable<UserViewModel>>(result);

            return map;
        }

        public async Task<UserViewModel> GetCurrentUserAsync()
        {
            var userId = _accessor.HttpContext.User
                .Identities.FirstOrDefault()
                ?.Claims.FirstOrDefault(x => x.Type == "userId")
                ?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            var map = _mapper.Map<UserViewModel>(user);
            return map;
        }
    }
}
