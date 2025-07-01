namespace myapi.Interfaces
{
    public interface IEmailServices
    {
        Task<bool> SendMailAsync(string toEmail, string subject, string body);
    }
}