using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Rooms
{
    public class Room : Entity<Guid>, IHasCreateAudit, IHasUpdateAudit, IFullAudit
    {
        public DateTime CreationTime { get; set; }
        public string? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? LastModifierId { get; set; }



        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public List<Season> Seasons { get; set; } = [];

        public List<Seat> Seats { get; set; } = [];


        private Room() { }
        public void Update(string name)
        {
            Name = name;
        }
        public static Room Create(string name, int rows, int columns) 
        { 
            return new Room {Id=Guid.NewGuid(), Rows=rows, Columns=columns,Name=name}; 
        }
    }
}
