using MedCenter.Api.DTOs;
using MedCenter.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CentersController : ControllerBase
    {
        private readonly ICenterService _svc;
        public CentersController(ICenterService svc) => _svc = svc;

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id, CancellationToken ct) =>
            (await _svc.GetAsync(id, ct)) is { } c ? Ok(c) : NotFound();

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string? q, CancellationToken ct) =>
            Ok(await _svc.SearchAsync(q, ct));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CenterCreateDto dto, CancellationToken ct)
        {
            var c = await _svc.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(Get), new { id = c.Id }, c);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] CenterUpdateDto dto, CancellationToken ct) =>
            await _svc.UpdateAsync(id, dto, ct) ? NoContent() : NotFound();

        [HttpPost("{id:long}/extend")]
        public async Task<IActionResult> Extend(long id, [FromQuery] int days = 30, CancellationToken ct = default) =>
            await _svc.ExtendSubscriptionAsync(id, TimeSpan.FromDays(days), ct) ? Ok() : NotFound();
    }
}