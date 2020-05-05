using WebAPI.DBServiceReference;

namespace WebAPI.Models.ContactInfo
{
    public class UpdateResponse
    {
        public string Result { get; set; }

        public ContactInfoModel Data { get; set; }

        public string Sign { get; set; }
    }
}