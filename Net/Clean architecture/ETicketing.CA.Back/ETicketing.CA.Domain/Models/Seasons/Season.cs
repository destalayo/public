using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Seasons
{
    public class Season : Entity<Guid>, IHasCreateAudit, IHasUpdateAudit, IFullAudit
    {
        public DateTime CreationTime { get; set; }
        public string? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? LastModifierId { get; set; }



        public string Name { get; set; }
        public Guid RoomId { get; set; }

        public Room Room { get; set; }
        public List<Reservation> Reservations { get; set; } = [];

        public void Update(string name)
        {
            Name = name;
        }
        public static Season Create(Guid roomId, string name)
        {
            return new Season {Id=Guid.NewGuid(), RoomId=roomId, Name = name };
        }
    }
}
