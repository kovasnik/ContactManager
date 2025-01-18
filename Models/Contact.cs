using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace ContactManager.Models
{
    public class Contact
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("date_of_birth")]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [Column("is_married")]
        public bool IsMarried { get; set; }
        [Required]
        [Column("phone")]
        public string Phone { get; set; }
        [Required]
        [Column("salary")]
        public decimal Salary { get; set; }

    }
}
