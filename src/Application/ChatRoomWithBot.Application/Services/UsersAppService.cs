using AutoMapper;
using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Data.Interfaces;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ChatRoomWithBot.Application.Services
{
    internal  class UsersAppService: IUsersAppService
    {
        private readonly IUserIdentityRepository _userIdentityRepository;
        private readonly IError _error;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;


        public UsersAppService(IUserIdentityRepository userIdentityRepository, IError error, IMapper mapper, IHttpContextAccessor accessor)
        {
            _userIdentityRepository = userIdentityRepository;
            _error = error;
            _mapper = mapper;
            _accessor = accessor;
        }

        public void Dispose()
        {
            _userIdentityRepository.Dispose(); 
            GC.SuppressFinalize(this);
        }

        public  bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid userId)
        {
            var result = _userIdentityRepository.GetUserByIdAsync(userId);

            var map = _mapper.Map<UserViewModel>(result);

            return map;
        }

        public async   Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var result =await  _userIdentityRepository.GetAllUsersAsync( );

            var map = _mapper.Map<IEnumerable<UserViewModel>> (result);

            return map;
        }

        public Task<UserViewModel> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
