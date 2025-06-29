using Application.DTOs.ReservationDTOs;
using Domain.ApiResponse;

namespace Infrastructure.Interfaces;

public interface IReservationService
{
    Task<Response<string>> CreateReservationAsync(CreateReservationDto create);
    Task<Response<List<GetReservationDto>>> GetAllReservationsAsync();
    Task<Response<string>> UpdateReservationAsync(int id, UpdateReservationDto update);
    Task<Response<string>> DeleteReservationAsync(int id);
    Task<Response<GetReservationDto>> GetReservationByIdAsync(int id);
}
