using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return members;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.Users.FindAsync(id);

            if (member == null) return NotFound();

            return member;
        }
        [HttpPost]
        public async Task<ActionResult<AppUser>> TESTINSERT()
        {
            Random r = new Random();
            string emailtemp = r.Next(10000, 100000).ToString();

            AppUser a = new AppUser()
            {
                DisplayName = Guid.NewGuid().ToString().Substring(0, 4),
                Email = emailtemp + "@qq.com"
            };
            await context.Users.AddAsync(a);
            await context.SaveChangesAsync();
            return a;
        }
    }
}
