using System.ComponentModel.DataAnnotations;

namespace ABBY.MODELS
{
    public class FoodType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
     
    }
}
