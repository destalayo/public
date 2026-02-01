using ETicketing.CA.Domain.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.Services
{
    public interface IAuditProcessor
    {
        Task ProcessAsync(AuditDTO audit, CancellationToken ct);
    }

}
