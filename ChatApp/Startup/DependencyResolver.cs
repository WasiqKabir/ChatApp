using BusinessServices.Services.Account;
using BusinessServices.Services.Chat;
using BusinessServices.Services.MessagesService;
using BusinessServices.Services.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Concurrent;

namespace ChatApp.Startup
{
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddSignalR();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors();

            services.AddScoped<IAccount, Account>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ConcurrentDictionary<string, string>>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IChatService, ChatService>();

            return services;
        }
    }
}
