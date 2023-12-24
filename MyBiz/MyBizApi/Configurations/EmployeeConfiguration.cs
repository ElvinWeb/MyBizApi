using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBizApi.Entities;

namespace MyBizApi.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(emp => emp.FullName).IsRequired().HasMaxLength(60);
            builder.Property(emp => emp.Description).IsRequired().HasMaxLength(100);
            builder.Property(emp => emp.RedirectUrl).IsRequired().HasMaxLength(150);
            builder.HasOne(emp => emp.Profession).WithMany(emp => emp.Employees).HasForeignKey(emp => emp.ProfessionId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(emp => emp.ImgUrl).HasMaxLength(100);
        }
    }
}
