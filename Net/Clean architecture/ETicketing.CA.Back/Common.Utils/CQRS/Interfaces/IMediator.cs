using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.CQRS.Interfaces
{
    public interface IMediator
    {
        Task CommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> CommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
        Task<TResult> QueryAsync<TQuery,TResult>(TQuery query) where TQuery:IQuery<TResult>;
    }
}
