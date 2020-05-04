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

        public bool AddContactInfo(ContactInfoModel objContactInfoModel)
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

            return bolResult;
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

        public bool AddUpdateContactInfo(ContactInfoModel objContactInfoModel)
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

            return bolResult;
        }
    }
}
