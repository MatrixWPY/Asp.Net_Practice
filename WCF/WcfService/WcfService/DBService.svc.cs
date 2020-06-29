using System;
using System.Collections.Generic;
using WcfService.Model;
using WcfService.Repository;

namespace WcfService
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼、svc 和組態檔中的類別名稱 "Service1"。
    // 注意: 若要啟動 WCF 測試用戶端以便測試此服務，請在 [方案總管] 中選取 Service1.svc 或 Service1.svc.cs，然後開始偵錯。
    public class DBService : IDBService
    {
        public List<ContactInfoExModel> GetContactInfoByCondition(QueryBaseModel objQueryBaseModel)
        {
            List<ContactInfoExModel> liContactInfoExModel = null;

            try
            {
                ContactInfoRepository objContactInfoRepo = new ContactInfoRepository();
                liContactInfoExModel = objContactInfoRepo.GetContactInfoByCondition(objQueryBaseModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return liContactInfoExModel;
        }

        public ContactInfoModel GetContactInfo(long lContactInfoID)
        {
            ContactInfoModel objContactInfoModel = null;

            try
            {
                ContactInfoRepository objContactInfoRepo = new ContactInfoRepository();
                objContactInfoModel = objContactInfoRepo.GetContactInfo(lContactInfoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objContactInfoModel;
        }

        public long AddContactInfo(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                ContactInfoRepository objContactInfoRepo = new ContactInfoRepository();
                bolResult = objContactInfoRepo.AddContactInfo(objContactInfoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (bolResult ? objContactInfoModel.ContactInfoID : -1);
        }

        public bool UpdateContactInfo(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                ContactInfoRepository objContactInfoRepo = new ContactInfoRepository();
                bolResult = objContactInfoRepo.UpdateContactInfo(objContactInfoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool DeleteContactInfo(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                ContactInfoRepository objContactInfoRepo = new ContactInfoRepository();
                bolResult = objContactInfoRepo.DeleteContactInfo(objContactInfoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public long AddUpdateContactInfo(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                ContactInfoRepository objContactInfoRepo = new ContactInfoRepository();
                bolResult = objContactInfoRepo.AddUpdateContactInfo(objContactInfoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (bolResult ? objContactInfoModel.ContactInfoID : -1);
        }

        #region [WebAPI RESTful]
        public ContactInfoModel GetContactInfoRESTful(long lContactInfoID)
        {
            ContactInfoModel objContactInfoModel = null;

            try
            {
                ContactInfoRESTfulRepository objContactInfoRESTfulRepo = new ContactInfoRESTfulRepository();
                objContactInfoModel = objContactInfoRESTfulRepo.Query(lContactInfoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objContactInfoModel;
        }

        public long InsertContactInfoRESTful(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                ContactInfoRESTfulRepository objContactInfoRESTfulRepo = new ContactInfoRESTfulRepository();
                bolResult = objContactInfoRESTfulRepo.Insert(objContactInfoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (bolResult ? objContactInfoModel.ContactInfoID : -1);
        }

        public bool UpdateContactInfoRESTful(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                ContactInfoRESTfulRepository objContactInfoRESTfulRepo = new ContactInfoRESTfulRepository();
                bolResult = objContactInfoRESTfulRepo.Update(objContactInfoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool DeleteContactInfoRESTful(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                ContactInfoRESTfulRepository objContactInfoRESTfulRepo = new ContactInfoRESTfulRepository();
                bolResult = objContactInfoRESTfulRepo.Delete(objContactInfoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }
        #endregion

        #region [WebMVC EF]
        public int GetContactInfoEFTotalCnt()
        {
            int iTotalCnt = 0;

            try
            {
                ContactInfoEFRepository objContactInfoEFRepo = new ContactInfoEFRepository();
                iTotalCnt = objContactInfoEFRepo.QueryTotalCnt();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iTotalCnt;
        }

        public List<Tbl_ContactInfo> GetContactInfoEFByCondition(QueryBaseModel objQueryBaseModel)
        {
            List<Tbl_ContactInfo> liContactInfo = null;

            try
            {
                ContactInfoEFRepository objContactInfoEFRepo = new ContactInfoEFRepository();
                liContactInfo = objContactInfoEFRepo.QueryByCondition(objQueryBaseModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return liContactInfo;
        }

        public Tbl_ContactInfo GetContactInfoEF(long lContactInfoID)
        {
            Tbl_ContactInfo objContactInfo = null;

            try
            {
                ContactInfoEFRepository objContactInfoEFRepo = new ContactInfoEFRepository();
                objContactInfo = objContactInfoEFRepo.Query(lContactInfoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objContactInfo;
        }

        public long InsertContactInfoEF(Tbl_ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                ContactInfoEFRepository objContactInfoEFRepo = new ContactInfoEFRepository();
                bolResult = objContactInfoEFRepo.Insert(objContactInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (bolResult ? objContactInfo.ContactInfoID : -1);
        }

        public bool UpdateContactInfoEF(Tbl_ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                ContactInfoEFRepository objContactInfoEFRepo = new ContactInfoEFRepository();
                bolResult = objContactInfoEFRepo.Update(objContactInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool DeleteContactInfoEF(Tbl_ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                ContactInfoEFRepository objContactInfoEFRepo = new ContactInfoEFRepository();
                bolResult = objContactInfoEFRepo.Delete(objContactInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }
        #endregion
    }
}
