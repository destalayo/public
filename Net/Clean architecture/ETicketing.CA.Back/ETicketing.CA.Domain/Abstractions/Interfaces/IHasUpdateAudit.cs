using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Abstractions.Interfaces
{
    public interface IHasUpdateAudit
    {
        public DateTime? LastModificationTime { get; set; }

        public string? LastModifierId { get; set; }
    }
}
