namespace Application.DTOs.ReservationDTOs;

public class CreateReservationDto
{
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }
}
