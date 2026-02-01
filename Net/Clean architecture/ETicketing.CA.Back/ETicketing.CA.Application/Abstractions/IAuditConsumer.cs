using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.Abstractions
{
    public interface IAuditConsumer
    {
        Task ConsumeAsync(CancellationToken cancellationToken);
    }

}
