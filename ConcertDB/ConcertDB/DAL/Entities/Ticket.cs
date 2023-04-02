using System.ComponentModel.DataAnnotations;

namespace ConcertDB.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Display(Name ="Ticket Number")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Guid Id { get; set; }

        public DateTime? UseDate { get; set; }

        public bool? IsUsed { get; set; }

        public string? EntraceGate { get; set; }

    }
}
