using System;
using System.Configuration;
using WebAPI.DBServiceReference;
using WebAPI.Models.ContactInfo;
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
            DBServiceClient clientDB = new DBServiceClient();

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
                ContactInfoModel objContactInfoModel = clientDB.GetContactInfo(Convert.ToInt64(objQueryRequest.ContactInfoID));
                if (null == objContactInfoModel)
                {
                    objQueryResponse.Result = $"{MsgFail} : No Data";
                }
                else
                {
                    objQueryResponse.Result = MsgSuccess;
                    objQueryResponse.Data = objContactInfoModel;
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
            DBServiceClient clientDB = new DBServiceClient();

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
                ContactInfoModel objContactInfoModel = new ContactInfoModel();
                objContactInfoModel.Name = objAddRequest.Name.Trim();
                objContactInfoModel.Nickname = (!string.IsNullOrWhiteSpace(objAddRequest.Nickname) ? objAddRequest.Nickname.Trim() : null);
                objContactInfoModel.Gender = (ContactInfoModel.EnumGender?)objAddRequest.Gender;
                objContactInfoModel.Age = objAddRequest.Age;
                objContactInfoModel.PhoneNo = objAddRequest.PhoneNo.Trim();
                objContactInfoModel.Address = objAddRequest.Address.Trim();

                objContactInfoModel.ContactInfoID = clientDB.AddContactInfo(objContactInfoModel);
                if (objContactInfoModel.ContactInfoID > 0)
                {
                    objAddResponse.Result = MsgSuccess;
                    objAddResponse.Data = objContactInfoModel;
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
            DBServiceClient clientDB = new DBServiceClient();

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
                ContactInfoModel objContactInfoModel = clientDB.GetContactInfo(Convert.ToInt64(objUpdateRequest.ContactInfoID));
                if (null == objContactInfoModel)
                {
                    objUpdateResponse.Result = $"{MsgFail} : No Data";
                }
                else
                {
                    objContactInfoModel.Name = objUpdateRequest.Name.Trim();
                    objContactInfoModel.Nickname = (!string.IsNullOrWhiteSpace(objUpdateRequest.Nickname) ? objUpdateRequest.Nickname.Trim() : null);
                    objContactInfoModel.Gender = (ContactInfoModel.EnumGender?)objUpdateRequest.Gender;
                    objContactInfoModel.Age = objUpdateRequest.Age;
                    objContactInfoModel.PhoneNo = objUpdateRequest.PhoneNo.Trim();
                    objContactInfoModel.Address = objUpdateRequest.Address.Trim();

                    bool bolUpdateResult = clientDB.UpdateContactInfo(objContactInfoModel);
                    if (bolUpdateResult)
                    {
                        objUpdateResponse.Result = MsgSuccess;
                        objUpdateResponse.Data = objContactInfoModel;
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

        public DeleteResponse Delete(DeleteRequest objDeleteRequest)
        {
            DeleteResponse objDeleteResponse = new DeleteResponse();
            DBServiceClient clientDB = new DBServiceClient();

            try
            {
                #region [Validation]
                bool bolCheckSign = Utility.CheckSHA(objDeleteRequest.Sign.Trim(), GetSign(objDeleteRequest, ApiSignKey));
                if (!bolCheckSign)
                {
                    objDeleteResponse.Result = $"{MsgFail} : Sign Validate Error";
                    return objDeleteResponse;
                }
                #endregion

                #region [Logic]
                ContactInfoModel objContactInfoModel = clientDB.GetContactInfo(Convert.ToInt64(objDeleteRequest.ContactInfoID));
                if (null == objContactInfoModel)
                {
                    objDeleteResponse.Result = $"{MsgFail} : No Data";
                }
                else
                {
                    bool bolDeleteResult = clientDB.DeleteContactInfo(objContactInfoModel);
                    if (bolDeleteResult)
                    {
                        objDeleteResponse.Result = MsgSuccess;
                        objDeleteResponse.Sign = GetSign(objDeleteResponse, ApiSignKey);
                    }
                    else
                    {
                        objDeleteResponse.Result = $"{MsgFail} : Delete Data Error";
                    }
                }

                return objDeleteResponse;
                #endregion
            }
            catch (Exception ex)
            {
                #region [Exception]
                objDeleteResponse.Result = $"{MsgException} : {ex.Message}";
                return objDeleteResponse;
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

            #region [DeleteRequest]
            if (objData is DeleteRequest)
            {
                strParams = $"{strKey}&ContactInfoID={((DeleteRequest)objData).ContactInfoID}&{strKey}";
            }
            #endregion

            #region [DeleteResponse]
            if (objData is DeleteResponse)
            {
                strParams = $"{strKey}&Result={((DeleteResponse)objData).Result}&{strKey}";
            }
            #endregion

            //Log.Info("Sign calculate parameter : " + strParams);
            return Utility.GetSHA(strParams);
        }
    }
}