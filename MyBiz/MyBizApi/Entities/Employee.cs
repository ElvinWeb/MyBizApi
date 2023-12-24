using System.ComponentModel.DataAnnotations.Schema;

namespace MyBizApi.Entities
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string RedirectUrl { get; set; }
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
        public string? ImgUrl { get; set; }
    }
}
