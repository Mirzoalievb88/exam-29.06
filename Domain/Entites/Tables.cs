namespace Domain.Entities;

public class Tables
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int Seats { get; set; }
    public bool IsReserved { get; set; }

    public List<Reservation> Reservations { get; set; }
}
