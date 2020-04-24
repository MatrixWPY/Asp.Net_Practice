using System;
using System.Configuration;
using WebAPI.Models.ContactInfo;
using WebAPI.Models.Data;
using WebAPI.Models.Repository;
using WebAPI.Util;

namespace WebAPI.Logic
{
    public class ContactInfoLogic
    {
        private static readonly string ApiSignKey = ConfigurationManager.AppSettings["ApiSignKey"];
        private static readonly string MsgSuccess = "Success";
        private static readonly string MsgFail = "Fail";
        private static readonly string MsgException = "Exception";

        public QueryResponse Query(QueryRequest objQueryRequest)
        {
            QueryResponse objQueryResponse = new QueryResponse();
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();

            try
            {
                #region [Validation]
                bool bolCheckSign = Utility.CheckSHA(objQueryRequest.Sign.Trim(), GetSign(objQueryRequest, ApiSignKey));
                if (!bolCheckSign)
                {
                    objQueryResponse.Result = $"{MsgFail} : Sign Validate Error";
                    return objQueryResponse;
                }
                #endregion

                #region [Logic]
                ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(Convert.ToInt64(objQueryRequest.ContactInfoID));
                if (null == objContactInfoData)
                {
                    objQueryResponse.Result = $"{MsgFail} : No Data";
                }
                else
                {
                    objQueryResponse.Result = MsgSuccess;
                    objQueryResponse.Data = objContactInfoData;
                    objQueryResponse.Sign = GetSign(objQueryResponse, ApiSignKey);
                }

                return objQueryResponse;
                #endregion
            }
            catch (Exception ex)
            {
                #region [Exception]
                objQueryResponse.Result = $"{MsgException} : {ex.Message}";
                return objQueryResponse;
                #endregion
            }
        }

        public AddResponse Add(AddRequest objAddRequest)
        {
            AddResponse objAddResponse = new AddResponse();
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();

            try
            {
                #region [Validation]
                bool bolCheckSign = Utility.CheckSHA(objAddRequest.Sign.Trim(), GetSign(objAddRequest, ApiSignKey));
                if (!bolCheckSign)
                {
                    objAddResponse.Result = $"{MsgFail} : Sign Validate Error";
                    return objAddResponse;
                }
                #endregion

                #region [Logic]
                ContactInfoData objContactInfoData = new ContactInfoData();
                objContactInfoData.Name = objAddRequest.Name.Trim();
                objContactInfoData.Nickname = (!string.IsNullOrWhiteSpace(objAddRequest.Nickname) ? objAddRequest.Nickname.Trim() : null);
                objContactInfoData.Gender = (ContactInfoData.EnumGender?)objAddRequest.Gender;
                objContactInfoData.Age = objAddRequest.Age;
                objContactInfoData.PhoneNo = objAddRequest.PhoneNo.Trim();
                objContactInfoData.Address = objAddRequest.Address.Trim();

                bool bolAddResult = objContactInfoRepository.AddContactInfo(objContactInfoData);
                if (bolAddResult)
                {
                    objAddResponse.Result = MsgSuccess;
                    objAddResponse.Data = objContactInfoData;
                    objAddResponse.Sign = GetSign(objAddResponse, ApiSignKey);
                }
                else
                {
                    objAddResponse.Result = $"{MsgFail} : Add Data Error";
                }

                return objAddResponse;
                #endregion
            }
            catch (Exception ex)
            {
                #region [Exception]
                objAddResponse.Result = $"{MsgException} : {ex.Message}";
                return objAddResponse;
                #endregion
            }
        }

        private static string GetSign(object objData, string strKey)
        {
            string strParams = string.Empty;

            #region [QueryRequest]
            if (objData is QueryRequest)
            {
                strParams = $"{strKey}&ContactInfoID={((QueryRequest)objData).ContactInfoID}&{strKey}";
            }
            #endregion

            #region [QueryResponse]
            if (objData is QueryResponse)
            {
                strParams = $"{strKey}&Result={((QueryResponse)objData).Result}&ContactInfoID={((QueryResponse)objData).Data.ContactInfoID}&{strKey}";
            }
            #endregion

            #region [AddRequest]
            if (objData is AddRequest)
            {
                strParams = $"{strKey}&Name={((AddRequest)objData).Name}&PhoneNo={((AddRequest)objData).PhoneNo}&{strKey}";
            }
            #endregion

            #region [AddResponse]
            if (objData is AddResponse)
            {
                strParams = $"{strKey}&Result={((AddResponse)objData).Result}&ContactInfoID={((AddResponse)objData).Data.ContactInfoID}&{strKey}";
            }
            #endregion

            //Log.Info("Sign calculate parameter : " + strParams);
            return Utility.GetSHA(strParams);
        }
    }
}