﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Filters;
using WebMVC.Models.Data;
using WebMVC.Models.Repository;
using WebMVC.ViewModel;
using WebMVC.ViewModel.ContactInfo;

namespace WebMVC.Controllers
{
    public class ContactInfoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult Index(IndexVM objIndexVM)
        {
            try
            {
                QueryBaseData objQueryBaseData = new QueryBaseData();
                objQueryBaseData.QueryParam["IsEnable"] = 1;
                if (!string.IsNullOrWhiteSpace(objIndexVM.Name))
                {
                    objQueryBaseData.QueryParam["Name"] = objIndexVM.Name.Trim();
                }
                if (!string.IsNullOrWhiteSpace(objIndexVM.Nickname))
                {
                    objQueryBaseData.QueryParam["Nickname"] = objIndexVM.Nickname.Trim();
                }
                objQueryBaseData.DataTableParam.PageStartRow = objIndexVM.Start;
                objQueryBaseData.DataTableParam.PageRowCnt = objIndexVM.Length;
                objQueryBaseData.DataTableParam.OrderColumn = objIndexVM.OrderBy;
                objQueryBaseData.DataTableParam.OrderDir = objIndexVM.OrderDir.ToString();

                ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                List<ContactInfoExData> liContactInfoExData = objContactInfoRepository.GetContactInfoByCondition(objQueryBaseData);

                int iTotalCount = (liContactInfoExData.Any() ? liContactInfoExData.Select(e => e.TotalCount).FirstOrDefault() : 0);
                DataTableResVM<ContactInfoData> objDataTableResVM = new DataTableResVM<ContactInfoData>(objIndexVM.Draw, iTotalCount, iTotalCount, liContactInfoExData);
                return Content(JsonConvert.SerializeObject(objDataTableResVM), "application/json");
            }
            catch (Exception ex)
            {
                DataTableResVM<ContactInfoData> objDataTableResVM = new DataTableResVM<ContactInfoData>(ex.Message);
                return Content(JsonConvert.SerializeObject(objDataTableResVM), "application/json");
            }
        }

        public ActionResult Details(long lContactInfoID)
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(lContactInfoID);
            return View(objContactInfoData);
        }

        public ActionResult Create()
        {
            return View(new ContactInfoData());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactInfoData objContactInfoData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                    objContactInfoRepository.AddContactInfo(objContactInfoData);
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
                return View(objContactInfoData);
            }
        }

        public ActionResult Edit(long lContactInfoID)
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(lContactInfoID);
            return View(objContactInfoData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long lContactInfoID, FormCollection objFormCollection)
        {
            try
            {
                ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(lContactInfoID);
                if (null != objContactInfoData && TryUpdateModel<ContactInfoData>(objContactInfoData, "", objFormCollection.AllKeys, new string[] { "ContactInfoID", "CreateTime", "UpdateTime" }))
                {
                    objContactInfoRepository.UpdateContactInfo(objContactInfoData);
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

        public ActionResult Delete(long lContactInfoID)
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(lContactInfoID);
            return View(objContactInfoData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long lContactInfoID, FormCollection objFormCollection)
        {
            try
            {
                ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(lContactInfoID);
                if (null != objContactInfoData)
                {
                    objContactInfoRepository.DeleteContactInfo(objContactInfoData);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_AjaxFormPost(ContactInfoData objContactInfoData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                    objContactInfoRepository.AddContactInfo(objContactInfoData);
                    var returnData = new
                    {
                        IsSuccess = true
                    };
                    return Content(JsonConvert.SerializeObject(returnData), "application/json");
                }
                else
                {
                    ModelState.AddModelError("InsertError", "新增失敗!");
                    var returnData = new
                    {
                        IsSuccess = false,
                        ModelStateErrors = ModelState.Where(p => p.Value.Errors.Count > 0).ToDictionary(p => p.Key, p => p.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                    };
                    return Content(JsonConvert.SerializeObject(returnData), "application/json");
                }
            }
            catch (Exception ex)
            {
                var returnData = new
                {
                    IsSuccess = false,
                    ExceptionMsg = ex.Message
                };
                return Content(JsonConvert.SerializeObject(returnData), "application/json");
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult Create_AjaxModelPost(ContactInfoData objContactInfoData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                    objContactInfoRepository.AddContactInfo(objContactInfoData);
                    var returnData = new
                    {
                        IsSuccess = true
                    };
                    return Content(JsonConvert.SerializeObject(returnData), "application/json");
                }
                else
                {
                    ModelState.AddModelError("InsertError", "新增失敗!");
                    var returnData = new
                    {
                        IsSuccess = false,
                        ModelStateErrors = ModelState.Where(p => p.Value.Errors.Count > 0).ToDictionary(p => p.Key, p => p.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                    };
                    return Content(JsonConvert.SerializeObject(returnData), "application/json");
                }
            }
            catch (Exception ex)
            {
                var returnData = new
                {
                    IsSuccess = false,
                    ExceptionMsg = ex.Message
                };
                return Content(JsonConvert.SerializeObject(returnData), "application/json");
            }
        }

        public ActionResult CreateUpdate()
        {
            return View(new ContactInfoData());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(ContactInfoData objContactInfoData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                    objContactInfoRepository.AddUpdateContactInfo(objContactInfoData);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("InsertUpdateError", "新增+更新失敗!");
                    return View(objContactInfoData);
                }
            }
            catch
            {
                ModelState.AddModelError("InsertUpdateError", "新增+更新失敗!");
                return View(objContactInfoData);
            }
        }
    }
}
