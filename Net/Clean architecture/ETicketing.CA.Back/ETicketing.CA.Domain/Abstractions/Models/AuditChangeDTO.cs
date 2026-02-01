using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Abstractions.Models
{
    public class AuditChangeDTO
    {
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string FieldName { get; set; }
    }
}
