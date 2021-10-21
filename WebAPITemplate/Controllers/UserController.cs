using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPITemplate.Controllers
{
    public class UserController : UserControllerBase
    {
        private static List<User> Users { get; } = new List<User>
            {
                new User() { Id = "1", FirstName = "Lasse", LastName = "Skaalum" },
                new User() { Id = "2", FirstName = "Foo", LastName = "Bar" }
            };
        private static int _lastUsedId = 2;
        private static string NextId => $"{++_lastUsedId}";

        public override async Task<IActionResult> Create(User body)
        {
            Users.Add(new User() { Id = NextId, FirstName = body.FirstName, LastName = body.LastName });
            return await SimulateAsync(Ok());
        }

        public override async Task<ActionResult<ICollection<User>>> GetAll()
        {
            return Ok(await SimulateAsync(Users));
        }

        public override async Task<ActionResult<User>> Get(string id)
        {
            User user = await FindUserAsync(id);
            if (user == null)
                return BadRequest($"User with id '{id}' not found.");
            return Ok(user);
        }

        public override async Task<IActionResult> Delete(string id)
        {
            User user = await FindUserAsync(id);
            if (user == null)
                return BadRequest($"User with id '{id}' not found.");
            Users.Remove(user);
            return Ok();
        }

        public override async Task<IActionResult> Update(User body, string id)
        {
            User user = await FindUserAsync(id);
            if (user == null)
                return BadRequest($"User with id '{id}' not found.");
            user.FirstName = body.FirstName;
            user.LastName = body.LastName;
            return Ok();
        }

        private async Task<User> FindUserAsync(string id)
        {
            return await SimulateAsync(Users.Find(u => u.Id == id));
        }

        private async Task<T> SimulateAsync<T>(T obj)
        {
            return await Task.Run(() => obj);
        }
    }
}