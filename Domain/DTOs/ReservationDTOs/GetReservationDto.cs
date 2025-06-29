namespace Application.DTOs.ReservationDTOs;

public class GetReservationDto
{
    public int Id { get; set; }

    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int TableNumber { get; set; }
    public string CustomerFullName { get; set; } = string.Empty;
}
