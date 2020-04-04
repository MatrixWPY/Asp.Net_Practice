using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Filters;
using WebMVC.Models.Data;
using WebMVC.Models.Repository;
using WebMVC.Models.ViewModel;

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
        public ActionResult Index(ContactInfoReqVM objContactInfoReqVM)
        {
            DataTableQueryData objDataTableQueryData = new DataTableQueryData();
            objDataTableQueryData.DataTableParam.PageStartRow = objContactInfoReqVM.Start;
            objDataTableQueryData.DataTableParam.PageRowCnt = objContactInfoReqVM.Length;
            objDataTableQueryData.DataTableParam.OrderColumn = objContactInfoReqVM.OrderBy;
            objDataTableQueryData.DataTableParam.OrderDir = objContactInfoReqVM.OrderDir.ToString();
            objDataTableQueryData.QueryParam["IsEnable"] = "1";
            if (!string.IsNullOrWhiteSpace(objContactInfoReqVM.Name))
            {
                objDataTableQueryData.QueryParam["Name"] = objContactInfoReqVM.Name.Trim();
            }
            if (!string.IsNullOrWhiteSpace(objContactInfoReqVM.Nickname))
            {
                objDataTableQueryData.QueryParam["Nickname"] = objContactInfoReqVM.Nickname.Trim();
            }

            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            List<ContactInfoExData> liContactInfoExData = objContactInfoRepository.GetContactInfoByCondition(objDataTableQueryData);

            int iTotalCount = (liContactInfoExData.Any() ? liContactInfoExData.Select(e => e.TotalCount).FirstOrDefault() : 0);
            var result = new DataTableResVM<ContactInfoData>(objContactInfoReqVM.Draw, iTotalCount, iTotalCount, liContactInfoExData);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult Details(long lContactInfoID)
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(lContactInfoID);
            return View(objContactInfoData);
        }

        public ActionResult Create()
        {
            return View();
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
    }
}
