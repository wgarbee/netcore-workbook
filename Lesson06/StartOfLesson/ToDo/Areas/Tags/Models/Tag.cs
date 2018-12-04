using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Areas.Tags.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public Tenant Tenant { get; set; }

        public string Value { get; set; }
    }
}
