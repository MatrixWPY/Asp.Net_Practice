using System.Linq;
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
            QueryResponse objQueryResponse;

            if (ModelState.IsValid)
            {
                ContactInfoLogic objContactInfoLogic = new ContactInfoLogic();
                objQueryResponse = objContactInfoLogic.Query(objQueryRequest);

                //return Ok(objQueryResponse);
            }
            else
            {
                //return BadRequest(ModelState);

                string strErrorMsg = string.Empty;
                foreach (var item in ModelState.Values)
                {
                    strErrorMsg += (item.Errors.Count > 0 ? string.Join(",", item.Errors.Select(e => e.ErrorMessage)) : string.Empty);
                }
                objQueryResponse = new QueryResponse() { Result = strErrorMsg };
            }

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objQueryResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }
    }
}