namespace Customer.API.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<IResult> GetCustomerByUserNameAsync(string userName);

        Task<IResult> GetCustomers();
    }
}
