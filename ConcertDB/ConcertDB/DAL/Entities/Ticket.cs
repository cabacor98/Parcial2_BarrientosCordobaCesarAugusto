using System.ComponentModel.DataAnnotations;

namespace ConcertDB.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Display(Name ="Numero de ticket")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Id { get; set; }

        [Display(Name = "Fecha concierto")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "Usada")]
        public bool? IsUsed { get; set; }

        [Display(Name = "Puerta de entrada")]
        public string? EntranceGate { get; set; }

    }
}
