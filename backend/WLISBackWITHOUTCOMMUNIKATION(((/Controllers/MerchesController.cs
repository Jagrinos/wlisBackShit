using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackend.models;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;
using WLISBackWITHOUTCOMMUNIKATION___.Requests;

namespace WLISBackWITHOUTCOMMUNIKATION___.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerchesController(FullContext CX) : ControllerBase
    {
        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<Merch>>> GetMerches()
        {
            return await CX.Merches.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateMerch([FromBody] MerchRequest request)
        {
            var newMerch = new Merch() { Id = Guid.NewGuid(), Title = request.Title, Description = request.Description, Price = request.Price };

            await CX.Merches.AddAsync(newMerch);
            await CX.SaveChangesAsync();

            return Ok(newMerch.Id);
        }

        [HttpPut("{id::guid}")]
        public async Task<ActionResult<Guid>> UpdateMerch(Guid id, [FromBody] MerchRequest request)
        {
            if (CX.Merches.Where(m => m.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Merches.
            Where(m => m.Id == id).
            ExecuteUpdateAsync(m => m
            .SetProperty(m => m.Title, m => request.Title)
            .SetProperty(m => m.Description, m => request.Description)
            .SetProperty(m => m.Price, m => request.Price)
            );
            return Ok(id);



        }

        [HttpDelete("{id::guid}")]
        public async Task<ActionResult<Guid>> DeleteMerch(Guid id)
        {
            if (CX.Merches.Where(m => m.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Merches
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            return Ok();
        }
    }
}
