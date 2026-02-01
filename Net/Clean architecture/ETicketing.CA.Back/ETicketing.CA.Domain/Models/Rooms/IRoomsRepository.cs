using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Rooms
{
    public interface IRoomsRepository : IRepository<Room, Guid>
    {
    }
}
