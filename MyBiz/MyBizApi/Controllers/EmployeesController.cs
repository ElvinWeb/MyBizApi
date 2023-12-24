using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBizApi.CustomExceptions.Common;
using MyBizApi.DataAccessLayer;
using MyBizApi.DTO.EmployeeDtos;
using MyBizApi.Entities;
using MyBizApi.Helpers;
using System.Drawing;

namespace MyBizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public EmployeesController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            List<Employee> employees = await _context.Employees.Include(emp => emp.Profession).Where(emp => emp.IsDeleted == false).ToListAsync();

            List<EmployeeGetDto> employeeGetDtos = employees.Select(emp => new EmployeeGetDto { FullName = emp.FullName, Description = emp.Description, Profession = emp.Profession.Name, RedirectUrl = emp.RedirectUrl, ImgUrl = emp.ImgUrl }).ToList();

            return Ok(employeeGetDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null && id <= 0) return NotFound();

            Employee employee = await _context.Employees
                .Include(emp => emp.Profession)
                .Where(emp => emp.IsDeleted == false)
                .FirstOrDefaultAsync(emp => emp.Id == id);

            if (employee is null) return NotFound();
            #region oldCodes

            //EmployeeGetDto employeeGetDto = new EmployeeGetDto()
            //{
            //    FullName = employee.FullName,
            //    Description = employee.Description,
            //    RedirectUrl = employee.RedirectUrl,
            //    Profession = employee.Profession.Name,
            //};
            #endregion

            EmployeeGetDto employeeGetDto = _mapper.Map<EmployeeGetDto>(employee);
            return Ok(employeeGetDto);

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmployeeCreateDto employeeCreateDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeCreateDto);

            #region oldCodes
            //string ImgUrl = await Helper.UploadFile(imgFile);
            //Employee employee = new Employee()
            //{
            //    FullName = employeeCreateDto.FullName,
            //    Description = employeeCreateDto.Description,
            //    RedirectUrl = employeeCreateDto.RedirectUrl,
            //    ProfessionId = employeeCreateDto.ProfessionId,
            //};
            #endregion

            if (employeeCreateDto.ImgFile != null)
            {
                if (employeeCreateDto.ImgFile.ContentType != "image/png" && employeeCreateDto.ImgFile.ContentType != "image/jpeg")
                {
                    throw new InvalidContentTypeOrImageSize("please select correct file type");
                }

                if (employeeCreateDto.ImgFile.Length > 1048576)
                {
                    throw new InvalidContentTypeOrImageSize("file size should be more lower than 1mb");
                }
            }
            else
            {
                throw new InvalidImage("image file is must be choosed!! ");
            }

            string folder = "Uploads/employeesImages";
            string newImgUrl = await Helper.GetFileName(folder, employeeCreateDto.ImgFile);


            employee.CreatedDate = DateTime.UtcNow.AddHours(4);
            employee.UpdatedDate = DateTime.UtcNow.AddHours(4);
            employee.DeletedDate = DateTime.UtcNow.AddHours(4);
            employee.ImgUrl = newImgUrl;
            employee.IsDeleted = false;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { message = "Object yaradildi" });
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromForm] EmployeeUpdateDto employeeUpdateDto)
        {
            if (employeeUpdateDto.Id == null && employeeUpdateDto.Id <= 0) return NotFound();

            Employee employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == employeeUpdateDto.Id);
            if (employee is null) return NotFound();

            #region oldCodes

            //employee.FullName = employeeUpdateDto.FullName;
            //employee.Description = employeeUpdateDto.Description;
            //employee.ProfessionId = employeeUpdateDto.ProfessionId;
            //employee.RedirectUrl = employeeUpdateDto.RedirectUrl;
            #endregion

            if (employeeUpdateDto.ImgFile != null)
            {

                if (employeeUpdateDto.ImgFile.ContentType != "image/png" && employeeUpdateDto.ImgFile.ContentType != "image/jpeg")
                {
                    throw new InvalidContentTypeOrImageSize("Image", "please select correct file type");
                }

                if (employeeUpdateDto.ImgFile.Length > 1048576)
                {
                    throw new InvalidContentTypeOrImageSize("Image", "file size should be more lower than 1mb");
                }

                string folder = "Uploads/employeesImages";
                string newFilePath = await Helper.GetFileName(folder, employeeUpdateDto.ImgFile);

                string oldFilePath = Path.Combine(folder, employee.ImgUrl);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                employee.ImgUrl = newFilePath;
            }

            employee = _mapper.Map(employeeUpdateDto, employee);
            employee.UpdatedDate = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return Ok(employeeUpdateDto);
        }

        [HttpDelete("/employees/softDelete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            Employee employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == id);

            if (employee is null) return NotFound();

            employee.IsDeleted = !employee.IsDeleted;
            employee.DeletedDate = DateTime.UtcNow.AddHours(4);


            await _context.SaveChangesAsync();

            return StatusCode(204);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            Employee employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == id);

            if (employee is null) return NotFound();

            string fullPath = Path.Combine("Uploads/employeesImages", employee.ImgUrl);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            employee.DeletedDate = DateTime.UtcNow.AddHours(4);

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
