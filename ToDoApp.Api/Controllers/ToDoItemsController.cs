using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Route("api/to-do-items")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoItemService _service;

        public ToDoItemsController(IToDoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetToDoItemDto>>> GetAsync()
        {
            var items = await _service.GetAsync();
            var itemsDto = items.Select(x => new GetToDoItemDto
            {
                Id = x.Id,
                Description = x.Description,
                IsDone = x.IsDone,
                CreatedAt = x.CreatedAt,
                DueDate = x.DueDate,
            });
            return Ok(itemsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetToDoItemDto>> GetAsync(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            var itemDto = new GetToDoItemDto
            {
                Id = item.Id,
                Description = item.Description,
                IsDone = item.IsDone,
                CreatedAt = item.CreatedAt,
                DueDate = item.DueDate,
            };

            return Ok(itemDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateToDoItemDto itemDto)
        {
            await _service.CreateAsync(itemDto);
            return Ok();
        }

        [HttpPut("{id}/done")]
        public async Task<ActionResult> UpdateAsync(int id)
        {
            await _service.UpdateStatusAsync(id);
            return Ok();
        }
    }
}
