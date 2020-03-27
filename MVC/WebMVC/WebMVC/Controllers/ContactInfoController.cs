﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models.DB;
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
            List<ContactInfo> liContactInfo = GetContactInfo();
            return View(liContactInfo);
        }

        // GET: ContactInfo/Details/5
        public ActionResult Details(long ContactInfoID)
        {
            ContactInfo objContactInfo = GetContactInfo(ContactInfoID);
            return View(objContactInfo);
        }

        // GET: ContactInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactInfo/Create
        [HttpPost]
        public ActionResult Create(ContactInfo objContactInfo)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    AddContactInfo(objContactInfo);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("InsertError", "新增失敗!");
                    return View(objContactInfo);
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
            ContactInfo objContactInfo = GetContactInfo(ContactInfoID);
            return View(objContactInfo);
        }

        // POST: ContactInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(long ContactInfoID, FormCollection form)
        {
            try
            {
                // TODO: Add update logic here
                ContactInfo objContactInfo = GetContactInfo(ContactInfoID);
                if (null != objContactInfo && TryUpdateModel<ContactInfo>(objContactInfo, "", form.AllKeys, new string[] { "ContactInfoID", "CreateTime", "UpdateTime" }))
                {
                    UpdateContactInfo(objContactInfo);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UpdateError", "更新失敗!");
                    return View(objContactInfo);
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
            ContactInfo objContactInfo = GetContactInfo(ContactInfoID);
            return View(objContactInfo);
        }

        // POST: ContactInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(long ContactInfoID, FormCollection form)
        {
            try
            {
                // TODO: Add delete logic here
                ContactInfo objContactInfo = GetContactInfo(ContactInfoID);
                if (null != objContactInfo)
                {
                    DeleteContactInfo(objContactInfo);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DeleteError", "刪除失敗!");
                    return View(objContactInfo);
                }
            }
            catch
            {
                ModelState.AddModelError("DeleteError", "刪除失敗!");
                return View();
            }
        }

        private List<ContactInfo> GetContactInfo()
        {
            List<ContactInfo> liContactInfo = null;

            using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where IsEnable=1");
                sbSQL.AppendLine("order by ContactInfoID desc");

                liContactInfo = cn.Query<ContactInfo>(sbSQL.ToString()).ToList();
            }

            return liContactInfo;
        }

        private ContactInfo GetContactInfo(long ContactInfoID)
        {
            ContactInfo objContactInfo = null;

            using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where ContactInfoID=@ContactInfoID");
                sbSQL.AppendLine("order by ContactInfoID desc");

                objContactInfo = cn.Query<ContactInfo>(sbSQL.ToString(), new { ContactInfoID = ContactInfoID }).FirstOrDefault();
            }

            return objContactInfo;
        }

        private bool AddContactInfo(ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    objContactInfo.IsEnable = true;
                    objContactInfo.CreateTime = DateTime.Now;

                    long? ContactInfoID = cn.Insert(objContactInfo);

                    objContactInfo.ContactInfoID = Convert.ToInt64(ContactInfoID);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        private bool UpdateContactInfo(ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    objContactInfo.UpdateTime = DateTime.Now;

                    cn.Update(objContactInfo);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        private bool DeleteContactInfo(ContactInfo objContactInfo)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    cn.Delete(objContactInfo);
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
