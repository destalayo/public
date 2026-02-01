
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Rooms;
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
    public class ReservationsRepository: BaseRepository<Reservation, Guid>, IReservationsRepository
    {        
        public ReservationsRepository(IUnitOfWork unitOfWork): base(unitOfWork) { }
        async public Task<List<Reservation>> GetAllBySeasonBySeatIdsAsync(Guid seasonId, List<Guid> seatIds)
        {
            return await _unitOfWork.DbContext.Set<Reservation>().Where(x => x.SeasonId == seasonId && seatIds.Contains(x.SeatId))
                .ToListAsync();
        }
    }
}
