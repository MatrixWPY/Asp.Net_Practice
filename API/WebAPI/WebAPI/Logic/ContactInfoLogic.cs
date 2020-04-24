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

        public UpdateResponse Update(UpdateRequest objUpdateRequest)
        {
            UpdateResponse objUpdateResponse = new UpdateResponse();
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();

            try
            {
                #region [Validation]
                bool bolCheckSign = Utility.CheckSHA(objUpdateRequest.Sign.Trim(), GetSign(objUpdateRequest, ApiSignKey));
                if (!bolCheckSign)
                {
                    objUpdateResponse.Result = $"{MsgFail} : Sign Validate Error";
                    return objUpdateResponse;
                }
                #endregion

                #region [Logic]
                ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(Convert.ToInt64(objUpdateRequest.ContactInfoID));
                if (null == objContactInfoData)
                {
                    objUpdateResponse.Result = $"{MsgFail} : No Data";
                }
                else
                {
                    objContactInfoData.Name = objUpdateRequest.Name.Trim();
                    objContactInfoData.Nickname = (!string.IsNullOrWhiteSpace(objUpdateRequest.Nickname) ? objUpdateRequest.Nickname.Trim() : null);
                    objContactInfoData.Gender = (ContactInfoData.EnumGender?)objUpdateRequest.Gender;
                    objContactInfoData.Age = objUpdateRequest.Age;
                    objContactInfoData.PhoneNo = objUpdateRequest.PhoneNo.Trim();
                    objContactInfoData.Address = objUpdateRequest.Address.Trim();

                    bool bolUpdateResult = objContactInfoRepository.UpdateContactInfo(objContactInfoData);
                    if (bolUpdateResult)
                    {
                        objUpdateResponse.Result = MsgSuccess;
                        objUpdateResponse.Data = objContactInfoData;
                        objUpdateResponse.Sign = GetSign(objUpdateResponse, ApiSignKey);
                    }
                    else
                    {
                        objUpdateResponse.Result = $"{MsgFail} : Update Data Error";
                    }
                }

                return objUpdateResponse;
                #endregion
            }
            catch (Exception ex)
            {
                #region [Exception]
                objUpdateResponse.Result = $"{MsgException} : {ex.Message}";
                return objUpdateResponse;
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

            #region [UpdateRequest]
            if (objData is UpdateRequest)
            {
                strParams = $"{strKey}&ContactInfoID={((UpdateRequest)objData).ContactInfoID}&{strKey}";
            }
            #endregion

            #region [UpdateResponse]
            if (objData is UpdateResponse)
            {
                strParams = $"{strKey}&Result={((UpdateResponse)objData).Result}&ContactInfoID={((UpdateResponse)objData).Data.ContactInfoID}&{strKey}";
            }
            #endregion

            //Log.Info("Sign calculate parameter : " + strParams);
            return Utility.GetSHA(strParams);
        }
    }
}