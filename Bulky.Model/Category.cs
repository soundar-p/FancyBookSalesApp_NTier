using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [StringLength(30, ErrorMessage ="String length should be maximum 30chars")]
        public string Name { get; set; }

        [DisplayName("Category Description")]
        public string Description { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage="Range should be between 1 and 100")]
        public int DisplayOrder { get; set; } 

    }
}
