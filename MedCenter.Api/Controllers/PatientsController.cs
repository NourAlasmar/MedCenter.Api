using MedCenter.Api.DTOs;
using MedCenter.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _svc;
        public PatientsController(IPatientService svc) => _svc = svc;

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id, CancellationToken ct) =>
            (await _svc.GetAsync(id, ct)) is { } p ? Ok(p) : NotFound();

        [HttpGet("center/{centerId:long}")]
        public async Task<IActionResult> List(long centerId, [FromQuery] string? q, CancellationToken ct) =>
            Ok(await _svc.ListByCenterAsync(centerId, q, ct));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientCreateDto dto, CancellationToken ct)
        {
            var p = await _svc.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] PatientUpdateDto dto, CancellationToken ct) =>
            await _svc.UpdateAsync(id, dto, ct) ? NoContent() : NotFound();
    }
}