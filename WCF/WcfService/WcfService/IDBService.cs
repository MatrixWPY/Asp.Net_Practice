using System.Collections.Generic;
using System.ServiceModel;
using WcfService.Model;

namespace WcfService
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的介面名稱 "IService1"。
    [ServiceContract]
    public interface IDBService
    {
        // TODO: 在此新增您的服務作業
        [OperationContract]
        List<ContactInfoExModel> GetContactInfoByCondition(QueryBaseModel objQueryBaseModel);

        [OperationContract]
        ContactInfoModel GetContactInfo(long lContactInfoID);

        [OperationContract]
        long AddContactInfo(ContactInfoModel objContactInfoModel);

        [OperationContract]
        bool UpdateContactInfo(ContactInfoModel objContactInfoModel);

        [OperationContract]
        bool DeleteContactInfo(ContactInfoModel objContactInfoModel);

        [OperationContract]
        long AddUpdateContactInfo(ContactInfoModel objContactInfoModel);

        #region [WebAPI RESTful]
        [OperationContract]
        ContactInfoModel GetContactInfoRESTful(long lContactInfoID);

        [OperationContract]
        long InsertContactInfoRESTful(ContactInfoModel objContactInfoModel);

        [OperationContract]
        bool UpdateContactInfoRESTful(ContactInfoModel objContactInfoModel);

        [OperationContract]
        bool DeleteContactInfoRESTful(ContactInfoModel objContactInfoModel);
        #endregion

        #region [WebMVC EF]
        [OperationContract]
        int GetContactInfoEFTotalCnt();

        [OperationContract]
        List<Tbl_ContactInfo> GetContactInfoEFByCondition(QueryBaseModel objQueryBaseModel);

        [OperationContract]
        Tbl_ContactInfo GetContactInfoEF(long lContactInfoID);

        [OperationContract]
        long InsertContactInfoEF(Tbl_ContactInfo objContactInfo);

        [OperationContract]
        bool UpdateContactInfoEF(Tbl_ContactInfo objContactInfo);

        [OperationContract]
        bool DeleteContactInfoEF(Tbl_ContactInfo objContactInfo);
        #endregion
    }
}
