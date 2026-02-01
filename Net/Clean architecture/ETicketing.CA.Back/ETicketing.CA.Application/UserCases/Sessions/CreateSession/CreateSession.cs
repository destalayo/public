using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Sessions;
using ETicketing.CA.Domain.Models.Users;

namespace ETicketing.CA.Application.UserCases.Sessions.CreateSession
{
    public record CreateSessionCommand(string Id) : ICommand<Session>;
    public class CreateSessionHandler(IDbFactory _db, ITokenService _token) : ICommandHandler<CreateSessionCommand, Session>
    {
        async public Task<Session> HandleAsync(CreateSessionCommand command)
        {
            return Session.Create(_token.CreateAccessToken(command.Id), _token.CreateRefreshToken(command.Id));
        }
    }
}
