using ETicketing.CA.Domain.Abstractions.Models;
using ETicketing.CA.Domain.Models.Reservations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Domain.Models.Sessions
{
    public class Session
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpires { get; set; }


        public static Session Create(string accessToken, string refreshToken)
        {
            return new Session { AccessToken = accessToken, RefreshToken = refreshToken, AccessTokenExpires = DateTime.UtcNow.AddMinutes(20) };
        }
    }
}
