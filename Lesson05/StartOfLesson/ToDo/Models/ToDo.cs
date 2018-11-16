using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }

        [UIHint("Status")]
        public Status Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Created { get; set; }
    }
}
