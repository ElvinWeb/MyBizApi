using FluentValidation;

namespace MyBizApi.DTO.EmployeeDtos
{
    public class EmployeeCreateDto
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string RedirectUrl { get; set; }
        public int ProfessionId { get; set; }
        public IFormFile ImgFile { get; set; }
    }

    public class EmployeeCreateDtoValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateDtoValidator()
        {
            RuleFor(emp => emp.FullName)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(60).WithMessage("Max 60 ola biler!")
               .MinimumLength(5).WithMessage("Min 5 ola biler!");


            RuleFor(emp => emp.Description)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(100).WithMessage("Max 100 ola biler!")
               .MinimumLength(20).WithMessage("Min 5 ola biler!");


            RuleFor(emp => emp.RedirectUrl)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(150).WithMessage("Max 150 ola biler!")
               .MinimumLength(10).WithMessage("Min 10 ola biler!");

            RuleFor(emp => emp.ProfessionId)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .GreaterThan(0);

            RuleFor(emp => emp.ImgFile)
             .NotEmpty().WithMessage("Bos ola bilmez!")
             .NotNull().WithMessage("Null ola bilmez!");
        }
    }
}
