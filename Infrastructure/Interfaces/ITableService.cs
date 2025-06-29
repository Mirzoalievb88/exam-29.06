using Application.DTOs.TableDTOs;
using Domain.ApiResponse;

namespace Infrastructure.Interfaces;

public interface ITableService
{
    Task<Response<string>> CreateTableAsync(CreateTableDto create);
    Task<Response<List<GetTableDto>>> GetAllTablesAsync();
    Task<Response<string>> UpdateTableAsync(int id, UpdateTableDto update);
    Task<Response<string>> DeleteTableAsync(int id);
}