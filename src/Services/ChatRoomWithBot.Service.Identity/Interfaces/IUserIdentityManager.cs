using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Service.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ChatRoomWithBot.Service.Identity.Interfaces;

public interface IUserIdentityManager
{
    Task<OperationResult<SignInResult>> Login(LoginViewModel model);
    
    Task<OperationResult<IdentityResult>> Register(RegisterViewModel model );

    Task<OperationResult<bool>> ConfirmEmail(Guid userId, string code);

    Task<OperationResult<ForgotPasswordViewModel>> ForgotPassword(ForgotPasswordViewModel model);

    Task<OperationResult<ResetPasswordViewModel>> ResetPassword(ResetPasswordViewModel model);



}