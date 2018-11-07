using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        // Known issue with this attribute is that it does not work
        //[InverseProperty(nameof(ToDo.StatusId))]
        public List<ToDo> ToDos { get; set; }
    }
}
