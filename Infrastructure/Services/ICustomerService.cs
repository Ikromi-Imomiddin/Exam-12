using Domain.Dtos;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Task<AddCustomerDto> AddCustomer(AddCustomerDto CustomerDto);
    Task<bool> DeleteCustomer(int id);
    Task<AddCustomerDto> UpdateCustomer(AddCustomerDto customerDto);
    Task<AddCustomerDto> GetCustomerById(int id);
    Task<List<GetCustomerDto>> GetCustomers();
}
