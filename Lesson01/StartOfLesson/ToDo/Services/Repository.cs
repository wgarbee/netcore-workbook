using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class Repository
    {
        public static Dictionary<int, Status> status = new Dictionary<int, Status>
        {
            { 1, new Status { Id = 1, Value = "Not Started" } },
            { 2, new Status { Id = 2, Value = "In Progress" } },
            { 3, new Status { Id = 3, Value = "Done" } }
        };

        public static List<ToDo> list = new List<ToDo>
        {
            new ToDo { Id = 1, Title = "My First ToDo", Description = "Get the app working", Status = status[2] }
        };

        public static ToDo GetToDoById(int id)
        {
            ToDo toDo = list.SingleOrDefault(t => t.Id == id);
            return toDo;
        }

        // get the current todo based on the id
        // once we get that, overwrite each property from collection
        public static void SaveToDo(int id, IFormCollection collection)
        {
            ToDo toDo = GetToDoById(id);

            toDo.Id = Convert.ToInt32(collection["Id"]);
            toDo.Description = collection["Description"];
            toDo.Title = collection["Title"];
            
            if (collection["Status.Value"] == "Not Started")
            {
                toDo.Status.Value = "Not Started";
                toDo.Status.Id = 1;
            }
            else if (collection["Status.Value"] == "In Progress")
            {
                toDo.Status.Value = "In Progress";
                toDo.Status.Id = 2;
            }
            else if (collection["Status.Value"] == "Done")
            {
                toDo.Status.Value = "Done";
                toDo.Status.Id = 3;
            }
        }

        // Do not need to get anything from the list
        // From the collection, create a new object of type ToDo and append values from collection
        // Add new todo to list
        public static void CreateToDo(IFormCollection collection)
        {
            ToDo toDo = new ToDo
            {
                Id = Convert.ToInt32(collection["Id"]),
                Description = collection["Description"],
                Title = collection["Title"],
                Status = status[1]
            };

            list.Add(toDo);
        }

        // Done, removes a ToDo from the list of ToDos
        public static void DeleteToDo(int id)
        {
            ToDo toDo = GetToDoById(id);
            list.Remove(toDo);
        }
    }
}
