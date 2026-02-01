using ETicketing.CA.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Seats
{
    public interface ISeatsRepository: IRepository<Seat, Guid>
    {
        Task<List<Seat>> GetAllByRoomIdAsync(Guid roomId);
        Task<List<Guid>> GetAllReservedBySeasonIdAsync(Guid seasonId);
        
    }
}
