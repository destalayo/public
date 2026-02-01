using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Abstractions.Models
{
    public class AuditEntityDTO
    {
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public EntityChangeType EntityChangeType { get; set; }
        public List<AuditChangeDTO> Changes { get; set; } = [];
    }
    public enum EntityChangeType
    {
        Added,
        Updated,
        Deleted,
    }
}
