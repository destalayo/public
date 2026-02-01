using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Sessions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Sessions.UpdateSession
{
    public record UpdateSessionCommand(string RefreshToken) : ICommand<Session>;
    public class UpdateSessionHandler(IDbFactory _db, ITokenService _token) : ICommandHandler<UpdateSessionCommand, Session>
    {
        async public Task<Session> HandleAsync(UpdateSessionCommand command)
        {
            string userId = _token.ValidateRefreshToken(command.RefreshToken);

            return Session.Create(_token.CreateAccessToken(userId), _token.CreateRefreshToken(userId));
        }
    }
}
