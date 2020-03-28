using System.Collections.Generic;
using System.Web.Mvc;
using WebMVC.Models.Data;
using WebMVC.Models.Repository;

namespace WebMVC.Controllers
{
    public class ContactInfoController : Controller
    {
        public ActionResult Index()
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            List<ContactInfoData> liContactInfoData = objContactInfoRepository.GetContactInfo();
            return View(liContactInfoData);
        }

        public ActionResult Details(long ContactInfoID)
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(ContactInfoID);
            return View(objContactInfoData);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
                return View();
            }
        }

        public ActionResult Edit(long ContactInfoID)
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(ContactInfoID);
            return View(objContactInfoData);
        }

        [HttpPost]
        public ActionResult Edit(long ContactInfoID, FormCollection form)
        {
            try
            {
                ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(ContactInfoID);
                if (null != objContactInfoData && TryUpdateModel<ContactInfoData>(objContactInfoData, "", form.AllKeys, new string[] { "ContactInfoID", "CreateTime", "UpdateTime" }))
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

        public ActionResult Delete(long ContactInfoID)
        {
            ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
            ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(ContactInfoID);
            return View(objContactInfoData);
        }

        [HttpPost]
        public ActionResult Delete(long ContactInfoID, FormCollection form)
        {
            try
            {
                ContactInfoRepository objContactInfoRepository = new ContactInfoRepository();
                ContactInfoData objContactInfoData = objContactInfoRepository.GetContactInfo(ContactInfoID);
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
    }
}
