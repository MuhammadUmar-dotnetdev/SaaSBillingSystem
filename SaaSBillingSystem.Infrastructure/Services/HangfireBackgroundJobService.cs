using Hangfire;
using SaaSBillingSystem.Application.Interfaces;
using System.Linq.Expressions;

namespace SaaSBillingSystem.Infrastructure.Services
{
    public class HangfireBackgroundJobService: IBackgroundJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireBackgroundJobService(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public string Enqueue<T>(Expression<Func<T, Task>> methodCall)
        {
            return _backgroundJobClient.Enqueue(methodCall);
        }
    }
}
