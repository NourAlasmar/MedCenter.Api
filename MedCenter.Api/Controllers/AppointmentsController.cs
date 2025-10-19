using MedCenter.Api.DTOs;
using MedCenter.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _svc;
        public AppointmentsController(IAppointmentService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateDto dto, CancellationToken ct) =>
            Ok(await _svc.CreateAsync(dto, ct));

        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> ForDoctor(Guid doctorId, [FromQuery] DateTime from, [FromQuery] DateTime to, CancellationToken ct) =>
            Ok(await _svc.ForDoctorAsync(doctorId, from, to, ct));

        [HttpPost("{id:long}/cancel")]
        public async Task<IActionResult> Cancel(long id, [FromQuery] string? reason, CancellationToken ct) =>
            await _svc.CancelAsync(id, reason, ct) ? Ok() : NotFound();

        [HttpPost("{id:long}/complete")]
        public async Task<IActionResult> Complete(long id, CancellationToken ct) =>
            await _svc.CompleteAsync(id, ct) ? Ok() : NotFound();
    }
}