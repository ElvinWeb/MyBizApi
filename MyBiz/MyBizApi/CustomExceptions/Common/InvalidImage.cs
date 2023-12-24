namespace MyBizApi.CustomExceptions.Common
{
    public class InvalidImage : Exception
    {
        public string PropertyName { get; set; }
        public InvalidImage() { }
        public InvalidImage(string? message) : base(message)
        {

        }
        public InvalidImage(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
