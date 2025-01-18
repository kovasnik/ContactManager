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
        [Column("name")]
        public string Name { get; set; }
        [Column("date_of_birth")]
        public DateOnly DateOfBirth { get; set; }
        [Column("is_married")]
        public bool IsMarried { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("salary")]
        public decimal Salary { get; set; }

    }
}
