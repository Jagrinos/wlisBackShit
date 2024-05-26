using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackWITHOUTCOMMUNIKATION___.Auntefication;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;
using WLISBackWITHOUTCOMMUNIKATION___.Models;
using WLISBackWITHOUTCOMMUNIKATION___.Requests;

namespace WLISBackWITHOUTCOMMUNIKATION___.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpController(FullContext CX) : ControllerBase
    {

        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<Ip>>> GetIps()
        {
            return await CX.Ips.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<List<Guid>>> PostIp([FromBody] IpRequest request )
        {
            var newip = new Ip() { Id = Guid.NewGuid(), IpString = request.Ip };    

            await CX.Ips.AddAsync(newip);
            await CX.SaveChangesAsync();
            return Ok(newip);
        }

        [HttpPut("findip")]
        public ActionResult<bool> FindUser([FromBody] IpRequest request)
        {
            if (CX.Ips.FirstOrDefault(ip => ip.IpString == request.Ip) != null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteIp([FromBody] IpRequest request)
        {
            var FindIp = CX.Ips.FirstOrDefault(i => i.IpString == request.Ip);
            if (FindIp != null)
            {
                CX.Ips.Remove(FindIp);
                await CX.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
