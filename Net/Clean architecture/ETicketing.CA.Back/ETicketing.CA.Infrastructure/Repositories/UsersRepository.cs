
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETicketing.CA.Infrastructure.Repositories
{
    public class UsersRepository: BaseRepository<User, string>, IUsersRepository
    {        
        public UsersRepository(IUnitOfWork unitOfWork): base(unitOfWork) { }
        
    }
}
