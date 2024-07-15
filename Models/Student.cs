using System.ComponentModel.DataAnnotations;

namespace CoreCrud.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }
    }
}
