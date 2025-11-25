using GanttPert.Domain.Abstractions.Classes;
using GanttPert.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Domain.Models.Users
{
    public interface IUsersRepository : IRepository<User, int>
    {
    }
}
