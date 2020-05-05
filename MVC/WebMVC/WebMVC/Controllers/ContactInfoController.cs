using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.DBServiceReference;
using WebMVC.Filters;
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
                List<ContactInfoExModel> liContactInfoExData = clientDB.GetContactInfoByCondition(objQueryBaseModel).ToList();

                int iTotalCount = (liContactInfoExData.Any() ? liContactInfoExData.Select(e => e.TotalCount).FirstOrDefault() : 0);
                DataTableResVM<ContactInfoModel> objDataTableResVM = new DataTableResVM<ContactInfoModel>(objIndexVM.Draw, iTotalCount, iTotalCount, liContactInfoExData);
                return Content(JsonConvert.SerializeObject(objDataTableResVM), "application/json");
            }
            catch (Exception ex)
            {
                DataTableResVM<ContactInfoModel> objDataTableResVM = new DataTableResVM<ContactInfoModel>(ex.Message);
                return Content(JsonConvert.SerializeObject(objDataTableResVM), "application/json");
            }
        }

        public ActionResult Details(long lContactInfoID)
        {
            DBServiceClient clientDB = new DBServiceClient();
            ContactInfoModel objContactInfoData = clientDB.GetContactInfo(lContactInfoID);
            return View(objContactInfoData);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEditVM objCreateEditVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfoModel objContactInfoModel = new ContactInfoModel();
                    objContactInfoModel.Name = objCreateEditVM.Name.Trim();
                    objContactInfoModel.Nickname = (!string.IsNullOrWhiteSpace(objCreateEditVM.Nickname) ? objCreateEditVM.Nickname.Trim() : null);
                    objContactInfoModel.Gender = (ContactInfoModel.EnumGender?)objCreateEditVM.Gender;
                    objContactInfoModel.Age = objCreateEditVM.Age;
                    objContactInfoModel.PhoneNo = objCreateEditVM.PhoneNo.Trim();
                    objContactInfoModel.Address = objCreateEditVM.Address.Trim();

                    DBServiceClient clientDB = new DBServiceClient();
                    clientDB.AddContactInfo(objContactInfoModel);
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

        public ActionResult Edit(long lContactInfoID)
        {
            DBServiceClient clientDB = new DBServiceClient();
            ContactInfoModel objContactInfoModel = clientDB.GetContactInfo(lContactInfoID);

            CreateEditVM objCreateEditVM = new CreateEditVM();
            objCreateEditVM.ContactInfoID = objContactInfoModel.ContactInfoID;
            objCreateEditVM.Name = objContactInfoModel.Name;
            objCreateEditVM.Nickname = objContactInfoModel.Nickname;
            objCreateEditVM.Gender = (CreateEditVM.EnumGender?)objContactInfoModel.Gender;
            objCreateEditVM.Age = objContactInfoModel.Age;
            objCreateEditVM.PhoneNo = objContactInfoModel.PhoneNo;
            objCreateEditVM.Address = objContactInfoModel.Address;
            objCreateEditVM.IsEnable = objContactInfoModel.IsEnable;
            return View(objCreateEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long lContactInfoID, FormCollection objFormCollection)
        {
            try
            {
                DBServiceClient clientDB = new DBServiceClient();
                ContactInfoModel objContactInfoModel = clientDB.GetContactInfo(lContactInfoID);
                if (null != objContactInfoModel && TryUpdateModel<ContactInfoModel>(objContactInfoModel, "", objFormCollection.AllKeys, new string[] { "ContactInfoID", "CreateTime", "UpdateTime" }))
                {
                    clientDB.UpdateContactInfo(objContactInfoModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UpdateError", "更新失敗!");
                    return View(objContactInfoModel);
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
            DBServiceClient clientDB = new DBServiceClient();
            ContactInfoModel objContactInfoModel = clientDB.GetContactInfo(lContactInfoID);
            return View(objContactInfoModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long lContactInfoID, FormCollection objFormCollection)
        {
            try
            {
                DBServiceClient clientDB = new DBServiceClient();
                ContactInfoModel objContactInfoModel = clientDB.GetContactInfo(lContactInfoID);
                if (null != objContactInfoModel)
                {
                    clientDB.DeleteContactInfo(objContactInfoModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DeleteError", "刪除失敗!");
                    return View(objContactInfoModel);
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
        public ActionResult Create_AjaxFormPost(CreateEditVM objCreateEditVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfoModel objContactInfoModel = new ContactInfoModel();
                    objContactInfoModel.Name = objCreateEditVM.Name.Trim();
                    objContactInfoModel.Nickname = (!string.IsNullOrWhiteSpace(objCreateEditVM.Nickname) ? objCreateEditVM.Nickname.Trim() : null);
                    objContactInfoModel.Gender = (ContactInfoModel.EnumGender?)objCreateEditVM.Gender;
                    objContactInfoModel.Age = objCreateEditVM.Age;
                    objContactInfoModel.PhoneNo = objCreateEditVM.PhoneNo.Trim();
                    objContactInfoModel.Address = objCreateEditVM.Address.Trim();

                    DBServiceClient clientDB = new DBServiceClient();
                    clientDB.AddContactInfo(objContactInfoModel);
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
        public ActionResult Create_AjaxModelPost(CreateEditVM objCreateEditVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfoModel objContactInfoModel = new ContactInfoModel();
                    objContactInfoModel.Name = objCreateEditVM.Name.Trim();
                    objContactInfoModel.Nickname = (!string.IsNullOrWhiteSpace(objCreateEditVM.Nickname) ? objCreateEditVM.Nickname.Trim() : null);
                    objContactInfoModel.Gender = (ContactInfoModel.EnumGender?)objCreateEditVM.Gender;
                    objContactInfoModel.Age = objCreateEditVM.Age;
                    objContactInfoModel.PhoneNo = objCreateEditVM.PhoneNo.Trim();
                    objContactInfoModel.Address = objCreateEditVM.Address.Trim();

                    DBServiceClient clientDB = new DBServiceClient();
                    clientDB.AddContactInfo(objContactInfoModel);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(CreateEditVM objCreateEditVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfoModel objContactInfoModel = new ContactInfoModel();
                    objContactInfoModel.Name = objCreateEditVM.Name.Trim();
                    objContactInfoModel.Nickname = (!string.IsNullOrWhiteSpace(objCreateEditVM.Nickname) ? objCreateEditVM.Nickname.Trim() : null);
                    objContactInfoModel.Gender = (ContactInfoModel.EnumGender?)objCreateEditVM.Gender;
                    objContactInfoModel.Age = objCreateEditVM.Age;
                    objContactInfoModel.PhoneNo = objCreateEditVM.PhoneNo.Trim();
                    objContactInfoModel.Address = objCreateEditVM.Address.Trim();

                    DBServiceClient clientDB = new DBServiceClient();
                    clientDB.AddUpdateContactInfo(objContactInfoModel);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("InsertUpdateError", "新增+更新失敗!");
                    return View(objCreateEditVM);
                }
            }
            catch
            {
                ModelState.AddModelError("InsertUpdateError", "新增+更新失敗!");
                return View(objCreateEditVM);
            }
        }
    }
}
