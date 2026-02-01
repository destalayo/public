using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Reservations
{
    public interface IReservationsRepository: IRepository<Reservation, Guid>
    {
        Task<List<Reservation>> GetAllBySeasonBySeatIdsAsync(Guid seasonId, List<Guid> seatIds);
    }
}
