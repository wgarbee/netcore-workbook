using BaseProject.Data.Models;
using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Areas.Identity.Data
{
    public static class DBInitializer
    {
        public static void Initialize(AuthenticationContext context)
        {
            context.Database.Migrate();
            
            //if (context.Users.Any())
            //{
            //    return;
            //}

            //var defaultPassword = "1";

            //var users = new []
            //{
            //    new AuthUser{ Email = "dan@example.com", UserName = "dan", PasswordHash = defaultPassword }
            //};

            //context.Users.AddRange(users);

            //context.SaveChanges();
        }
    }
}
