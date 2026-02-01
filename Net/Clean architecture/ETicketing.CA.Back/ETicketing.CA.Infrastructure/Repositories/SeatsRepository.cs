
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Reservations;
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
    public class SeatsRepository: BaseRepository<Seat, Guid>, ISeatsRepository
    {        
        public SeatsRepository(IUnitOfWork unitOfWork): base(unitOfWork) { }
        public async Task<List<Seat>> GetAllByRoomIdAsync(Guid roomId)
        {
            return await _unitOfWork.DbContext.Set<Seat>().Where(x=>x.RoomId==roomId)
                .ToListAsync();
        }
        public async Task<List<Guid>> GetAllReservedBySeasonIdAsync(Guid seasonId)
        {

            return await _unitOfWork.DbContext.Set<Reservation>().Where(x => x.SeasonId == seasonId)
                .Select(x=>x.SeatId).ToListAsync();
        }
    }
}
