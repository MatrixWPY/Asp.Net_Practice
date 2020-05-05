using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WcfService.Model;

namespace WcfService.Repository
{
    public class ContactInfoRepository : DapperBaseRepository
    {
        public ContactInfoRepository(IDbConnection objConnect = null) : base(objConnect)
        {
        }

        public List<ContactInfoExModel> GetContactInfoByCondition(QueryBaseModel objQueryBaseModel)
        {
            List<ContactInfoExModel> liContactInfoExData = null;

            try
            {
                using (var objConnect = GetDBConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("SELECT *, (SELECT COUNT(1) FROM Tbl_ContactInfo) AS TotalCount FROM Tbl_ContactInfo");
                    sbSQL.AppendLine("WHERE 1=1");

                    #region [Query Condition]
                    if (objQueryBaseModel.QueryParam.Any())
                    {
                        if (objQueryBaseModel.QueryParam.ContainsKey("IsEnable"))
                        {
                            sbSQL.AppendLine("AND IsEnable=@IsEnable");
                        }
                        if (objQueryBaseModel.QueryParam.ContainsKey("Name"))
                        {
                            sbSQL.AppendLine("AND Name LIKE @Name");
                        }
                        if (objQueryBaseModel.QueryParam.ContainsKey("Nickname"))
                        {
                            sbSQL.AppendLine("AND Nickname LIKE @Nickname");
                        }
                    }
                    #endregion

                    #region [Order]
                    string strSort = objQueryBaseModel.DataTableParam.OrderColumn + " " + objQueryBaseModel.DataTableParam.OrderDir;
                    if (!string.IsNullOrWhiteSpace(strSort))
                    {
                        sbSQL.AppendLine("ORDER BY");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Name ASC' THEN Name END ASC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Name DESC' THEN Name END DESC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Nickname ASC' THEN Nickname END ASC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Nickname DESC' THEN Nickname END DESC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Gender ASC' THEN Gender END ASC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Gender DESC' THEN Gender END DESC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Age ASC' THEN Age END ASC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Age DESC' THEN Age END DESC");
                    }
                    else
                    {
                        sbSQL.AppendLine("ORDER BY ContactInfoID ASC");
                    }
                    #endregion

                    #region[Paging]
                    if (null != objQueryBaseModel.DataTableParam.PageStartRow && null != objQueryBaseModel.DataTableParam.PageRowCnt)
                    {
                        sbSQL.AppendLine("OFFSET @Start ROWS FETCH NEXT @Length ROWS ONLY");
                    }
                    #endregion

                    liContactInfoExData = objConnect.Query<ContactInfoExModel>(sbSQL.ToString(), new {
                        IsEnable = (objQueryBaseModel.QueryParam.ContainsKey("IsEnable") ? objQueryBaseModel.QueryParam["IsEnable"] : 0),
                        Name = (objQueryBaseModel.QueryParam.ContainsKey("Name") ? "%" + objQueryBaseModel.QueryParam["Name"] + "%" : string.Empty),
                        Nickname = (objQueryBaseModel.QueryParam.ContainsKey("Nickname") ? "%" + objQueryBaseModel.QueryParam["Nickname"] + "%" : string.Empty),
                        Sort = strSort ?? string.Empty,
                        Start = objQueryBaseModel.DataTableParam.PageStartRow ?? 0,
                        Length = objQueryBaseModel.DataTableParam.PageRowCnt ?? 0
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return liContactInfoExData;
        }

        public ContactInfoModel GetContactInfo(long lContactInfoID)
        {
            ContactInfoModel objContactInfoData = null;

            try
            {
                using (var objConnect = GetDBConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("SELECT * FROM Tbl_ContactInfo");
                    sbSQL.AppendLine("WHERE ContactInfoID=@ContactInfoID");
                    sbSQL.AppendLine("ORDER BY ContactInfoID DESC");

                    objContactInfoData = objConnect.Query<ContactInfoModel>(sbSQL.ToString(), new { ContactInfoID = lContactInfoID }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objContactInfoData;
        }

        public bool AddContactInfo(ContactInfoModel objContactInfoModel, IDbTransaction objOrgTran = null)
        {
            bool bolResult = false;

            try
            {
                if (null == objOrgTran)
                {
                    using (var objConnect = GetDBConnection())
                    {
                        objContactInfoModel.IsEnable = true;
                        objContactInfoModel.CreateTime = DateTime.Now;
                        long? ContactInfoID = objConnect.Insert(objContactInfoModel);

                        //Set PK
                        objContactInfoModel.ContactInfoID = Convert.ToInt64(ContactInfoID);
                    }
                }
                else
                {
                    objContactInfoModel.IsEnable = true;
                    objContactInfoModel.CreateTime = DateTime.Now;
                    long? ContactInfoID = GetDBConnection().Insert(objContactInfoModel, objOrgTran);

                    //Set PK
                    objContactInfoModel.ContactInfoID = Convert.ToInt64(ContactInfoID);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool UpdateContactInfo(ContactInfoModel objContactInfoModel, IDbTransaction objOrgTran = null)
        {
            bool bolResult = false;

            try
            {
                if (null == objOrgTran)
                {
                    using (var objConnect = GetDBConnection())
                    {
                        objContactInfoModel.UpdateTime = DateTime.Now;
                        objConnect.Update(objContactInfoModel);
                    }
                }
                else
                {
                    objContactInfoModel.UpdateTime = DateTime.Now;
                    GetDBConnection().Update(objContactInfoModel, objOrgTran);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool DeleteContactInfo(ContactInfoModel objContactInfoModel, IDbTransaction objOrgTran = null)
        {
            bool bolResult = false;

            try
            {
                if (null == objOrgTran)
                {
                    using (var objConnect = GetDBConnection())
                    {
                        objConnect.Delete(objContactInfoModel);
                    }
                }
                else
                {
                    GetDBConnection().Delete(objContactInfoModel, objOrgTran);
                }

                bolResult = true;
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
                using (var objConnect = GetDBConnection())
                {
                    using (var objTran = objConnect.BeginTransaction())
                    {
                        try
                        {
                            AddContactInfo(objContactInfoModel, objTran);
                            objContactInfoModel.Nickname = (string.IsNullOrWhiteSpace(objContactInfoModel.Nickname) ? objContactInfoModel.Name : objContactInfoModel.Nickname);
                            UpdateContactInfo(objContactInfoModel, objTran);

                            objTran.Commit();
                            bolResult = true;
                        }
                        catch (Exception ex)
                        {
                            objTran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }
    }
}