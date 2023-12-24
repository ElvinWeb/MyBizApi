using FluentValidation;

namespace MyBizApi.DTO.ProfessionDtos
{
    public class ProfessionUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProfessionUpdateDtoValidator : AbstractValidator<ProfessionUpdateDto>
    {
        public ProfessionUpdateDtoValidator()
        {
            RuleFor(profession => profession.Name)
                .NotEmpty().WithMessage("bos ola  bilmez!")
                .NotNull().WithMessage("null ola bilmez!")
                .MinimumLength(5).WithMessage("min 5 ola biler!")
                .MaximumLength(50).WithMessage("max 50 ola biler!");

            RuleFor(profession => profession.Id)
             .NotEmpty().WithMessage("Bos ola bilmez!")
             .NotNull().WithMessage("Null ola bilmez!")
             .GreaterThan(0);
        }
    }
}
