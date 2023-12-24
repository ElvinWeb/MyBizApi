using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBizApi.DataAccessLayer;
using MyBizApi.DTO.EmployeeDtos;
using MyBizApi.DTO.ProfessionDtos;
using MyBizApi.Entities;

namespace MyBizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProfessionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null && id <= 0) return NotFound();

            Profession profession = await _context.Professions
                .Where(profession => profession.IsDeleted == false)
                .FirstOrDefaultAsync(profession => profession.Id == id);

            if (profession == null) return NotFound();

            //ProfessionGetDto professionGetDto = new ProfessionGetDto()
            //{
            //    Id = profession.Id,
            //    Name = profession.Name,
            //};
            ProfessionGetDto professionGetDto = _mapper.Map<ProfessionGetDto>(profession);

            return Ok(professionGetDto);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            List<Profession> professions = await _context.Professions.Where(profession => profession.IsDeleted == false).ToListAsync();

            List<ProfessionGetDto> professionGetDtos = professions.Select(profession => new ProfessionGetDto { Id = profession.Id, Name = profession.Name }).ToList();

            return Ok(professionGetDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProfessionCreateDto professionCreateDto)
        {
            //Profession profession = new Profession()
            //{
            //    Name = professionCreateDto.Name
            //};
            Profession profession = _mapper.Map<Profession>(professionCreateDto);

            profession.CreatedDate = DateTime.UtcNow.AddHours(4);
            profession.UpdatedDate = DateTime.UtcNow.AddHours(4);
            profession.DeletedDate = DateTime.UtcNow.AddHours(4);
            profession.IsDeleted = false;


            await _context.Professions.AddAsync(profession);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPut("")]
        public async Task<IActionResult> Update(ProfessionUpdateDto professionUpdateDto)
        {
            Profession profession = await _context.Professions.FirstOrDefaultAsync(profession => profession.Id == professionUpdateDto.Id);

            if (profession == null) return NotFound();

            profession = _mapper.Map(professionUpdateDto, profession);


            profession.UpdatedDate = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("/professions/softDelete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            Profession profession = await _context.Professions.FirstOrDefaultAsync(profession => profession.Id == id);

            if (profession == null) return NotFound();

            profession.IsDeleted = !profession.IsDeleted;
            profession.DeletedDate = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return StatusCode(204);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            Profession profession = await _context.Professions.FirstOrDefaultAsync(profession => profession.Id == id);

            if (profession == null) return NotFound();

            profession.DeletedDate = DateTime.UtcNow.AddHours(4);

            _context.Professions.Remove(profession);
            await _context.SaveChangesAsync();

            return StatusCode(204);
        }

    }
}
