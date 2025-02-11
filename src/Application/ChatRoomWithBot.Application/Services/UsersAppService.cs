 
using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel; 
using Microsoft.AspNetCore.Http; 

namespace ChatRoomWithBot.Application.Services
{
	public class UsersAppService : IUsersAppService
	{

		 
		private readonly IHttpContextAccessor _accessor;

		public UsersAppService(  IHttpContextAccessor accessor)
		{
 
			_accessor = accessor;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public bool IsAuthenticated()
		{
			return  _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;


		}



		public async Task<UserViewModel> GetUserByIdAsync(Guid userId)
		{
			// To Implement

			//var map = _mapper.Map<UserViewModel>(result);

			//return map;

			return new UserViewModel();
		}

		public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
		{
			// To Implement

			//var map = _mapper.Map<IEnumerable<UserViewModel>>(result);

			//return map;

			return new List<UserViewModel>();
		}

		public async Task<UserViewModel> GetCurrentUserAsync()
		{
			if (!IsAuthenticated()) return new UserViewModel();

			var result = new UserViewModel()
			{
				Email = _accessor.HttpContext?.User?.Claims.First(x => x.Type == "preferred_username")?.Value,
				Name = _accessor.HttpContext?.User?.Claims.First(x => x.Type == "name")?.Value,

			};

			return result;
			

		}

		public string GetUserName()
		{

			if (!IsAuthenticated()) return string.Empty; 

			return _accessor.HttpContext?.User?.Claims.First(x=> x.Type == "name")?.Value ?? "Usuário não encontrado";
		}

		public string GetTenantId()
		{
			if (!IsAuthenticated()) return string.Empty;

			return _accessor.HttpContext?.User?
				.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value ?? "Tenant não encontrado";
		}
	}
}
