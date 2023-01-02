using AutoMapper;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Shared.DTOs.Customer;

namespace Customer.API.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerServices(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResult> GetCustomerByUserNameAsync(string userName)
        {
            var entity = await _repository.GetCustomerByUserNameAsync(userName);
            var result = _mapper.Map<CustomerDto>(entity);

            return Results.Ok(result);
        }

        public async Task<IResult> GetCustomers()
        {
            var entities = await _repository.GetCustomersAsync();
            var result = _mapper.Map<IEnumerable<CustomerDto>>(entities);
            return Results.Ok(result);
        }
    }
}