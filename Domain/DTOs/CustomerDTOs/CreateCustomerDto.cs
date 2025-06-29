using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CustomerDTOs;

public class CreateCustomerDto
{
    [Required(ErrorMessage = "ФИО обязательно для заполнения")]
    public string FullName { get; set; }
    public string Phone { get; set; }
}
