using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Areas.Tags.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
