namespace Application.DTOs.ReservationDTOs;

public class UpdateReservationDto
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }
}
