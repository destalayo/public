using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Abstractions.Models
{
    public class Entity<T>
    {
        public T Id { get; set; }
    }
}
