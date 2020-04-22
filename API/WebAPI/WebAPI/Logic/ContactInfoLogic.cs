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
                //Check Required
                if (null == objQueryRequest.ContactInfoID)
                {
                    objQueryResponse.Result = "ContactInfoID Required";
                }
                else if (string.IsNullOrWhiteSpace(objQueryRequest.Sign))
                {
                    objQueryResponse.Result = "Sign Required";
                }
                if (!string.IsNullOrWhiteSpace(objQueryResponse.Result))
                {
                    objQueryResponse.Sign = GetSign(objQueryResponse, ApiSignKey);
                    return objQueryResponse;
                }

                //Check Sign
                bool bolCheckSign = Utility.CheckSHA(objQueryRequest.Sign.Trim(), GetSign(objQueryRequest, ApiSignKey));
                if (!bolCheckSign)
                {
                    objQueryResponse.Result = MsgFail;
                    objQueryResponse.Sign = GetSign(objQueryResponse, ApiSignKey);
                    return objQueryResponse;
                }
                #endregion

                #region [Logic]
                ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(Convert.ToInt64(objQueryRequest.ContactInfoID));

                if (null == objContactInfoData)
                {
                    objQueryResponse.Result = MsgFail;
                    objQueryResponse.Sign = GetSign(objQueryResponse, ApiSignKey);
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
                objQueryResponse.Result = MsgException;
                objQueryResponse.Sign = GetSign(objQueryResponse, ApiSignKey);
                return objQueryResponse;
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
                strParams = $"{strKey}&Result={((QueryResponse)objData).Result}&{strKey}";
            }
            #endregion

            //Log.Info("Sign calculate parameter : " + strParams);
            return Utility.GetSHA(strParams);
        }
    }
}