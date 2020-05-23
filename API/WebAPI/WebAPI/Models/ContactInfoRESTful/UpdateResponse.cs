using WebAPI.Models.Data;

namespace WebAPI.Models.ContactInfoRESTful
{
    public class UpdateResponse
    {
        public string Result { get; set; }

        public ContactInfoData Data { get; set; }
    }
}