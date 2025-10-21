using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(AppDbContext context) : BaseApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null) return Unauthorized("Invalid email address");
            using (var hmac = new HMACSHA512(user.PwdSalt))
            {
                byte[] computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Pwd));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != user.PwdHash[i])
                        return Unauthorized("Invalid password");
                }
                return user;
            }
        }

        //註冊
        [HttpPost]
        public async Task<ActionResult<AppUser>> Register(RegisterDto request)
        {
            if (await EmailExists(request.Email))
            {
                return BadRequest("Email taken");
            }
            using (var hmac = new HMACSHA512())
            {
                AppUser appUser = new AppUser()
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    PwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Pwd)),
                    PwdSalt = hmac.Key,
                };
                await context.AddAsync(appUser);
                await context.SaveChangesAsync();
                return appUser;
            }
        }
        private async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
