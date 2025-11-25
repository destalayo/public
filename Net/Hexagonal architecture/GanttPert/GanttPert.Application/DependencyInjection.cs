using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using Common.Utils.Tools.Models;
using GanttPert.Application.Features.CreateFeature;
using GanttPert.Application.Features.DeleteFeature;
using GanttPert.Application.Features.ReadFeature;
using GanttPert.Application.Features.ReadFeatures;
using GanttPert.Application.Features.ReadUserFeatures;
using GanttPert.Application.Features.UpdateFeature;
using GanttPert.Application.Features.UpdateFeatureTask;
using GanttPert.Application.Tasks.CreateTask;
using GanttPert.Application.Tasks.DeleteTask;
using GanttPert.Application.Tasks.ReadFeatureTasks;
using GanttPert.Application.Tasks.ReadTask;
using GanttPert.Application.Tasks.ReadUserTasks;
using GanttPert.Application.Tasks.UpdateTask;
using GanttPert.Application.Users.CreateUser;
using GanttPert.Application.Users.DeleteUser;
using GanttPert.Application.Users.ReadUser;
using GanttPert.Application.Users.ReadUsers;
using GanttPert.Application.Users.UpdateUser;
using GanttPert.Application.Users.UpdateUserTask;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICommandHandler<CreateUserCommand, int>, CreateUserHandler>();
            services.AddScoped<ICommandHandler<DeleteUserCommand>, DeleteUserHandler>();
            services.AddScoped<IQueryHandler<ReadUserQuery, User>, ReadUserHandler>();
            services.AddScoped<IQueryHandler<ReadUsersQuery, IEnumerable<User>>, ReadUsersHandler>();
            services.AddScoped<ICommandHandler<UpdateUserCommand>, UpdateUserHandler>();
            services.AddScoped<ICommandHandler<UpdateUserTaskCommand>, UpdateUserTaskHandler>();

            services.AddScoped<ICommandHandler<CreateTaskCommand, int>, CreateTaskHandler>();
            services.AddScoped<ICommandHandler<DeleteTaskCommand>, DeleteTaskHandler>();
            services.AddScoped<IQueryHandler<ReadTaskQuery, Domain.Models.Tasks.Task>, ReadTaskHandler>();
            services.AddScoped<IQueryHandler<ReadUserTasksQuery, IEnumerable<Domain.Models.Tasks.Task>>, ReadUserTasksHandler>();
            services.AddScoped<IQueryHandler<ReadFeatureTasksQuery, IEnumerable<Domain.Models.Tasks.Task>>, ReadFeatureTasksHandler>();
            services.AddScoped<ICommandHandler<UpdateTaskCommand>, UpdateTaskHandler>();

            services.AddScoped<ICommandHandler<CreateFeatureCommand, int>, CreateFeatureHandler>();
            services.AddScoped<ICommandHandler<DeleteFeatureCommand>, DeleteFeatureHandler>();
            services.AddScoped<IQueryHandler<ReadFeatureQuery, Feature>, ReadFeatureHandler>();
            services.AddScoped<IQueryHandler<ReadFeaturesQuery, IEnumerable<Feature>>, ReadFeaturesHandler>();
            services.AddScoped<IQueryHandler<ReadUserFeaturesQuery, IEnumerable<Feature>>, ReadUserFeaturesHandler>();
            services.AddScoped<ICommandHandler<UpdateFeatureCommand>, UpdateFeatureHandler>();
            services.AddScoped<ICommandHandler<UpdateFeatureTaskCommand>, UpdateFeatureTaskHandler>();
            return services;
        }
    }
}
