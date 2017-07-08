using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicesAndInterfaces.Interfaces;
using System.ComponentModel;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private IDataManager _manager;

      
        public HomeController(IDataManager manager)
        {
            _manager = manager;
        }
      
        public ActionResult AllPerson()
        {
            return View(_manager.GetAll());
        }
       
       
    }
}