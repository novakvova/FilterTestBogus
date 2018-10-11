using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LaptopWebSite.Models.Entities
{
    [Table("tblCategories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        [ForeignKey("CategoryOf")]
        public int ? ParentId { get; set; }
        public Category CategoryOf { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }
}