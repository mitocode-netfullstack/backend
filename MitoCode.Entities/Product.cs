using System.ComponentModel.DataAnnotations;

namespace mitocode.netfullstack.entities
{
    public class Product : EntityBase
    {
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }        
        public bool Enabled { get; set; }

    }
}