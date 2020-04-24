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
                    strErrorMsg += string.Join(",", item.Errors.Select(e => e.ErrorMessage));
                }
                objQueryResponse = new QueryResponse() { Result = strErrorMsg };
            }

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objQueryResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] AddRequest objAddRequest)
        {
            AddResponse objAddResponse;

            if (ModelState.IsValid)
            {
                ContactInfoLogic objContactInfoLogic = new ContactInfoLogic();
                objAddResponse = objContactInfoLogic.Add(objAddRequest);

                //return Ok(objAddResponse);
            }
            else
            {
                //return BadRequest(ModelState);

                string strErrorMsg = string.Empty;
                foreach (var item in ModelState.Values)
                {
                    strErrorMsg += string.Join(",", item.Errors.Select(e => e.ErrorMessage));
                }
                objAddResponse = new AddResponse() { Result = strErrorMsg };
            }

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objAddResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }

        [HttpPost]
        public IHttpActionResult Update([FromBody] UpdateRequest objUpdateRequest)
        {
            UpdateResponse objUpdateResponse;

            if (ModelState.IsValid)
            {
                ContactInfoLogic objContactInfoLogic = new ContactInfoLogic();
                objUpdateResponse = objContactInfoLogic.Update(objUpdateRequest);

                //return Ok(objUpdateResponse);
            }
            else
            {
                //return BadRequest(ModelState);

                string strErrorMsg = string.Empty;
                foreach (var item in ModelState.Values)
                {
                    strErrorMsg += string.Join(",", item.Errors.Select(e => e.ErrorMessage));
                }
                objUpdateResponse = new UpdateResponse() { Result = strErrorMsg };
            }

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objUpdateResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }
    }
}