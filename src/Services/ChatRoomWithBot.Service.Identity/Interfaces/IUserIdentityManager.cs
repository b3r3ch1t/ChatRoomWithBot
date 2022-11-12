﻿using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Service.Identity.ViewModels;

namespace ChatRoomWithBot.Service.Identity.Interfaces;

public interface IUserIdentityManager
{
    Task<OperationResult<LoginViewModel>> Login(LoginViewModel model);

    Task<OperationResult<LogoutViewModel>> Logout(LogoutViewModel model);

    Task<OperationResult<RegisterViewModel>> Register(RegisterViewModel model, string returnUrl);

    Task<OperationResult<bool>> ConfirmEmail(Guid userId, string code);

    Task<OperationResult<ForgotPasswordViewModel>> ForgotPassword(ForgotPasswordViewModel model);

    Task<OperationResult<ResetPasswordViewModel>> ResetPassword(ResetPasswordViewModel model);



}