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

      
        public HomeController(IDataManager manager)
        {
            _manager = manager;
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
        public PartialViewResult InserNode()
        {
           
                return PartialView("InserNode");
           
        }


        [HttpPost]
        public ActionResult Insert(int id, int Age, string Name, string Gender)
        {
            try {
                Person person = new Person();
                person.id = id; person.Age = Age; person.Name = Name; person.Gender = Gender;
                _manager.InsertNode(person);
                return Redirect(Url.Action("Xml"));
            }
            catch {
                return Json("test Fail");
            }
        }
        

    }
}