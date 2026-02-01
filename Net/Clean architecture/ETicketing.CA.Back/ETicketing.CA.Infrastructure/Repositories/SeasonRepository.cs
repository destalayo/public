
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Seats;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETicketing.CA.Infrastructure.Repositories
{
    public class SeasonsRepository: BaseRepository<Season, Guid>, ISeasonsRepository
    {        
        public SeasonsRepository(IUnitOfWork unitOfWork): base(unitOfWork) { }
        
    }
}
