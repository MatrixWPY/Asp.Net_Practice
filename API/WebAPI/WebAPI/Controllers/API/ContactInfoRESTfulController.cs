using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebAPI.DBServiceReference;
using WebAPI.Models.ContactInfoRESTful;
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
                    DBServiceClient clientDB = new DBServiceClient();
                    ContactInfoModel objContactInfoModel = clientDB.GetContactInfoRESTful(id);
                    if (null == objContactInfoModel)
                    {
                        objQueryResponse = new QueryResponse() { Result = $"{ MsgFail } : No Data" };
                    }
                    else
                    {
                        objQueryResponse = new QueryResponse() { Result = MsgSuccess, Data = objContactInfoModel };
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
                    ContactInfoModel objContactInfoModel = new ContactInfoModel();
                    objContactInfoModel.Name = objAddRequest.Name.Trim();
                    objContactInfoModel.Nickname = (!string.IsNullOrWhiteSpace(objAddRequest.Nickname) ? objAddRequest.Nickname.Trim() : null);
                    objContactInfoModel.Gender = (ContactInfoModel.EnumGender?)objAddRequest.Gender;
                    objContactInfoModel.Age = objAddRequest.Age;
                    objContactInfoModel.PhoneNo = objAddRequest.PhoneNo.Trim();
                    objContactInfoModel.Address = objAddRequest.Address.Trim();

                    DBServiceClient clientDB = new DBServiceClient();
                    objContactInfoModel.ContactInfoID = clientDB.InsertContactInfoRESTful(objContactInfoModel);
                    if (objContactInfoModel.ContactInfoID > 0)
                    {
                        objAddResponse = new AddResponse() { Result = MsgSuccess, Data = objContactInfoModel };
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
                    DBServiceClient clientDB = new DBServiceClient();
                    ContactInfoModel objContactInfoModel = clientDB.GetContactInfoRESTful(id);
                    if (null == objContactInfoModel)
                    {
                        objUpdateResponse = new UpdateResponse() { Result = $"{MsgFail} : No Data" };
                    }
                    else
                    {
                        objContactInfoModel.Name = objUpdateRequest.Name.Trim();
                        objContactInfoModel.Nickname = (!string.IsNullOrWhiteSpace(objUpdateRequest.Nickname) ? objUpdateRequest.Nickname.Trim() : null);
                        objContactInfoModel.Gender = (ContactInfoModel.EnumGender?)objUpdateRequest.Gender;
                        objContactInfoModel.Age = objUpdateRequest.Age;
                        objContactInfoModel.PhoneNo = objUpdateRequest.PhoneNo.Trim();
                        objContactInfoModel.Address = objUpdateRequest.Address.Trim();

                        bool bolResult = clientDB.UpdateContactInfoRESTful(objContactInfoModel);
                        if (bolResult)
                        {
                            objUpdateResponse = new UpdateResponse() { Result = MsgSuccess, Data = objContactInfoModel };
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
                    DBServiceClient clientDB = new DBServiceClient();
                    ContactInfoModel objContactInfoModel = clientDB.GetContactInfoRESTful(id);
                    if (null == objContactInfoModel)
                    {
                        objDeleteResponse = new DeleteResponse() { Result = $"{MsgFail} : No Data" };
                    }
                    else
                    {
                        bool bolResult = clientDB.DeleteContactInfoRESTful(objContactInfoModel);
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
