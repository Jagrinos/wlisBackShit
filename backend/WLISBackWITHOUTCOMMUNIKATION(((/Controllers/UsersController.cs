using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;
using WLISBackWITHOUTCOMMUNIKATION___.Requests;
using WLISBackWITHOUTCOMMUNIKATION___.Auntefication;

namespace WLISBackWITHOUTCOMMUNIKATION___.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(FullContext CX) : ControllerBase
    {

        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await CX.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UserRequest request)
        {
            var newUser = new User() { Id = Guid.NewGuid(), Login = request.Login, Password = request.Password};

            await CX.Users.AddAsync(newUser);
            await CX.SaveChangesAsync();

            return Ok(newUser.Id);
        }

        [HttpPut("finduser")]
        public ActionResult<bool> FindUser([FromBody] UserRequest request)
        {
            if (CX.Users.FirstOrDefault(u => u.Login == request.Login && u.Password == request.Password) != null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Deleteuser(Guid id)
        {
            if (CX.Users.Where(a => a.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Users
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            return Ok();
        }
    }
}


//private readonly FullContext CX = CX;

//[HttpGet("login")]
//public async Task<ActionResult<List<User>>> GetUsers()
//{
//    return await CX.Users.ToListAsync();
//}

//[HttpPost]
//public async Task<ActionResult<Guid>> CreateUser([FromBody] UserRequest request)
//{
//    var newUser = new User() { Id = Guid.NewGuid(), Login = request.Login, Password = request.Password, Role = request.Role  };

//    await CX.Users.AddAsync(newUser);
//    await CX.SaveChangesAsync();

//    return Ok(newUser.Id);
//}

//[HttpPut("{id::guid}")]
//public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UserRequest request)
//{
//    if (CX.Users.Where(m => m.Id == id).ToList().Count == 0)
//        return BadRequest();

//    await CX.Users.
//    Where(m => m.Id == id).
//    ExecuteUpdateAsync(m => m
//    .SetProperty(m => m.Login, m => request.Login)
//    .SetProperty(m => m.Password, m => request.Password)
//    .SetProperty(m => m.Role, m => request.Role)
//    );
//    return Ok(id);



//}

//[HttpDelete("{id::guid}")]
//public async Task<ActionResult<Guid>> DeleteUser(Guid id)
//{
//    if (CX.Users.Where(m => m.Id == id).ToList().Count == 0)
//        return BadRequest();

//    await CX.Users
//        .Where(m => m.Id == id)
//        .ExecuteDeleteAsync();
//    return Ok();
//}

//autor
//autor