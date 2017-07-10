using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicesAndInterfaces.Interfaces;
using System.ComponentModel;
using Entities.Models;
using System.Web.UI.WebControls;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private IDataManager _manager;
        private IDataFileValidation _managerValidation;

        public ActionResult Index()
        {
            return View();
        }
        public HomeController(IDataManager manager,IDataFileValidation  managerValidate)
        {
            _manager = manager;
            _managerValidation = managerValidate;
        }

        public ActionResult Xml()
        {
            _manager.CreateXml();
            return View(_manager.GetAll());
        }
        public bool Create()
        {
            try
            {
                _manager.CreateXml();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult CheckNode(int id)
        {
            try
            {
               if( _managerValidation.IsNodeExist(id))
                return Json(true);
               else
                   return Json(false);
            }
            catch
            {
                return Json(false);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                _manager.DeleteNode(id);
                return View("Xml", _manager.GetAll());
            }
            catch
            {
                return View("Xml", _manager.GetAll());
            }

        }

        [HttpGet]
        public PartialViewResult InsertNode()
        {
            return PartialView();
        }



        [HttpPost]
        public ActionResult InsertNode(int id, int Age, string Name, string Gender)
        {
            try
            {
                Person person = new Person();
                //if (!(id > 0 && id <= 90))
                //{
                //    ModelState.AddModelError("id", "Please enter");
                //}

                person.id = id; person.Age = Age; person.Name = Name; person.Gender = Gender;
                _manager.InsertNode(person);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
    }
}