namespace Domain.Entities;

public class Reservation
{
    public int Id { get; set; }

    public int TableId { get; set; }
    public Tables Table { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public DateTime ReservationDate { get; set; }
}
