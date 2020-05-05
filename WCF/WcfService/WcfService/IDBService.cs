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
    }
}
