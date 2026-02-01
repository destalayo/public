using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ETicketing.CA.Infrastructure.Services
{
    public class SessionApiService : ISessionService
    {
        private readonly IHttpContextAccessor _http;
        private readonly IUsersRepository _repo;
        private User CurrentUser { get; set; }
        public SessionApiService(IHttpContextAccessor http, IUsersRepository repo)
        {
            _http = http; _repo = repo;
        }
        public async Task<User> ReadCurrentUser()
        {
            if (CurrentUser != null)
            {
                return CurrentUser;
            }

            var userId = _http.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new Exception("User not authenticated");

            CurrentUser = await _repo.GetByIdAsync(userId);
            return CurrentUser;
        }
    }
}
