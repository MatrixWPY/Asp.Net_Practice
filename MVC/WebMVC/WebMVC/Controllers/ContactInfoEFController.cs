using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.DBServiceReference;
using WebMVC.Filters;
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
                QueryBaseModel objQueryBaseModel = new QueryBaseModel();
                objQueryBaseModel.QueryParam = new Dictionary<string, object>();
                objQueryBaseModel.QueryParam["IsEnable"] = 1;
                if (!string.IsNullOrWhiteSpace(objIndexVM.Name))
                {
                    objQueryBaseModel.QueryParam["Name"] = objIndexVM.Name.Trim();
                }
                if (!string.IsNullOrWhiteSpace(objIndexVM.Nickname))
                {
                    objQueryBaseModel.QueryParam["Nickname"] = objIndexVM.Nickname.Trim();
                }
                objQueryBaseModel.DataTableParam = new QueryBaseModel.DataTableBase();
                objQueryBaseModel.DataTableParam.PageStartRow = objIndexVM.Start;
                objQueryBaseModel.DataTableParam.PageRowCnt = objIndexVM.Length;
                objQueryBaseModel.DataTableParam.OrderColumn = objIndexVM.OrderBy;
                objQueryBaseModel.DataTableParam.OrderDir = objIndexVM.OrderDir.ToString();

                DBServiceClient clientDB = new DBServiceClient();
                int iTotalCount = clientDB.GetContactInfoEFTotalCnt();
                List<Tbl_ContactInfo> liContactInfo = clientDB.GetContactInfoEFByCondition(objQueryBaseModel).ToList();

                DataTableResVM <Tbl_ContactInfo> objDataTableResVM = new DataTableResVM<Tbl_ContactInfo>(objIndexVM.Draw, iTotalCount, iTotalCount, liContactInfo);
                return Content(JsonConvert.SerializeObject(objDataTableResVM), "application/json");
            }
            catch (Exception ex)
            {
                DataTableResVM<ContactInfoModel> objDataTableResVM = new DataTableResVM<ContactInfoModel>(ex.Message);
                return Content(JsonConvert.SerializeObject(objDataTableResVM), "application/json");
            }
        }

        public ActionResult Details(int id)
        {
            DBServiceClient clientDB = new DBServiceClient();
            Tbl_ContactInfo objContactInfo = clientDB.GetContactInfoEF(id);
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

                    DBServiceClient clientDB = new DBServiceClient();
                    clientDB.InsertContactInfoEF(objContactInfo);
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
            DBServiceClient clientDB = new DBServiceClient();
            Tbl_ContactInfo objContactInfo = clientDB.GetContactInfoEF(id);

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
                DBServiceClient clientDB = new DBServiceClient();
                Tbl_ContactInfo objContactInfo = clientDB.GetContactInfoEF(id);
                if (null != objContactInfo && TryUpdateModel<Tbl_ContactInfo>(objContactInfo, "", objFormCollection.AllKeys, new string[] { "ContactInfoID", "CreateTime", "UpdateTime" }))
                {
                    objContactInfo.UpdateTime = DateTime.Now;
                    clientDB.UpdateContactInfoEF(objContactInfo);
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
            DBServiceClient clientDB = new DBServiceClient();
            Tbl_ContactInfo objContactInfo = clientDB.GetContactInfoEF(id);
            return View(objContactInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                DBServiceClient clientDB = new DBServiceClient();
                Tbl_ContactInfo objContactInfo = clientDB.GetContactInfoEF(id);
                if (null != objContactInfo)
                {
                    clientDB.DeleteContactInfoEF(objContactInfo);
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
