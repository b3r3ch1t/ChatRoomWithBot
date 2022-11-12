﻿using System.ComponentModel.DataAnnotations;

namespace ChatRoomWithBot.Service.Identity.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "EMAIL_REQUIRED")]
        [EmailAddress(ErrorMessage = "EMAIL_INVALID")]
        public string Email { get; set; }

        public string Code { get; set; }
    }
}
