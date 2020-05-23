using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebAPI.Models.ContactInfoRESTful;
using WebAPI.Models.Data;
using WebAPI.Models.Repository;
using WebAPI.Util;

namespace WebAPI.Controllers.API
{
    public class ContactInfoRESTfulController : ApiController
    {
        private static readonly string MsgSuccess = "Success";
        private static readonly string MsgFail = "Fail";
        private static readonly string MsgException = "Exception";

        public IHttpActionResult Get(long id)
        {
            QueryResponse objQueryResponse;

            if (ModelState.IsValid)
            {
                try
                {
                    ContactInfoRESTfulRepository objContactInfoRESTfulRepository = new ContactInfoRESTfulRepository();
                    ContactInfoData objContactInfoData = objContactInfoRESTfulRepository.Query(id);
                    if (null == objContactInfoData)
                    {
                        objQueryResponse = new QueryResponse() { Result = $"{ MsgFail } : No Data" };
                    }
                    else
                    {
                        objQueryResponse = new QueryResponse() { Result = MsgSuccess, Data = objContactInfoData };
                    }
                }
                catch (Exception ex)
                {
                    objQueryResponse = new QueryResponse() { Result = $"{MsgException} : {ex.Message}" };
                }

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
                objQueryResponse = new QueryResponse() { Result = string.IsNullOrWhiteSpace(strErrorMsg) ? "Request Format Error" : strErrorMsg };
            }

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objQueryResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }

        public IHttpActionResult Post([FromBody] AddRequest objAddRequest)
        {
            AddResponse objAddResponse;

            if (null != objAddRequest && ModelState.IsValid)
            {
                try
                {
                    ContactInfoData objContactInfoData = new ContactInfoData();
                    objContactInfoData.Name = objAddRequest.Name.Trim();
                    objContactInfoData.Nickname = (!string.IsNullOrWhiteSpace(objAddRequest.Nickname) ? objAddRequest.Nickname.Trim() : null);
                    objContactInfoData.Gender = (ContactInfoData.EnumGender?)objAddRequest.Gender;
                    objContactInfoData.Age = objAddRequest.Age;
                    objContactInfoData.PhoneNo = objAddRequest.PhoneNo.Trim();
                    objContactInfoData.Address = objAddRequest.Address.Trim();

                    ContactInfoRESTfulRepository objContactInfoRESTfulRepository = new ContactInfoRESTfulRepository();
                    bool bolResult = objContactInfoRESTfulRepository.Insert(objContactInfoData);
                    if (bolResult)
                    {
                        objAddResponse = new AddResponse() { Result = MsgSuccess, Data = objContactInfoData };
                    }
                    else
                    {
                        objAddResponse = new AddResponse() { Result = $"{MsgFail} : Insert Data Error" };
                    }
                }
                catch (Exception ex)
                {
                    objAddResponse = new AddResponse() { Result = $"{MsgException} : {ex.Message}" };
                }

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
                objAddResponse = new AddResponse() { Result = string.IsNullOrWhiteSpace(strErrorMsg) ? "Request Format Error" : strErrorMsg };
            }

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objAddResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }

        public IHttpActionResult Put(long id, [FromBody] UpdateRequest objUpdateRequest)
        {
            UpdateResponse objUpdateResponse;

            if (null != objUpdateRequest && ModelState.IsValid)
            {
                try
                {
                    ContactInfoRESTfulRepository objContactInfoRESTfulRepository = new ContactInfoRESTfulRepository();
                    ContactInfoData objContactInfoData = objContactInfoRESTfulRepository.Query(id);
                    if (null == objContactInfoData)
                    {
                        objUpdateResponse = new UpdateResponse() { Result = $"{MsgFail} : No Data" };
                    }
                    else
                    {
                        objContactInfoData.Name = objUpdateRequest.Name.Trim();
                        objContactInfoData.Nickname = (!string.IsNullOrWhiteSpace(objUpdateRequest.Nickname) ? objUpdateRequest.Nickname.Trim() : null);
                        objContactInfoData.Gender = (ContactInfoData.EnumGender?)objUpdateRequest.Gender;
                        objContactInfoData.Age = objUpdateRequest.Age;
                        objContactInfoData.PhoneNo = objUpdateRequest.PhoneNo.Trim();
                        objContactInfoData.Address = objUpdateRequest.Address.Trim();

                        bool bolResult = objContactInfoRESTfulRepository.Update(objContactInfoData);
                        if (bolResult)
                        {
                            objUpdateResponse = new UpdateResponse() { Result = MsgSuccess, Data = objContactInfoData };
                        }
                        else
                        {
                            objUpdateResponse = new UpdateResponse() { Result = $"{MsgFail} : Update Data Error" };
                        }
                    }
                }
                catch (Exception ex)
                {
                    objUpdateResponse = new UpdateResponse() { Result = $"{MsgException} : {ex.Message}" };
                }

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
                objUpdateResponse = new UpdateResponse() { Result = string.IsNullOrWhiteSpace(strErrorMsg) ? "Request Format Error" : strErrorMsg };
            }

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objUpdateResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }

        public IHttpActionResult Delete(long id)
        {
            DeleteResponse objDeleteResponse;

            if (ModelState.IsValid)
            {
                try
                {
                    ContactInfoRESTfulRepository objContactInfoRESTfulRepository = new ContactInfoRESTfulRepository();
                    ContactInfoData objContactInfoData = objContactInfoRESTfulRepository.Query(id);
                    if (null == objContactInfoData)
                    {
                        objDeleteResponse = new DeleteResponse() { Result = $"{MsgFail} : No Data" };
                    }
                    else
                    {
                        bool bolResult = objContactInfoRESTfulRepository.Delete(objContactInfoData);
                        if (bolResult)
                        {
                            objDeleteResponse = new DeleteResponse() { Result = MsgSuccess };
                        }
                        else
                        {
                            objDeleteResponse = new DeleteResponse() { Result = $"{MsgFail} : Delete Data Error" };
                        }
                    }
                }
                catch (Exception ex)
                {
                    objDeleteResponse = new DeleteResponse() { Result = $"{MsgException} : {ex.Message}" };
                }

                //return Ok(objDeleteResponse);
            }
            else
            {
                //return BadRequest(ModelState);

                string strErrorMsg = string.Empty;
                foreach (var item in ModelState.Values)
                {
                    strErrorMsg += string.Join(",", item.Errors.Select(e => e.ErrorMessage));
                }
                objDeleteResponse = new DeleteResponse() { Result = string.IsNullOrWhiteSpace(strErrorMsg) ? "Request Format Error" : strErrorMsg };
            }

            //轉換JSON格式回傳
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(Utility.GetJSON(objDeleteResponse), Encoding.GetEncoding("UTF-8"), "application/json") };
            return ResponseMessage(result);
        }
    }
}
