using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Seasons
{
    public interface ISeasonsRepository : IRepository<Season, Guid>
    {
    }
}
