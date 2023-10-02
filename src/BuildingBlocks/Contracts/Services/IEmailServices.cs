namespace Contracts.Services
{
    public interface IEmailServices<in T> where T : class
    {
        Task SendEmailAsync(T request, CancellationToken cancellationToken = new CancellationToken());
    }
}