using ETicketing.CA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.Abstractions
{
    public interface ISessionService 
    { 
        Task<User> ReadCurrentUser(); 
    }
}
