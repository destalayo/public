using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Abstractions.Interfaces
{
    public interface IHasDeleteAudit
    {
        public bool IsDeleted { get; set; }

        public string? DeleterId { get; set; }

        public DateTime? DeletionTime { get; set; }
    }
}
