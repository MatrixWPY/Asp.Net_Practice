using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Filters;
using WebMVC.Models.Data;
using WebMVC.Models.Repository;
using WebMVC.ViewModel;
using WebMVC.ViewModel.ContactInfoEF;

namespace WebMVC.Controllers
{
    public class ContactInfoEFController : Controller
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
                objQueryBaseData.QueryParam = new Dictionary<string, object>();
                objQueryBaseData.QueryParam["IsEnable"] = 1;
                if (!string.IsNullOrWhiteSpace(objIndexVM.Name))
                {
                    objQueryBaseData.QueryParam["Name"] = objIndexVM.Name.Trim();
                }
                if (!string.IsNullOrWhiteSpace(objIndexVM.Nickname))
                {
                    objQueryBaseData.QueryParam["Nickname"] = objIndexVM.Nickname.Trim();
                }
                objQueryBaseData.DataTableParam = new QueryBaseData.DataTableBase();
                objQueryBaseData.DataTableParam.PageStartRow = objIndexVM.Start;
                objQueryBaseData.DataTableParam.PageRowCnt = objIndexVM.Length;
                objQueryBaseData.DataTableParam.OrderColumn = objIndexVM.OrderBy;
                objQueryBaseData.DataTableParam.OrderDir = objIndexVM.OrderDir.ToString();

                ContactInfoEFRepository objContactInfoEFRepository = new ContactInfoEFRepository();
                int iTotalCount = objContactInfoEFRepository.QueryTotalCnt();
                List<Tbl_ContactInfo> liContactInfo = objContactInfoEFRepository.QueryByCondition(objQueryBaseData).ToList();

                DataTableResVM<Tbl_ContactInfo> objDataTableResVM = new DataTableResVM<Tbl_ContactInfo>(objIndexVM.Draw, iTotalCount, iTotalCount, liContactInfo);
                return Content(JsonConvert.SerializeObject(objDataTableResVM), "application/json");
            }
            catch (Exception ex)
            {
                DataTableResVM<ContactInfoData> objDataTableResVM = new DataTableResVM<ContactInfoData>(ex.Message);
                return Content(JsonConvert.SerializeObject(objDataTableResVM), "application/json");
            }
        }

        public ActionResult Details(int id)
        {
            ContactInfoEFRepository objContactInfoEFRepository = new ContactInfoEFRepository();
            Tbl_ContactInfo objContactInfo = objContactInfoEFRepository.Query(id);
            return View(objContactInfo);
        }

        public ActionResult Create()
        {
            return View(new CreateEditVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEditVM objCreateEditVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tbl_ContactInfo objContactInfo = new Tbl_ContactInfo();
                    objContactInfo.Name = objCreateEditVM.Name.Trim();
                    objContactInfo.Nickname = (!string.IsNullOrWhiteSpace(objCreateEditVM.Nickname) ? objCreateEditVM.Nickname.Trim() : null);
                    objContactInfo.Gender = (byte?)objCreateEditVM.Gender;
                    objContactInfo.Age = (byte?)objCreateEditVM.Age;
                    objContactInfo.PhoneNo = objCreateEditVM.PhoneNo.Trim();
                    objContactInfo.Address = objCreateEditVM.Address.Trim();
                    objContactInfo.IsEnable = true;
                    objContactInfo.CreateTime = DateTime.Now;

                    ContactInfoEFRepository objContactInfoEFRepository = new ContactInfoEFRepository();
                    objContactInfoEFRepository.Insert(objContactInfo);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("InsertError", "新增失敗!");
                    return View(objCreateEditVM);
                }
            }
            catch
            {
                ModelState.AddModelError("InsertError", "新增失敗!");
                return View(objCreateEditVM);
            }
        }

        public ActionResult Edit(int id)
        {
            ContactInfoEFRepository objContactInfoEFRepository = new ContactInfoEFRepository();
            Tbl_ContactInfo objContactInfo = objContactInfoEFRepository.Query(id);

            CreateEditVM objCreateEditVM = new CreateEditVM();
            objCreateEditVM.ContactInfoID = objContactInfo.ContactInfoID;
            objCreateEditVM.Name = objContactInfo.Name;
            objCreateEditVM.Nickname = objContactInfo.Nickname;
            objCreateEditVM.Gender = (int?)objContactInfo.Gender;
            objCreateEditVM.Age = objContactInfo.Age;
            objCreateEditVM.PhoneNo = objContactInfo.PhoneNo;
            objCreateEditVM.Address = objContactInfo.Address;
            objCreateEditVM.IsEnable = objContactInfo.IsEnable;
            return View(objCreateEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection objFormCollection)
        {
            try
            {
                ContactInfoEFRepository objContactInfoEFRepository = new ContactInfoEFRepository();
                Tbl_ContactInfo objContactInfo = objContactInfoEFRepository.Query(id);
                if (null != objContactInfo && TryUpdateModel<Tbl_ContactInfo>(objContactInfo, "", objFormCollection.AllKeys, new string[] { "ContactInfoID", "CreateTime", "UpdateTime" }))
                {
                    objContactInfo.UpdateTime = DateTime.Now;
                    objContactInfoEFRepository.Update(objContactInfo);
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

        public ActionResult Delete(int id)
        {
            ContactInfoEFRepository objContactInfoEFRepository = new ContactInfoEFRepository();
            Tbl_ContactInfo objContactInfo = objContactInfoEFRepository.Query(id);
            return View(objContactInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ContactInfoEFRepository objContactInfoEFRepository = new ContactInfoEFRepository();
                Tbl_ContactInfo objContactInfo = objContactInfoEFRepository.Query(id);
                if (null != objContactInfo)
                {
                    objContactInfoEFRepository.Delete(objContactInfo);
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
    }
}