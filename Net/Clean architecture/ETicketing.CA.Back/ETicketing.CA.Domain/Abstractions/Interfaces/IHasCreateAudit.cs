using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Abstractions.Interfaces
{
    public interface IHasCreateAudit
    {
        public DateTime CreationTime { get; set; }

        public string? CreatorId { get; set; }
    }
}
