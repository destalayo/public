using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Seasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Users
{
    public class User : Entity<string>, IHasCreateAudit, IHasDeleteAudit, IFullAudit
    {
        public DateTime CreationTime { get; set; }
        public string? CreatorId { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }


        public string PasswordHash { get; set; }
        public List<Reservation> Reservations { get; set; } = [];

        public static User Create(string id, string hash)
        {
            return new User { Id = id.ToLower(), PasswordHash= hash};
        }
    }
}
