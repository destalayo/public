using ETicketing.CA.Domain.Abstractions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ETicketing.CA.Application.Services
{
    public class AuditProcessor : IAuditProcessor
    {
        //private readonly IAuditRepository _repo;

        //public AuditProcessor(IAuditRepository repo)
        //{
        //    _repo = repo;
        //}

        public async Task ProcessAsync(AuditDTO audit, CancellationToken ct)
        {
            Console.WriteLine(JsonConvert.SerializeObject(audit));

            //// Lógica de negocio de auditoría
            //await _repo.SaveAsync(audit, ct);
        }
    }

}
