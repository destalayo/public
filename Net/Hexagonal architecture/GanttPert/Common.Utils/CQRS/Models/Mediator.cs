using Common.Utils.CQRS.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.CQRS.Models
{
    public class Mediator : IMediator
    {
        IServiceProvider _provider;
        public Mediator(IServiceProvider provider)
        {
            _provider = provider;
        }
        public async Task CommandAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _provider.GetRequiredService<ICommandHandler<TCommand>>().HandleAsync(command);
        }
        public async Task<TResult> CommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            return await _provider.GetRequiredService<ICommandHandler<TCommand, TResult>>().HandleAsync(command);
        }
        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            return await _provider.GetRequiredService<IQueryHandler<TQuery, TResult>>().HandleAsync(query);
        }
    }
}
