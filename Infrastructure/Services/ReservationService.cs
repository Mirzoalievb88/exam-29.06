using System.Net;
using Application.DTOs.ReservationDTOs;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ReservationService(DataContext context) : IReservationService
{
    public async Task<Response<string>> CreateReservationAsync(CreateReservationDto create)
    {
        var table = await context.Tables.FindAsync(create.TableId);
        if (table == null)
            return new Response<string>("Столик не найден", HttpStatusCode.NotFound);

        var customer = await context.Customers.FindAsync(create.CustomerId);
        if (customer == null)
            return new Response<string>("Клиент не найден", HttpStatusCode.NotFound);

        bool conflict = await context.Reservations.AnyAsync(r =>
            r.TableId == create.TableId &&
            r.ReservationDate == create.ReservationDate);

        if (conflict)
            return new Response<string>("Этот столик уже забронирован на указанное время", HttpStatusCode.NotFound);

        var reservation = new Reservation
        {
            TableId = create.TableId,
            CustomerId = create.CustomerId,
            ReservationDate = create.ReservationDate
        };

        context.Reservations.Add(reservation);
        table.IsReserved = true;

        await context.SaveChangesAsync();

        return new Response<string>(default!, "Бронирование успешно создано");
    }

    public async Task<Response<List<GetReservationDto>>> GetAllReservationsAsync()
    {
        var reservations = await context.Reservations
            .Include(r => r.Table)
            .Include(r => r.Customer)
            .AsNoTracking()
            .Select(r => new GetReservationDto
            {
                Id = r.Id,
                TableId = r.TableId,
                CustomerId = r.CustomerId,
                ReservationDate = r.ReservationDate,
                TableNumber = r.Table.Number,
                CustomerFullName = r.Customer.FullName
            })
            .ToListAsync();

        return new Response<List<GetReservationDto>>(reservations, "All Worked");
    }

    public async Task<Response<string>> UpdateReservationAsync(int id, UpdateReservationDto update)
    {
        var reservation = await context.Reservations.FindAsync(id);
        if (reservation == null)
            return new Response<string>("Бронирование не найдено", HttpStatusCode.NotFound);

        reservation.TableId = update.TableId;
        reservation.CustomerId = update.CustomerId;
        reservation.ReservationDate = update.ReservationDate;

        context.Reservations.Update(reservation);
        await context.SaveChangesAsync();

        return new Response<string>(default!, "Бронирование обновлено");
    }

    public async Task<Response<string>> DeleteReservationAsync(int id)
    {
        var reservation = await context.Reservations.FindAsync(id);
        if (reservation == null)
            return new Response<string>("Бронирование не найдено", HttpStatusCode.NotFound);

        var table = await context.Tables.FindAsync(reservation.TableId);
        if (table != null)
            table.IsReserved = false;

        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync();

        return new Response<string>(default!, "Бронирование удалено");
    }

    public async Task<Response<GetReservationDto>> GetReservationByIdAsync(int id)
    {
        var res = await context.Reservations.FindAsync(id);
        if (res == null)
        {
            return new Response<GetReservationDto>("Customer not found", HttpStatusCode.NotFound);
        }

        var reser = new GetReservationDto
        {
            TableId = res.TableId,
            CustomerId = res.CustomerId,
            ReservationDate = res.ReservationDate,
        };

        await context.SaveChangesAsync();

        return new Response<GetReservationDto>(default!, "All Worked");
    }

}
