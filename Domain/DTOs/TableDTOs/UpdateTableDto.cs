namespace Application.DTOs.TableDTOs;

public class UpdateTableDto
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int Seats { get; set; }
    public bool IsReserved { get; set; }
}
