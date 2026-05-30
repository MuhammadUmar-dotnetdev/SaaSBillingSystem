using System;
using System.Collections.Generic;
using System.Text;

namespace SaaSBillingSystem.Application.Features.Users.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
