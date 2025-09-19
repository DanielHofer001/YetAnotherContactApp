using System.ComponentModel.DataAnnotations;

namespace YetAnotherContactApp.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [MaxLength(50)] 
        public string Name { get; set; }

        [StringLength(10)] 
        public string Phone { get; set; }

    }
}
