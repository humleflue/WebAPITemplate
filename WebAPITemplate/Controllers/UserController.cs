using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WASPQueryAPI.Controllers;

namespace ABK_Kasse_API.Controllers
{
    public class UserController : UserControllerBase
    {
        private static List<User> Users { get; set; } = new List<User>
            {
                new User() { Id = 1, FirstName = "Lasse", LastName = "Skaalum" },
                new User() { Id = 2, FirstName = "Foo", LastName = "Bar" }
            };
        private static int _lastUsedId = 2;
        private static int NextId => ++_lastUsedId;
        public override async Task<IActionResult> CreateUser(CreateUser body)
        {
            Users.Add(new User() { Id = NextId, FirstName = body.FirstName, LastName = body.LastName });
            return await Task.Run(() => Ok());
        }

        public override async Task<ActionResult<ICollection<User>>> GetAll()
        {
            return Ok(await Task.Run(() => Users));
        }
    }
}