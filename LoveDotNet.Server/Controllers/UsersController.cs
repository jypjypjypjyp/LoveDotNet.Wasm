using LoveDotNet.Models;
using LoveDotNet.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveDotNet.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDBContext _context;

        public UsersController(MyDBContext context)
        {
            _context = context;
        }

        #region Auto_Gen
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
        #endregion

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody]User user)
        {
            var result = await _context.User.Where(a => a.Email == user.Email).FirstOrDefaultAsync();
            return result ?? new User();
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<User>> Signup([FromBody]User user)
        {
            var result = await _context.User.Where(a => a.Email == user.Email).FirstOrDefaultAsync();

            if (result != null)
                return new User();
            user.IsEditor = false;
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            EmailHelper.SendAsyncWithoutAwait(user.Email, 
                "【LoveDotNet】欢迎加入",
                string.Format(
@"<p>{0}：<br>
<p> 注册成功！请存档本邮件以方便找回密码，您的密码为{1}<br>
<p> --LoveDotNet <br>", user.Email, user.Password));

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpGet("Apply/{id}")]
        public async Task<ActionResult<bool>> Apply(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
                return false;

            if(await EmailHelper.SendAsync(
                "1104462345@qq.com",
                "【LoveDotNet】申请成为编辑",
                string.Format(
@"<p>蛤蛤小子：<br>
<p> {0}想成为编辑，代码{1}！<br>
<p> --LoveDotNet <br>", user.Email, user.Id)))
            {
                return true;
            }

            return false;
        }

        [HttpGet("Accept/{id}")]
        public async Task<ActionResult<bool>> Accept(int id)
        {
            var user = await _context.User.FindAsync(id);
            user.IsEditor = true;
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            EmailHelper.SendAsyncWithoutAwait(
                user.Email,
                "【LoveDotNet】您已成为本站编辑",
                string.Format(
@"<p>{0}：<br>
<p> 您已成为本站编辑！<br>
<p> --LoveDotNet <br>", user.Email));
            return true;
        }


    }
}
