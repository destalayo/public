using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.Abstractions
{
    public interface ITokenService
    {
        string CreateAccessToken(string id);
        string CreateRefreshToken(string id);
        string ValidateRefreshToken(string refreshToken);
    }
}
