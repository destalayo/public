using ETicketing.CA.Domain.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.Abstractions
{
    public interface IAuditProducer
    {
        Task SendAsync(AuditDTO audit);
    }

}
