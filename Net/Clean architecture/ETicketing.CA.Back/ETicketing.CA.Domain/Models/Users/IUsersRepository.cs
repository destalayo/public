using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Users
{
    public interface IUsersRepository: IRepository<User, string>
    {
            
    }
}
