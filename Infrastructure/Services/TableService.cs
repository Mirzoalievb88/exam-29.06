using System.Net;
using Application.DTOs.TableDTOs;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TableService(DataContext context) : ITableService
{
    public async Task<Response<string>> CreateTableAsync(CreateTableDto create)
    {
        var table = new Tables
        {
            Number = create.Number,
            Seats = create.Seats,
            IsReserved = create.IsReserved
        };

        context.Tables.Add(table);
        await context.SaveChangesAsync();

        return new Response<string>(default!, "Столик успешно создан");
    }

    public async Task<Response<List<GetTableDto>>> GetAllTablesAsync()
    {
        var tables = await context.Tables
            .AsNoTracking()
            .Select(t => new GetTableDto
            {
                Id = t.Id,
                Number = t.Number,
                Seats = t.Seats,
                IsReserved = t.IsReserved
            })
            .ToListAsync();

        return new Response<List<GetTableDto>>(tables, "All Worked");
    }

    public async Task<Response<string>> UpdateTableAsync(int id, UpdateTableDto update)
    {
        var table = await context.Tables.FindAsync(id);
        if (table == null)
            return new Response<string>("Столик не найден", HttpStatusCode.NotFound);

        table.Number = update.Number;
        table.Seats = update.Seats;
        table.IsReserved = update.IsReserved;

        context.Tables.Update(table);
        await context.SaveChangesAsync();

        return new Response<string>(default!, "Столик успешно обновлён");
    }

    public async Task<Response<string>> DeleteTableAsync(int id)
    {
        var table = await context.Tables.FindAsync(id);
        if (table == null)
            return new Response<string>("Столик не найден", HttpStatusCode.NotFound);

        context.Tables.Remove(table);
        await context.SaveChangesAsync();

        return new Response<string>(default!, "Столик удалён");
    }
}
