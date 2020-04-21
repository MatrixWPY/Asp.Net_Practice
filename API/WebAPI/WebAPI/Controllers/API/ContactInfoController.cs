using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebAPI.Models.ContactInfo;
using WebMVC.Models.Data;
using WebMVC.Models.Repository;

namespace WebAPI.Controllers
{
    public class ContactInfoController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Query([FromBody] QueryRequest objQueryRequest)
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(objQueryRequest.ContactInfoID);

            QueryResponse objQueryResponse = new QueryResponse();
            if (null == objContactInfoData)
            {
                objQueryResponse.Result = "Fail";
            }
            else
            {
                objQueryResponse.Result = "Success";
                objQueryResponse.Data = objContactInfoData;
            }

            //物件回傳
            //return Ok(objQueryResponse);

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(objQueryResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }
    }
}
