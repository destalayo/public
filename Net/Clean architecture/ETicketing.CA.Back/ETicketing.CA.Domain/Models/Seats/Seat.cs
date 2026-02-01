using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Seats
{
    public class Seat : Entity<Guid>, IHasCreateAudit, IHasUpdateAudit, IFullAudit
    {
        public DateTime CreationTime { get; set; }
        public string? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? LastModifierId { get; set; }



        public int Column { get; set; }
        public int Row { get; set; }
        public Guid RoomId { get; set; }

        public Room Room { get; set; }
        public List<Reservation> Reservations { get; set; } = [];
        public static Seat Create(Guid roomId, int row, int column)
        {
            return new Seat { Id = Guid.NewGuid(), Row = row, Column = column, RoomId=roomId };
        }
    }
}
