using BaseProject.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Models
{
    public class UserSelectList : SelectList
    {
        public UserSelectList(IEnumerable<User> items, int selected) : base(items, nameof(User.Id), nameof(User.FullName), selected)
        {
        }
        public UserSelectList(IEnumerable<User> items) : base(items, nameof(User.Id), nameof(User.FullName))
        {
        }
    }
}
