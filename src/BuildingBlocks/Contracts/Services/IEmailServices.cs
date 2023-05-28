namespace Contracts.Services
{
    public interface IEmailServices<in T> where T : class
    {
        Task SendEmailServices(T request, CancellationToken cancellationToken = new CancellationToken());
    }
}