using WebAPI.DBServiceReference;

namespace WebAPI.Models.ContactInfoRESTful
{
    public class AddResponse
    {
        public string Result { get; set; }

        public ContactInfoModel Data { get; set; }
    }
}