using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public interface IReadOnlyToDoContext
    {
        IQueryable<ToDo> ToDos { get; }

        IQueryable<Status> Statuses { get; }
    }
}
