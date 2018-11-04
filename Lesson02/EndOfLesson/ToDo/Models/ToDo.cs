using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDoApp.ValidationAttributes;

namespace ToDoApp.Models
{
    public class ToDo : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [UIHint("Status")]
        public Status Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DateTimeBeforeToDayValidation]
        public DateTime? Created { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Title == this.Description)
                yield return new ValidationResult("Title and Description cannot be the same");
        }
    }
}
