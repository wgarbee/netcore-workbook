﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class ToDo
    {
        public int ID { get; set; }

        public string Description { get; set; }

        [UIHint("Status")]
        public Status Status { get; set; }
    }
}