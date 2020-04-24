using WebAPI.Models.Data;

namespace WebAPI.Models.ContactInfo
{
    public class AddResponse
    {
        public string Result { get; set; }

        public ContactInfoData Data { get; set; }

        public string Sign { get; set; }
    }
}