using Application.DTOs.CustomerDTOs;
using Domain.ApiResponse;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Task<Response<string>> CreateCustomerAsync(CreateCustomerDto create);
    Task<Response<List<GetCustomerDto>>> GetAllCustomersAsync();
    Task<Response<string>> UpdateCustomerAsync(int id, UpdateCustomerDto update);
    Task<Response<string>> DeleteCustomerAsync(int id);
    Task<Response<GetCustomerDto>> GetCustomerByIdAsync(int id);
}
