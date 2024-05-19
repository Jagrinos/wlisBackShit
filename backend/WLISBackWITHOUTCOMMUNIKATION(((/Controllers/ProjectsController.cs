using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WLISBackend.models;
using WLISBackWITHOUTCOMMUNIKATION___.Contexts;
using WLISBackWITHOUTCOMMUNIKATION___.Requests;

namespace WLISBackWITHOUTCOMMUNIKATION___.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController(FullContext CX) : ControllerBase
    {
        private readonly FullContext CX = CX;

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            return await CX.Projects.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProject([FromBody] ProjectRequest request)
        {
            var newProject = new Project() { Id = Guid.NewGuid(), Title = request.Title, Description = request.Description };

            await CX.Projects.AddAsync(newProject);
            await CX.SaveChangesAsync();

            return Ok(newProject.Id);
        }

        [HttpPut("{id::guid}")]
        public async Task<ActionResult<Guid>> UpdateProject(Guid id, [FromBody] ProjectRequest request)
        {
            if (CX.Projects.Where(m => m.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Projects.
            Where(m => m.Id == id).
            ExecuteUpdateAsync(m => m
            .SetProperty(m => m.Title, m => request.Title)
            .SetProperty(m => m.Description, m => request.Description));
            return Ok(id);



        }

        [HttpDelete("{id::guid}")]
        public async Task<ActionResult<Guid>> DeleteProject(Guid id)
        {
            if (CX.Projects.Where(m => m.Id == id).ToList().Count == 0)
                return BadRequest();

            await CX.Projects
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            return Ok();
        }
    }
}
