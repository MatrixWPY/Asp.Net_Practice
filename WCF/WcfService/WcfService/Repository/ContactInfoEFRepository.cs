using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using WcfService.Model;

namespace WcfService.Repository
{
    public class ContactInfoEFRepository
    {
        public int QueryTotalCnt()
        {
            int iTotalCnt = 0;

            try
            {
                using (var db = new LocalDBEntities())
                {
                    iTotalCnt = db.Tbl_ContactInfo.Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iTotalCnt;
        }

        public List<Tbl_ContactInfo> QueryByCondition(QueryBaseModel objQueryBaseModel)
        {
            List<Tbl_ContactInfo> liContactInfo = null;

            try
            {
                using (var db = new LocalDBEntities())
                {
                    var filterData = db.Tbl_ContactInfo.AsQueryable();

                    #region [Query Condition]
                    if (objQueryBaseModel.QueryParam.Any())
                    {
                        if (objQueryBaseModel.QueryParam.ContainsKey("IsEnable"))
                        {
                            bool bolIsEnable = Convert.ToBoolean(objQueryBaseModel.QueryParam["IsEnable"]);
                            filterData = filterData.Where(e => e.IsEnable == bolIsEnable);
                        }
                        if (objQueryBaseModel.QueryParam.ContainsKey("Name"))
                        {
                            string strName = Convert.ToString(objQueryBaseModel.QueryParam["Name"]);
                            filterData = filterData.Where(e => e.Name.Contains(strName));
                        }
                        if (objQueryBaseModel.QueryParam.ContainsKey("Nickname"))
                        {
                            string strNickname = Convert.ToString(objQueryBaseModel.QueryParam["Nickname"]);
                            filterData = filterData.Where(e => e.Nickname.Contains(strNickname));
                        }
                    }
                    #endregion

                    #region [Order]
                    string strSort = objQueryBaseModel.DataTableParam.OrderColumn + " " + objQueryBaseModel.DataTableParam.OrderDir;
                    if (!string.IsNullOrWhiteSpace(strSort))
                    {
                        filterData = filterData.OrderBy(strSort);
                    }
                    #endregion

                    #region[Paging]
                    if (null != objQueryBaseModel.DataTableParam.PageStartRow && null != objQueryBaseModel.DataTableParam.PageRowCnt)
                    {
                        int iPageStartRow = objQueryBaseModel.DataTableParam.PageStartRow ?? 0;
                        int iPageRowCnt = objQueryBaseModel.DataTableParam.PageRowCnt ?? 0;
                        filterData = filterData.Skip(iPageStartRow).Take(iPageRowCnt);
                    }
                    #endregion

                    liContactInfo = filterData.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return liContactInfo;
        }

        public Tbl_ContactInfo Query(long lContactInfoID)
        {
            Tbl_ContactInfo objContactInfo = null;

            try
            {
                using (var db = new LocalDBEntities())
                {
                    objContactInfo = db.Tbl_ContactInfo.Where(e => e.ContactInfoID == lContactInfoID).OrderByDescending(e => e.ContactInfoID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objContactInfo;
        }

        public bool Insert(Tbl_ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                using (var db = new LocalDBEntities())
                {
                    db.Tbl_ContactInfo.Add(objContactInfo);
                    db.SaveChanges();
                }
                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool Update(Tbl_ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                using (var db = new LocalDBEntities())
                {
                    Tbl_ContactInfo objOrgContactInfo = db.Tbl_ContactInfo.Find(objContactInfo.ContactInfoID);
                    db.Entry(objOrgContactInfo).CurrentValues.SetValues(objContactInfo);
                    db.SaveChanges();
                }
                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool Delete(Tbl_ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                using (var db = new LocalDBEntities())
                {
                    Tbl_ContactInfo objOrgContactInfo = db.Tbl_ContactInfo.Find(objContactInfo.ContactInfoID);
                    db.Tbl_ContactInfo.Remove(objOrgContactInfo);
                    db.SaveChanges();
                }
                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }
    }
}