namespace MyBizApi.CustomExceptions.Common
{
    public class InvalidContentTypeOrImageSize : Exception
    {
        public string PropertyName { get; set; }
        public InvalidContentTypeOrImageSize() { }

        public InvalidContentTypeOrImageSize(string? message) : base(message)
        {
         
        }
        public InvalidContentTypeOrImageSize(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
