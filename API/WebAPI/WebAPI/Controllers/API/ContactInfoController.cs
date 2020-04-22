using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebAPI.Logic;
using WebAPI.Models.ContactInfo;
using WebAPI.Util;

namespace WebAPI.Controllers.API
{
    public class ContactInfoController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Query([FromBody] QueryRequest objQueryRequest)
        {
            ContactInfoLogic objContactInfoLogic = new ContactInfoLogic();
            QueryResponse objQueryResponse = objContactInfoLogic.Query(objQueryRequest);

            //物件回傳
            //return Ok(objQueryResponse);

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objQueryResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }
    }
}
