using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Reservations
{
    public class Reservation : Entity<Guid>, IHasCreateAudit, IHasUpdateAudit, IFullAudit
    {
        public DateTime CreationTime { get; set; }
        public string? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? LastModifierId { get; set; }



        public string UserId { get; set; }
        public Guid SeatId { get; set; }
        public Guid SeasonId { get; set; }

        public Season Season { get; set; }
        public Seat Seat { get; set; }
        public User User { get; set; }

        public static Reservation Create(string UserId, Guid SeasonId, Guid SeatId)
        {
            return new Reservation {Id=Guid.NewGuid(), UserId = UserId, SeasonId=SeasonId, SeatId= SeatId };
        }
    }
}
