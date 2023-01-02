using Customer.API.Services.Interfaces;

namespace Customer.API.Controllers
{
    public static class CustomerController
    {
        public static void MapCustomerAPI(this WebApplication app)
        {
            app.MapGet("/api/customers/{username}",
               async (string username, ICustomerServices customerService) =>
               {
                   var result = await customerService.GetCustomerByUserNameAsync(username);
                   return result != null ? result : Results.NotFound();
               });
            app.MapGet("/", () => "Welcome to Customer API");
            app.MapGet("/api/customers", (ICustomerServices customerServices) => customerServices.GetCustomers());
        }
    }
}