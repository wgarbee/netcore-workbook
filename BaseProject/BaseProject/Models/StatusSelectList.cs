using BaseProject.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BaseProject.Models
{
    public class StatusSelectList : SelectList
    {
        public StatusSelectList(Status selected) : base(Enum.GetValues(typeof(Status)), selected)
        {

        }
        public StatusSelectList() : base(Enum.GetValues(typeof(Status)))
        {

        }
    }
}
