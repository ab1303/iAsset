using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IAsset.Data.Models
{
    public class Weather : BaseEntity
    {
        [Key]
        public int WeatherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public decimal Amount { get; set; }

    }
}
