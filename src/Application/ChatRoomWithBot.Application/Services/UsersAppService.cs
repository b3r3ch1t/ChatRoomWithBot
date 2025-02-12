using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace ChatRoomWithBot.Application.Services
{
    public class UsersAppService : IUsersAppService
    {


        private readonly IHttpContextAccessor _accessor;

        private readonly GraphServiceClient _graphServiceClient;

        private readonly IBerechitLogger _berechitLogger;

        public UsersAppService(IHttpContextAccessor accessor, GraphServiceClient graphServiceClient, IBerechitLogger berechitLogger)
        {
            _accessor = accessor;
            _graphServiceClient = graphServiceClient;
            _berechitLogger = berechitLogger;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;


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
            try
            {
                var allUsers = new List<User>();

                var users = (await _graphServiceClient.Users
                    .GetAsync((requestConfiguration) =>
                    {
                        requestConfiguration.QueryParameters.Top = 15;
                    }));


                allUsers.AddRange(users.Value);

                Console.WriteLine($"`{DateTime.Now}==> {allUsers.Count}");

                while (users.OdataNextLink != null)
                {
                    users = await _graphServiceClient.Users
                        .WithUrl(users.OdataNextLink)
                        .GetAsync((requestConfiguration) =>
                        {
                            requestConfiguration.QueryParameters.Top = 15;

                        });
                    allUsers.AddRange(users.Value);

                    Console.WriteLine($"`{DateTime.Now}==> {allUsers.Count}");
                }

                var result = allUsers.Select(x => new UserViewModel()
                {
                    Name = x.DisplayName ?? string.Empty,
                    TenantId = x.Id,
                    Email = x.UserPrincipalName ?? string.Empty
                });



                return result;

            }
            catch (Exception e)
            {
                return Enumerable.Empty<UserViewModel>();
            }
        }

        public async Task<UserViewModel> GetCurrentUserAsync()
        {
            if (!IsAuthenticated()) return new UserViewModel();

            var result = new UserViewModel
            {
                Email = _accessor.HttpContext?.User?.Claims.First(x => x.Type == "preferred_username")?.Value,
                Name = _accessor.HttpContext?.User?.Claims.First(x => x.Type == "name")?.Value,
                TenantId = _accessor.HttpContext?.User?
                    .FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value

            };

            return result;


        }

        public string GetUserName()
        {

            if (!IsAuthenticated()) return string.Empty;

            return _accessor.HttpContext?.User?.Claims.First(x => x.Type == "name")?.Value ?? "Usuário não encontrado";
        }

        public string GetTenantId()
        {
            if (!IsAuthenticated()) return string.Empty;

            return _accessor.HttpContext?.User?
                .FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value ?? "Tenant não encontrado";
        }

        public async Task<IEnumerable<ChatRoom>> GetChats()
        {
            try
            {
                var result = new List<ChatRoom>();

                var chatRoomDefault = ChatRoom.GetChatRoomDefault();

                result.Add(chatRoomDefault);

                if (!IsAuthenticated()) return new List<ChatRoom>();

                var groups = (await _graphServiceClient
                    .Groups
                    .GetAsync())?.Value;


                result.AddRange(groups
                    .Where(x => !string.IsNullOrWhiteSpace(x.Id) && !string.IsNullOrWhiteSpace(x.DisplayName))
                    .Select(x => new ChatRoom(Guid.Parse(x.Id), x.DisplayName)));

                return result.OrderBy(x => x.Name);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ChatRoom?> GetChat(Guid id)
        {
            try
            {
                var chats = await GetChats();
                var result = chats.FirstOrDefault(x => x.Id == id);

                return result ?? null;
            }
            catch (Exception ex)
            {
                _berechitLogger.Error(ex);
                return null;
            }
        }

        public async Task<IEnumerable<AuditModel>> GetAudits()
        {
            try
            {
                var allLogs = new List<SignIn>();


                var signInLogs = (await _graphServiceClient.AuditLogs
                    .SignIns
                    .GetAsync((requestConfiguration) =>
                    {
                        requestConfiguration.QueryParameters.Top = 15;
                    }));


                allLogs.AddRange(signInLogs.Value);

                Console.WriteLine($"`{DateTime.Now}==> {allLogs.Count}");

                while (signInLogs.OdataNextLink != null)
                {
                    signInLogs = await _graphServiceClient.AuditLogs
                        .SignIns
                        .WithUrl(signInLogs.OdataNextLink)
                        .GetAsync((requestConfiguration) =>
                        {
                            requestConfiguration.QueryParameters.Top = 15;

                        });
                    allLogs.AddRange(signInLogs.Value);

                    Console.WriteLine($"`{DateTime.Now}==> {allLogs.Count}");
                }

                var result = allLogs.Select(x => new AuditModel()
                {
                    Id = x.Id,
                    IpAddress = x.IpAddress,
                    Status = x.Status.ToString(),
                    UserId = x.UserId,
                    CreatedDateTime = x.CreatedDateTime,

                });



                return result;

            }
            catch (Exception e)
            {
                return Enumerable.Empty<AuditModel>();
            }
        }

        public  async Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync()
        {

            try
            {
                var listGroups = new List<Group>();

                var groups = (await _graphServiceClient.Groups 
                    .GetAsync((requestConfiguration) =>
                    {
                        requestConfiguration.QueryParameters.Top = 15;
                    }));


                listGroups.AddRange(groups.Value );

                Console.WriteLine($"`{DateTime.Now}==> {listGroups.Count}");

                while (groups.OdataNextLink != null)
                {
                    groups = await _graphServiceClient.Groups 
                        .WithUrl(groups.OdataNextLink)
                        .GetAsync((requestConfiguration) =>
                        {
                            requestConfiguration.QueryParameters.Top = 15;

                        });
                    listGroups.AddRange(groups.Value);

                    Console.WriteLine($"`{DateTime.Now}==> {listGroups.Count}");
                }

                var result = listGroups.Select(x => new GroupViewModel()
                {
                    Id = x.Id,
                    Description = x.DisplayName?? string.Empty,

                });



                return result;

            }
            catch (Exception e)
            {
                return Enumerable.Empty<GroupViewModel>();
            }
        }

         
    }
}
