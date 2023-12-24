namespace MyBizApi.Entities
{
    public class Profession :  BaseEntity
    {
        public string Name { get; set; }    
        public List<Employee>? Employees { get; set; }
    }
}
