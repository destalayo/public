using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Abstractions.Models
{
    public class AuditDTO
    {
        public string UserId { get; set; }
        public string Method { get; set; }
        public DateTime StartDateTime { get; set; }
        public string? Log { get; set; }
        public bool HasError { get; set; }
        public DateTime EndDateTime { get; set; }
        public ConcurrentBag<AuditDTO> Children { get; set; } = [];
        public List<AuditEntityDTO> Entities { get; set; } = [];
    }
}
