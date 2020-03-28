using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models.Data;
using Dapper;
using System.Text;

namespace WebMVC.Controllers
{
    public class ContactInfoController : Controller
    {
        private static readonly ConnectionStringSettings ConnectionSet = ConfigurationManager.ConnectionStrings["DBConnect"];

        // GET: ContactInfo
        public ActionResult Index()
        {
            List<ContactInfoData> liContactInfoData = GetContactInfo();
            return View(liContactInfoData);
        }

        // GET: ContactInfo/Details/5
        public ActionResult Details(long ContactInfoID)
        {
            ContactInfoData objContactInfoData = GetContactInfo(ContactInfoID);
            return View(objContactInfoData);
        }

        // GET: ContactInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactInfo/Create
        [HttpPost]
        public ActionResult Create(ContactInfoData objContactInfoData)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    AddContactInfo(objContactInfoData);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("InsertError", "新增失敗!");
                    return View(objContactInfoData);
                }
            }
            catch
            {
                ModelState.AddModelError("InsertError", "新增失敗!");
                return View();
            }
        }

        // GET: ContactInfo/Edit/5
        public ActionResult Edit(long ContactInfoID)
        {
            ContactInfoData objContactInfoData = GetContactInfo(ContactInfoID);
            return View(objContactInfoData);
        }

        // POST: ContactInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(long ContactInfoID, FormCollection form)
        {
            try
            {
                // TODO: Add update logic here
                ContactInfoData objContactInfoData = GetContactInfo(ContactInfoID);
                if (null != objContactInfoData && TryUpdateModel<ContactInfoData>(objContactInfoData, "", form.AllKeys, new string[] { "ContactInfoID", "CreateTime", "UpdateTime" }))
                {
                    UpdateContactInfo(objContactInfoData);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UpdateError", "更新失敗!");
                    return View(objContactInfoData);
                }
            }
            catch
            {
                ModelState.AddModelError("UpdateError", "更新失敗!");
                return View();
            }
        }

        // GET: ContactInfo/Delete/5
        public ActionResult Delete(long ContactInfoID)
        {
            ContactInfoData objContactInfoData = GetContactInfo(ContactInfoID);
            return View(objContactInfoData);
        }

        // POST: ContactInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(long ContactInfoID, FormCollection form)
        {
            try
            {
                // TODO: Add delete logic here
                ContactInfoData objContactInfoData = GetContactInfo(ContactInfoID);
                if (null != objContactInfoData)
                {
                    DeleteContactInfo(objContactInfoData);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DeleteError", "刪除失敗!");
                    return View(objContactInfoData);
                }
            }
            catch
            {
                ModelState.AddModelError("DeleteError", "刪除失敗!");
                return View();
            }
        }

        private List<ContactInfoData> GetContactInfo()
        {
            List<ContactInfoData> liContactInfoData = null;

            using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where IsEnable=1");
                sbSQL.AppendLine("order by ContactInfoID desc");

                liContactInfoData = cn.Query<ContactInfoData>(sbSQL.ToString()).ToList();
            }

            return liContactInfoData;
        }

        private ContactInfoData GetContactInfo(long ContactInfoID)
        {
            ContactInfoData objContactInfoData = null;

            using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where ContactInfoID=@ContactInfoID");
                sbSQL.AppendLine("order by ContactInfoID desc");

                objContactInfoData = cn.Query<ContactInfoData>(sbSQL.ToString(), new { ContactInfoID = ContactInfoID }).FirstOrDefault();
            }

            return objContactInfoData;
        }

        private bool AddContactInfo(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    objContactInfoData.IsEnable = true;
                    objContactInfoData.CreateTime = DateTime.Now;

                    long? ContactInfoID = cn.Insert(objContactInfoData);

                    objContactInfoData.ContactInfoID = Convert.ToInt64(ContactInfoID);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        private bool UpdateContactInfo(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    objContactInfoData.UpdateTime = DateTime.Now;

                    cn.Update(objContactInfoData);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        private bool DeleteContactInfo(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    cn.Delete(objContactInfoData);
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
