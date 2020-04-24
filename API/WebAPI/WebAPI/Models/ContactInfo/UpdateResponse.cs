using WebAPI.Models.Data;

namespace WebAPI.Models.ContactInfo
{
    public class UpdateResponse
    {
        public string Result { get; set; }

        public ContactInfoData Data { get; set; }

        public string Sign { get; set; }
    }
}