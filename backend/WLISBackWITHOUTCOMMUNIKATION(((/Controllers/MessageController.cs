using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackend.models;
using WLISBackend.requests;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;

namespace WLISBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController(FullContext CX) : ControllerBase
    {
        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetMessages()
        {
            return await CX.Messages.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateMessage([FromBody] MessageRequest request)
        {
            var newMessage = new Message() { Id = Guid.NewGuid(), Name = request.Name, MessageText = request.MessageText };

            await CX.Messages.AddAsync(newMessage);
            await CX.SaveChangesAsync();

            return Ok(newMessage.Id);
        }

        [HttpPut("{id::guid}")]
        public async Task<ActionResult<Guid>> UpdateMessage(Guid id, [FromBody] MessageRequest request)
        {
            if (CX.Messages.Where(m => m.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Messages.
            Where(m => m.Id == id).
            ExecuteUpdateAsync(m => m
            .SetProperty(m => m.Name, m => request.Name)
            .SetProperty(m => m.MessageText, m => request.MessageText));
            return Ok(id);



        }

        [HttpDelete("{id::guid}")]
        public async Task<ActionResult<Guid>> DeleteMessage(Guid id)
        {
            if (CX.Messages.Where(m => m.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Messages
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            return Ok();
        }
    }
}
