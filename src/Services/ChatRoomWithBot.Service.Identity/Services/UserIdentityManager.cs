using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Service.Identity.Interfaces;
using ChatRoomWithBot.Service.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Domain.OperationResults;

namespace ChatRoomWithBot.Service.Identity.Services
{
    public class UserIdentityManager :  IUserIdentityManager, IDisposable
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IBerechitLogger _berechitLogger;


        public UserIdentityManager(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, IBerechitLogger berechitLogger)  
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _berechitLogger = berechitLogger;
        }

        public async Task<OperationResult<SignInResult>> Login(LoginViewModel model)
        {

            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return new OperationResult<SignInResult>(SignInResult.Failed);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);


                if (!result.Succeeded) return new OperationResult<SignInResult>(SignInResult.Failed);


                _berechitLogger.Information("User logged in.");
                return new OperationResult<SignInResult>(SignInResult.Success);


            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);

                return new OperationResult<SignInResult>("User or password is not valid !");

            }



        }

        public async Task<OperationResult<IdentityResult>> Register(RegisterViewModel model)
        {

            try
            {
                var user = new UserIdentity()
                {

                    Email = "user1@teste.com",
                    UserName = "user1@teste.com",
                    Name = "Jane Doe",
                    EmailConfirmed = true,

                };


                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return new OperationResult<IdentityResult>(IdentityResult.Success);
                }

                var msg = result.Errors.Aggregate(string.Empty, (current, error) => current + (error.Description + @"\"));
                _berechitLogger.Error(msg); 


                return new OperationResult<IdentityResult>(IdentityResult.Failed( ));

            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
                return new OperationResult<IdentityResult>(IdentityResult.Failed());

            }


        }

        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
