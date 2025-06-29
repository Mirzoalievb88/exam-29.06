using System.Net;
using Application.DTOs.CustomerDTOs;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CustomerService(DataContext context) : ICustomerService
{
    public async Task<Response<string>> CreateCustomerAsync(CreateCustomerDto create)
    {
        var customer = new Customer
        {
            FullName = create.FullName,
            Phone = create.Phone
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        return new Response<string>(default!, "Клиент успешно создан");
    }

    public async Task<Response<List<GetCustomerDto>>> GetAllCustomersAsync()
    {
        var customers = await context.Customers
            .AsNoTracking()
            .Select(c => new GetCustomerDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Phone = c.Phone
            })
            .ToListAsync();

        return new Response<List<GetCustomerDto>>(customers, "All Worked");
    }

    public async Task<Response<string>> UpdateCustomerAsync(int id, UpdateCustomerDto update)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null)
            return new Response<string>("Клиент не найден", HttpStatusCode.NotFound);

        customer.FullName = update.FullName;
        customer.Phone = update.Phone;

        context.Customers.Update(customer);
        await context.SaveChangesAsync();

        return new Response<string>(default!, "Клиент обновлён");
    }

    public async Task<Response<string>> DeleteCustomerAsync(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null)
            return new Response<string>("Клиент не найден", HttpStatusCode.NotFound);

        context.Customers.Remove(customer);
        await context.SaveChangesAsync();

        return new Response<string>(default!, "Клиент удалён");
    }

    public async Task<Response<GetCustomerDto>> GetCustomerByIdAsync(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null)
        {
            return new Response<GetCustomerDto>("Customer not found", HttpStatusCode.NotFound);
        }

        var cus = new GetCustomerDto
        {
            FullName = customer.FullName,
            Phone = customer.Phone,
        };

        await context.SaveChangesAsync();

        return new Response<GetCustomerDto>(default!, "All Worked");
    }

}
